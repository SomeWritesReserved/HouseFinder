namespace HouseFinderUI
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.scrapeObxButton = new System.Windows.Forms.Button();
			this.dumpDebugButton = new System.Windows.Forms.Button();
			this.parseObxButton = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.dataGridView2 = new System.Windows.Forms.DataGridView();
			this.scrapeSbButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
			this.SuspendLayout();
			// 
			// webBrowser1
			// 
			this.webBrowser1.Location = new System.Drawing.Point(0, 0);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new System.Drawing.Size(130, 110);
			this.webBrowser1.TabIndex = 0;
			this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
			// 
			// scrapeObxButton
			// 
			this.scrapeObxButton.Enabled = false;
			this.scrapeObxButton.Location = new System.Drawing.Point(12, 185);
			this.scrapeObxButton.Name = "scrapeObxButton";
			this.scrapeObxButton.Size = new System.Drawing.Size(75, 23);
			this.scrapeObxButton.TabIndex = 1;
			this.scrapeObxButton.Text = "Scrape OBX";
			this.scrapeObxButton.UseVisualStyleBackColor = true;
			this.scrapeObxButton.Click += new System.EventHandler(this.scrapeObxButton_Click);
			// 
			// dumpDebugButton
			// 
			this.dumpDebugButton.Location = new System.Drawing.Point(12, 116);
			this.dumpDebugButton.Name = "dumpDebugButton";
			this.dumpDebugButton.Size = new System.Drawing.Size(75, 23);
			this.dumpDebugButton.TabIndex = 2;
			this.dumpDebugButton.Text = "Dump";
			this.dumpDebugButton.UseVisualStyleBackColor = true;
			this.dumpDebugButton.Click += new System.EventHandler(this.dumpDebugButton_Click);
			// 
			// parseObxButton
			// 
			this.parseObxButton.Location = new System.Drawing.Point(12, 225);
			this.parseObxButton.Name = "parseObxButton";
			this.parseObxButton.Size = new System.Drawing.Size(75, 23);
			this.parseObxButton.TabIndex = 3;
			this.parseObxButton.Text = "Parse OBX";
			this.parseObxButton.UseVisualStyleBackColor = true;
			this.parseObxButton.Click += new System.EventHandler(this.parseObxButton_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(379, 0);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(1189, 511);
			this.dataGridView1.TabIndex = 4;
			// 
			// dataGridView2
			// 
			this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.Location = new System.Drawing.Point(12, 254);
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.Size = new System.Drawing.Size(361, 257);
			this.dataGridView2.TabIndex = 5;
			// 
			// scrapeSbButton
			// 
			this.scrapeSbButton.Enabled = false;
			this.scrapeSbButton.Location = new System.Drawing.Point(130, 185);
			this.scrapeSbButton.Name = "scrapeSbButton";
			this.scrapeSbButton.Size = new System.Drawing.Size(75, 23);
			this.scrapeSbButton.TabIndex = 6;
			this.scrapeSbButton.Text = "Scrape SB";
			this.scrapeSbButton.UseVisualStyleBackColor = true;
			this.scrapeSbButton.Click += new System.EventHandler(this.scrapeSbButton_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1580, 518);
			this.Controls.Add(this.scrapeSbButton);
			this.Controls.Add(this.dataGridView2);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.parseObxButton);
			this.Controls.Add(this.dumpDebugButton);
			this.Controls.Add(this.scrapeObxButton);
			this.Controls.Add(this.webBrowser1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.WebBrowser webBrowser1;
		private System.Windows.Forms.Button scrapeObxButton;
		private System.Windows.Forms.Button dumpDebugButton;
		private System.Windows.Forms.Button parseObxButton;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridView dataGridView2;
		private System.Windows.Forms.Button scrapeSbButton;
	}
}

