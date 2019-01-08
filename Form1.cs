using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Device.Location;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace HouseFinderUI
{
	public partial class Form1 : Form
	{
		private string currentHouse;
		private AutoResetEvent autoResetEvent = new AutoResetEvent(false);
		private int docCompletedCount = 0;
		private int houseCompletedCount = 0;
		private volatile bool isWaiting = false;

		public Form1()
		{
			InitializeComponent();
		}

		private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if (this.isWaiting)
			{
				this.docCompletedCount++;
				this.autoResetEvent.Set();
				this.isWaiting = false;
			}
		}

		private void dumpDebugButton_Click(object sender, EventArgs e)
		{
			/*var documentAsIHtmlDocument3 = (mshtml.IHTMLDocument3)this.webBrowser1.Document.DomDocument;
			var content = documentAsIHtmlDocument3.documentElement.innerHTML;

			File.WriteAllText("test.html", content);*/
		}


		private void scrapeObxButton_Click(object sender, EventArgs e)
		{
			new Thread(() =>
			{
				// Step 1: Scrape all obx houses
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(@"obxhouses.xml");

				var allHouses = xmlDocument.SelectNodes("houses/div/div/a").OfType<XmlNode>().Select((node) => node.Attributes["href"].Value).ToArray();

				foreach (var housePath in allHouses)
				{
					string fullUrl = "https://www.outerbeaches.com" + housePath;

					this.currentHouse = housePath;
					Directory.CreateDirectory("obx");

					this.Invoke(new Action(() =>
					{
						this.Text = $"({this.houseCompletedCount} - ${this.docCompletedCount}) {housePath}";
						this.isWaiting = true;
						this.webBrowser1.Navigate(fullUrl);
					}));
					this.autoResetEvent.WaitOne();
					this.autoResetEvent.Reset();
					Thread.Sleep(1000);

					this.Invoke(new Action(() =>
					{
						//var documentAsIHtmlDocument3 = (mshtml.IHTMLDocument3)this.webBrowser1.Document.DomDocument;
						//var content = documentAsIHtmlDocument3.documentElement.innerHTML;
						//File.WriteAllText(Path.Combine("obx", this.currentHouse.Replace("/property/", string.Empty)) + ".html", content);
					}));
					this.houseCompletedCount++;
				}
			}).Start();
		}


		private void scrapeSbButton_Click(object sender, EventArgs e)
		{
			new Thread(() =>
			{
				// Step 1: Scrape all obx houses
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(@"sbhouses.xml");

				var allHouses = xmlDocument.SelectNodes("ul/li").OfType<XmlNode>()
					.Select((node) => node.SelectSingleNode(".//a").Attributes["href"].Value)
					.Select((fullUrl) => fullUrl.Substring(0, fullUrl.IndexOf('?')))
					.ToArray();

				foreach (var housePath in allHouses)
				{
					string fullUrl = "https://www.sandbridge.com" + housePath;

					WebClient webClient = new WebClient();
					webClient.DownloadFile(fullUrl, Path.Combine("sb", housePath.Replace("/rental/house/", string.Empty)) + ".html");

					/*this.currentHouse = housePath;
					Directory.CreateDirectory("sb");

					this.Invoke(new Action(() =>
					{
						this.Text = $"({this.houseCompletedCount} - ${this.docCompletedCount}) {housePath}";
						this.isWaiting = true;
						this.webBrowser1.Navigate(fullUrl);
					}));
					this.autoResetEvent.WaitOne();
					this.autoResetEvent.Reset();
					Thread.Sleep(1000);

					this.Invoke(new Action(() =>
					{
						var documentAsIHtmlDocument3 = (mshtml.IHTMLDocument3)this.webBrowser1.Document.DomDocument;
						var content = documentAsIHtmlDocument3.documentElement.innerHTML;
						File.WriteAllText(Path.Combine("sb", this.currentHouse.Replace("/rental/house/", string.Empty)) + ".html", content);
					}));
					this.houseCompletedCount++;*/
				}
			}).Start();
		}

		private void parseObxButton_Click(object sender, EventArgs e)
		{
			// Step 2: Parse scraped houses for information
			List<Rental> rentals = new List<Rental>();
			foreach (string houseFile in Directory.GetFiles("obx"))
			{
				string houseText = File.ReadAllText(houseFile);
				string name = Regex.Match(houseText, "<h1 class=\"property-page-title\">(.+)</h1>").Groups[1].Value;
				string summary = Regex.Match(houseText, "<h2 class=\"property-page-subtitle\">(.+)</h2>").Groups[1].Value;
				if (summary.Contains("Condo")) { continue; }

				Rental rental = new Rental();
				rental.Url = "https://www.outerbeaches.com/property/" + Path.GetFileNameWithoutExtension(houseFile);
				rental.Name = name;
				rental.Bedrooms = int.Parse(Regex.Match(summary, @"(\d+) [bB]edroom").Groups[1].Value);
				rental.Town = Regex.Match(summary, @"in (.+)").Groups[1].Value;
				rental.RentalType = (RentalType)Enum.Parse(typeof(RentalType), Regex.Match(summary, @",(.+) [hH]ouse").Groups[1].Value.Trim().Replace(" ", "").Replace("-", ""), true);
				rental.HasPool = houseText.Contains("lr-021-pool");

				var coordinateMatch = Regex.Match(houseText, @"map.setView\(\[(-?\d+\.\d+) , (-?\d+\.\d+)\]");
				rental.CoordinateX = coordinateMatch.Groups[1].Value;
				rental.CoordinateY = coordinateMatch.Groups[2].Value;

				foreach (var match in Regex.Matches(houseText, "<tr.+?</tr>").OfType<Match>().Skip(1))
				{
					XmlDocument weeklyPriceXmlDoc = new XmlDocument();
					weeklyPriceXmlDoc.LoadXml(match.Value);
					string weekStart = weeklyPriceXmlDoc.FirstChild.ChildNodes[1].InnerText;
					string priceString = weeklyPriceXmlDoc.FirstChild.ChildNodes[3].InnerText;
					if (priceString == "Reserved") { continue; }
					int price = int.Parse(priceString.Replace("$", "").Replace(",", ""));

					rental.WeekPrices.Add(new WeekPrice()
					{
						StartDate = DateTime.Parse(weekStart.Trim().Replace("th,", ",").Replace("st,", ",").Replace("nd,", ",").Replace("rd,", ",")),
						Price = price,
					});
				}

				rentals.Add(rental);
			}

			//================================
			// filter
			//================================
			rentals = rentals
				.Where((r) => r.RentalType == RentalType.Oceanfront || r.RentalType == RentalType.SemiOceanfront || r.RentalType == RentalType.Oceanside)
				.Where((r) => r.Bedrooms >= 4)
				//.Where((r) => r.HasPool)
				//.Where((r) => r.PricePerBedroom < 810)
				.Where((r) => r.DatePrice > 0)
				.Where((r) => r.Town != "Frisco" && r.Town != "Buxton" && r.Town != "Hatteras")
				.ToList();

			//================================
			// calc distances
			//================================
			List<BetweenDistance> betweenDistances = new List<BetweenDistance>();
			foreach (Rental rentalA in rentals)
			{
				Rental closestRental = null;
				double closestDistance = double.MaxValue;
				foreach (Rental rentalB in rentals)
				{
					if (rentalA == rentalB) { continue; }

					GeoCoordinate coordinateA = new GeoCoordinate(double.Parse(rentalA.CoordinateX), double.Parse(rentalA.CoordinateY));
					GeoCoordinate coordinateB = new GeoCoordinate(double.Parse(rentalB.CoordinateX), double.Parse(rentalB.CoordinateY));
					double distanceBetween = Math.Round(coordinateA.GetDistanceTo(coordinateB) * 3.28084);

					if (distanceBetween < closestDistance)
					{
						closestRental = rentalB;
						closestDistance = distanceBetween;
					}

					betweenDistances.Add(new BetweenDistance()
					{
						DistanceBetween = distanceBetween,
						RentalA = rentalA,
						RentalB = rentalB,
					});
				}
				rentalA.ClosestRental = closestRental;
				rentalA.ClosestRentalDistance = closestDistance;
			}

			Clipboard.SetText(string.Join(Environment.NewLine, rentals.OrderBy((r) => r.ClosestRentalDistance).Select((r) => $"{r.Name}\t{r.Town}\t{r.RentalType}\t{r.Bedrooms}\t{r.HasPool}\t{r.DatePrice}\t{r.PricePerBedroom}\t{r.ClosestRental.Name}\t{r.ClosestRentalDistance}\t{r.CoordinateX}\t{r.CoordinateY}\t{r.Url}")));

			this.Text = rentals.Count.ToString();
			this.dataGridView1.DataSource = rentals
				.OrderBy((r) => r.ClosestRentalDistance)
				//.OrderBy((r) => r.Town)
				//.ThenBy((r) => r.PricePerBedroom)
				.ToList();

			this.dataGridView2.DataSource = betweenDistances
				.OrderBy((d) => d.DistanceBetween)
				.Where((d) => d.DistanceBetween < 1320)
				.ToList();
		}

		private void parseSbButton_Click(object sender, EventArgs e)
		{
			// Step 2: Parse scraped houses for information
			List<Rental> rentals = new List<Rental>();
			foreach (string houseFile in Directory.GetFiles("sb"))
			{
				string houseText = File.ReadAllText(houseFile);

				Rental rental = new Rental();
				rental.Url = "https://www.sandbridge.com/rental/house/" + Path.GetFileNameWithoutExtension(houseFile);
				rental.Name = Regex.Match(houseText, "<title>(.+)</title>").Groups[1].Value.Split('|')[0].Trim();
				rental.Bedrooms = int.Parse(Regex.Match(houseText, @"<li><strong>Bedrooms:</strong> (\d+)</li>").Groups[1].Value);
				rental.Town = "Sandbridge";
				rental.RentalType = (RentalType)Enum.Parse(typeof(RentalType), Regex.Match(houseText, @">(.+) -  Rental Property").Groups[1].Value.Trim().Replace(" ", "").Replace("-", "").Replace("Bay/Canal", "Canalfront"), true);
				rental.HasPool = houseText.Contains("Pool Season");

				var coordinateMatch = Regex.Match(houseText, @"lat=(-?\d+\.\d+)&lng=(-?\d+\.\d+)");
				rental.CoordinateX = coordinateMatch.Groups[1].Value;
				rental.CoordinateY = coordinateMatch.Groups[2].Value;

				var priceTableText = Regex.Match(houseText.Replace("\r", "").Replace("\n", ""), "<tbody>.+?</tbody>").Value;
				XmlDocument priceTableXmlDoc = new XmlDocument();
				priceTableXmlDoc.LoadXml(priceTableText);
				foreach (XmlNode weekNode in priceTableXmlDoc.FirstChild.SelectNodes("tr"))
				{
					DateTime startDate = DateTime.Parse(weekNode.ChildNodes[0].InnerText);
					string priceString = weekNode.ChildNodes[3].InnerText.Trim();
					if (priceString == "N/A") { continue; }
					int price = (int)double.Parse(priceString.Replace("$", "").Replace(",", ""));

					rental.WeekPrices.Add(new WeekPrice()
					{
						StartDate = startDate,
						Price = price,
					});
				}

				rentals.Add(rental);
			}

			//================================
			// filter
			//================================
			rentals = rentals
				.Where((r) => r.RentalType == RentalType.Oceanfront || r.RentalType == RentalType.SemiOceanfront || r.RentalType == RentalType.Oceanside || r.RentalType == RentalType.Waterview3rdRow || r.RentalType == RentalType.Waterview4thRow)
				.Where((r) => r.Bedrooms >= 4)
				//.Where((r) => r.HasPool)
				//.Where((r) => r.PricePerBedroom < 810)
				.Where((r) => r.DatePrice > 0)
				.ToList();

			//================================
			// calc distances
			//================================
			List<BetweenDistance> betweenDistances = new List<BetweenDistance>();
			foreach (Rental rentalA in rentals)
			{
				Rental closestRental = null;
				double closestDistance = double.MaxValue;
				foreach (Rental rentalB in rentals)
				{
					if (rentalA == rentalB) { continue; }

					GeoCoordinate coordinateA = new GeoCoordinate(double.Parse(rentalA.CoordinateX), double.Parse(rentalA.CoordinateY));
					GeoCoordinate coordinateB = new GeoCoordinate(double.Parse(rentalB.CoordinateX), double.Parse(rentalB.CoordinateY));
					double distanceBetween = Math.Round(coordinateA.GetDistanceTo(coordinateB) * 3.28084);

					if (distanceBetween < closestDistance)
					{
						closestRental = rentalB;
						closestDistance = distanceBetween;
					}

					betweenDistances.Add(new BetweenDistance()
					{
						DistanceBetween = distanceBetween,
						RentalA = rentalA,
						RentalB = rentalB,
					});
				}
				rentalA.ClosestRental = closestRental;
				rentalA.ClosestRentalDistance = closestDistance;
			}

			Clipboard.SetText(string.Join(Environment.NewLine, rentals.OrderBy((r) => r.ClosestRentalDistance).Select((r) => $"{r.Name}\t{r.Town}\t{r.RentalType}\t{r.Bedrooms}\t{r.HasPool}\t{r.DatePrice}\t{r.PricePerBedroom}\t{r.ClosestRental.Name}\t{r.ClosestRentalDistance}\t{r.CoordinateX}\t{r.CoordinateY}\t{r.Url}")));

			this.Text = rentals.Count.ToString();
			this.dataGridView1.DataSource = rentals
				.OrderBy((r) => r.ClosestRentalDistance)
				//.OrderBy((r) => r.Town)
				//.ThenBy((r) => r.PricePerBedroom)
				.ToList();

			this.dataGridView2.DataSource = betweenDistances
				.OrderBy((d) => d.DistanceBetween)
				.Where((d) => d.DistanceBetween < 1320)
				.ToList();
		}
	}

	public class Rental
	{
		public List<WeekPrice> WeekPrices = new List<WeekPrice>();
		public string Name { get; set; }
		public RentalType RentalType { get; set; }
		public string Town { get; set; }
		public int Bedrooms { get; set; }
		public bool HasPool { get; set; }

		public int DatePrice
		{
			get
			{
				int price = -1;

				var weeks = this.WeekPrices.Where((w) => w.StartDate.Month == 8 && (w.StartDate.Day == 9 || w.StartDate.Day == 10));
				if (weeks.Any())
				{
					price = weeks.Single().Price;
				}

				return price;
			}
		}

		public int PricePerBedroom
		{
			get { return this.DatePrice / this.Bedrooms; }
		}

		public Rental ClosestRental { get; set; }
		public double ClosestRentalDistance { get; set; }

		public string CoordinateX { get; set; }
		public string CoordinateY { get; set; }

		public string Url { get; set; }


		public override string ToString()
		{
			return this.Name;
		}
	}

	public class WeekPrice
	{
		public DateTime StartDate { get; set; }
		public int Price { get; set; }
	}

	public class BetweenDistance
	{
		public double DistanceBetween { get; set; }
		public string RentalAName { get { return this.RentalA.Name; } }
		public string RentalBName { get { return this.RentalB.Name; } }
		public Rental RentalA;
		public Rental RentalB;
	}

	public enum RentalType
	{
		Oceanfront,
		SemiOceanfront,
		Oceanside,

		Soundfront,
		SemiSoundfront,
		Soundside,

		Canalfront,

		// SB
		Waterview3rdRow,
		Waterview4thRow,
	}
}
