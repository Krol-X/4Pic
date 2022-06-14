namespace _4Pic.src
{
    partial class BinDialogForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.track_thr = new System.Windows.Forms.TrackBar();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.ud_bin = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.track_thr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ud_bin)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Порог";
            // 
            // track_thr
            // 
            this.track_thr.LargeChange = 4;
            this.track_thr.Location = new System.Drawing.Point(105, 12);
            this.track_thr.Maximum = 255;
            this.track_thr.Name = "track_thr";
            this.track_thr.Size = new System.Drawing.Size(349, 45);
            this.track_thr.TabIndex = 9;
            this.track_thr.TickFrequency = 8;
            this.track_thr.Value = 128;
            this.track_thr.Scroll += new System.EventHandler(this.track_thr_Scroll);
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(242, 75);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(200, 23);
            this.button_cancel.TabIndex = 13;
            this.button_cancel.Text = "Отмена";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // button_ok
            // 
            this.button_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_ok.Location = new System.Drawing.Point(12, 75);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(200, 23);
            this.button_ok.TabIndex = 12;
            this.button_ok.Text = "ОК";
            this.button_ok.UseVisualStyleBackColor = true;
            // 
            // ud_bin
            // 
            this.ud_bin.Location = new System.Drawing.Point(56, 20);
            this.ud_bin.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ud_bin.Name = "ud_bin";
            this.ud_bin.Size = new System.Drawing.Size(43, 20);
            this.ud_bin.TabIndex = 15;
            this.ud_bin.ValueChanged += new System.EventHandler(this.ud_bin_ValueChanged);
            // 
            // BinForm
            // 
            this.AcceptButton = this.button_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(454, 106);
            this.Controls.Add(this.ud_bin);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.track_thr);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Бинаризация";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.track_thr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ud_bin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar track_thr;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.NumericUpDown ud_bin;
    }
}