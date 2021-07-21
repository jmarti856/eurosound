
namespace EuroSound_Application.CustomControls.statisticsForm
{
    partial class EuroSound_Graphics
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.Chart_SoundsStatics = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_SoundsStatics)).BeginInit();
            this.SuspendLayout();
            // 
            // Chart_SoundsStatics
            // 
            this.Chart_SoundsStatics.BackColor = System.Drawing.SystemColors.Control;
            this.Chart_SoundsStatics.BorderlineColor = System.Drawing.Color.Black;
            this.Chart_SoundsStatics.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.Chart_SoundsStatics.BorderlineWidth = 0;
            chartArea1.AxisX.Interval = 1D;
            chartArea1.AxisX.MajorGrid.LineWidth = 0;
            chartArea1.AxisX.MaximumAutoSize = 30F;
            chartArea1.AxisX2.MaximumAutoSize = 30F;
            chartArea1.AxisY.MaximumAutoSize = 30F;
            chartArea1.AxisY2.MaximumAutoSize = 30F;
            chartArea1.CursorX.LineColor = System.Drawing.Color.Black;
            chartArea1.CursorY.LineColor = System.Drawing.Color.Black;
            chartArea1.Name = "ChartArea1";
            this.Chart_SoundsStatics.ChartAreas.Add(chartArea1);
            this.Chart_SoundsStatics.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.AutoFitMinFontSize = 5;
            legend1.Name = "Legend1";
            this.Chart_SoundsStatics.Legends.Add(legend1);
            this.Chart_SoundsStatics.Location = new System.Drawing.Point(0, 0);
            this.Chart_SoundsStatics.Name = "Chart_SoundsStatics";
            this.Chart_SoundsStatics.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Grayscale;
            series1.ChartArea = "ChartArea1";
            series1.CustomProperties = "LabelStyle=Bottom";
            series1.IsValueShownAsLabel = true;
            series1.IsVisibleInLegend = false;
            series1.Label = "#VAL{D}";
            series1.Legend = "Legend1";
            series1.Name = "Sounds";
            series1.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.No;
            this.Chart_SoundsStatics.Series.Add(series1);
            this.Chart_SoundsStatics.Size = new System.Drawing.Size(592, 481);
            this.Chart_SoundsStatics.TabIndex = 0;
            this.Chart_SoundsStatics.Text = "chart1";
            this.Chart_SoundsStatics.TextAntiAliasingQuality = System.Windows.Forms.DataVisualization.Charting.TextAntiAliasingQuality.SystemDefault;
            title1.Name = "Main Title";
            title1.Text = "PS2 Sounds Frequencies Statistics";
            this.Chart_SoundsStatics.Titles.Add(title1);
            // 
            // EuroSound_Graphics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 481);
            this.Controls.Add(this.Chart_SoundsStatics);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EuroSound_Graphics";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Statistics";
            this.Load += new System.EventHandler(this.EuroSound_Graphics_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Chart_SoundsStatics)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_SoundsStatics;
    }
}