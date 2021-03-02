namespace _4Pic.src
{
    partial class BriConForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.track_bri = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.track_con = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.ud_bri = new System.Windows.Forms.NumericUpDown();
            this.ud_con = new System.Windows.Forms.NumericUpDown();
            this.chart_hist = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.track_bri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.track_con)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ud_bri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ud_con)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_hist)).BeginInit();
            this.SuspendLayout();
            // 
            // track_bri
            // 
            this.track_bri.LargeChange = 4;
            this.track_bri.Location = new System.Drawing.Point(148, 121);
            this.track_bri.Maximum = 127;
            this.track_bri.Minimum = -128;
            this.track_bri.Name = "track_bri";
            this.track_bri.Size = new System.Drawing.Size(352, 45);
            this.track_bri.TabIndex = 1;
            this.track_bri.TickFrequency = 8;
            this.track_bri.Scroll += new System.EventHandler(this.track_bri_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Яркость";
            // 
            // track_con
            // 
            this.track_con.LargeChange = 4;
            this.track_con.Location = new System.Drawing.Point(148, 149);
            this.track_con.Maximum = 127;
            this.track_con.Minimum = -128;
            this.track_con.Name = "track_con";
            this.track_con.Size = new System.Drawing.Size(352, 45);
            this.track_con.TabIndex = 2;
            this.track_con.TickFrequency = 8;
            this.track_con.Scroll += new System.EventHandler(this.track_cont_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Контрастность";
            // 
            // button_ok
            // 
            this.button_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_ok.Location = new System.Drawing.Point(12, 186);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(220, 23);
            this.button_ok.TabIndex = 3;
            this.button_ok.Text = "ОК";
            this.button_ok.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(280, 186);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(220, 23);
            this.button_cancel.TabIndex = 4;
            this.button_cancel.Text = "Отмена";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // ud_bri
            // 
            this.ud_bri.Location = new System.Drawing.Point(99, 128);
            this.ud_bri.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.ud_bri.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.ud_bri.Name = "ud_bri";
            this.ud_bri.Size = new System.Drawing.Size(43, 20);
            this.ud_bri.TabIndex = 10;
            this.ud_bri.ValueChanged += new System.EventHandler(this.ud_bri_ValueChanged);
            // 
            // ud_con
            // 
            this.ud_con.Location = new System.Drawing.Point(99, 157);
            this.ud_con.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.ud_con.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.ud_con.Name = "ud_con";
            this.ud_con.Size = new System.Drawing.Size(43, 20);
            this.ud_con.TabIndex = 11;
            this.ud_con.ValueChanged += new System.EventHandler(this.ud_con_ValueChanged);
            // 
            // chart_hist
            // 
            this.chart_hist.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea1.Name = "ChartArea1";
            this.chart_hist.ChartAreas.Add(chartArea1);
            this.chart_hist.Dock = System.Windows.Forms.DockStyle.Top;
            this.chart_hist.Location = new System.Drawing.Point(0, 0);
            this.chart_hist.Name = "chart_hist";
            this.chart_hist.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series1.ChartArea = "ChartArea1";
            series1.Name = "main";
            this.chart_hist.Series.Add(series1);
            this.chart_hist.Size = new System.Drawing.Size(512, 115);
            this.chart_hist.TabIndex = 12;
            this.chart_hist.TabStop = false;
            this.chart_hist.Text = "chart1";
            // 
            // BriConForm
            // 
            this.AcceptButton = this.button_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(512, 216);
            this.ControlBox = false;
            this.Controls.Add(this.chart_hist);
            this.Controls.Add(this.ud_con);
            this.Controls.Add(this.ud_bri);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.track_con);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.track_bri);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BriConForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Яркость и контрастность";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.track_bri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.track_con)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ud_bri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ud_con)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_hist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar track_bri;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar track_con;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.NumericUpDown ud_bri;
        private System.Windows.Forms.NumericUpDown ud_con;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_hist;
    }
}