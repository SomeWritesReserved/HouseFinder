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
			this.parseSbButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
			this.SuspendLayout();
			// 
			// webBrowser1
			// 
			this.webBrowser1.Location = new System.Drawing.Point(0, 0);
			this.webBrowser1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(40, 38);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new System.Drawing.Size(260, 212);
			this.webBrowser1.TabIndex = 0;
			this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
			// 
			// scrapeObxButton
			// 
			this.scrapeObxButton.Enabled = false;
			this.scrapeObxButton.Location = new System.Drawing.Point(24, 356);
			this.scrapeObxButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.scrapeObxButton.Name = "scrapeObxButton";
			this.scrapeObxButton.Size = new System.Drawing.Size(150, 44);
			this.scrapeObxButton.TabIndex = 1;
			this.scrapeObxButton.Text = "Scrape OBX";
			this.scrapeObxButton.UseVisualStyleBackColor = true;
			this.scrapeObxButton.Click += new System.EventHandler(this.scrapeObxButton_Click);
			// 
			// dumpDebugButton
			// 
			this.dumpDebugButton.Location = new System.Drawing.Point(24, 223);
			this.dumpDebugButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.dumpDebugButton.Name = "dumpDebugButton";
			this.dumpDebugButton.Size = new System.Drawing.Size(150, 44);
			this.dumpDebugButton.TabIndex = 2;
			this.dumpDebugButton.Text = "Dump";
			this.dumpDebugButton.UseVisualStyleBackColor = true;
			this.dumpDebugButton.Click += new System.EventHandler(this.dumpDebugButton_Click);
			// 
			// parseObxButton
			// 
			this.parseObxButton.Location = new System.Drawing.Point(24, 433);
			this.parseObxButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.parseObxButton.Name = "parseObxButton";
			this.parseObxButton.Size = new System.Drawing.Size(150, 44);
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
			this.dataGridView1.Location = new System.Drawing.Point(758, 0);
			this.dataGridView1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(2378, 983);
			this.dataGridView1.TabIndex = 4;
			// 
			// dataGridView2
			// 
			this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.Location = new System.Drawing.Point(24, 488);
			this.dataGridView2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.Size = new System.Drawing.Size(722, 494);
			this.dataGridView2.TabIndex = 5;
			// 
			// scrapeSbButton
			// 
			this.scrapeSbButton.Enabled = false;
			this.scrapeSbButton.Location = new System.Drawing.Point(260, 356);
			this.scrapeSbButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.scrapeSbButton.Name = "scrapeSbButton";
			this.scrapeSbButton.Size = new System.Drawing.Size(150, 44);
			this.scrapeSbButton.TabIndex = 6;
			this.scrapeSbButton.Text = "Scrape SB";
			this.scrapeSbButton.UseVisualStyleBackColor = true;
			this.scrapeSbButton.Click += new System.EventHandler(this.scrapeSbButton_Click);
			// 
			// parseSbButton
			// 
			this.parseSbButton.Location = new System.Drawing.Point(260, 432);
			this.parseSbButton.Margin = new System.Windows.Forms.Padding(6);
			this.parseSbButton.Name = "parseSbButton";
			this.parseSbButton.Size = new System.Drawing.Size(150, 44);
			this.parseSbButton.TabIndex = 7;
			this.parseSbButton.Text = "Parse SB";
			this.parseSbButton.UseVisualStyleBackColor = true;
			this.parseSbButton.Click += new System.EventHandler(this.parseSbButton_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(2740, 996);
			this.Controls.Add(this.parseSbButton);
			this.Controls.Add(this.scrapeSbButton);
			this.Controls.Add(this.dataGridView2);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.parseObxButton);
			this.Controls.Add(this.dumpDebugButton);
			this.Controls.Add(this.scrapeObxButton);
			this.Controls.Add(this.webBrowser1);
			this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
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
		private System.Windows.Forms.Button parseSbButton;
	}
}

