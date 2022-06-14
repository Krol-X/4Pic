namespace _4Pic.src
{
    partial class PowerDialogForm
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
            this.ud_k = new System.Windows.Forms.NumericUpDown();
            this.ud_c = new System.Windows.Forms.NumericUpDown();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.track_k = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.track_c = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.ud_k)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ud_c)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.track_k)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.track_c)).BeginInit();
            this.SuspendLayout();
            // 
            // ud_k
            // 
            this.ud_k.Location = new System.Drawing.Point(65, 46);
            this.ud_k.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ud_k.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ud_k.Name = "ud_k";
            this.ud_k.Size = new System.Drawing.Size(43, 20);
            this.ud_k.TabIndex = 19;
            this.ud_k.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ud_k.ValueChanged += new System.EventHandler(this.ud_k_ValueChanged);
            // 
            // ud_c
            // 
            this.ud_c.Location = new System.Drawing.Point(65, 20);
            this.ud_c.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ud_c.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ud_c.Name = "ud_c";
            this.ud_c.Size = new System.Drawing.Size(43, 20);
            this.ud_c.TabIndex = 18;
            this.ud_c.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ud_c.ValueChanged += new System.EventHandler(this.ud_c_ValueChanged);
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(280, 79);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(220, 23);
            this.button_cancel.TabIndex = 16;
            this.button_cancel.Text = "Отмена";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // button_ok
            // 
            this.button_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_ok.Location = new System.Drawing.Point(12, 79);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(220, 23);
            this.button_ok.TabIndex = 15;
            this.button_ok.Text = "ОК";
            this.button_ok.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Коеф. K";
            // 
            // track_k
            // 
            this.track_k.LargeChange = 4;
            this.track_k.Location = new System.Drawing.Point(114, 42);
            this.track_k.Maximum = 255;
            this.track_k.Minimum = 1;
            this.track_k.Name = "track_k";
            this.track_k.Size = new System.Drawing.Size(386, 45);
            this.track_k.TabIndex = 13;
            this.track_k.TickFrequency = 8;
            this.track_k.Value = 1;
            this.track_k.Scroll += new System.EventHandler(this.track_k_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Коеф. C";
            // 
            // track_c
            // 
            this.track_c.LargeChange = 4;
            this.track_c.Location = new System.Drawing.Point(114, 14);
            this.track_c.Maximum = 255;
            this.track_c.Minimum = 1;
            this.track_c.Name = "track_c";
            this.track_c.Size = new System.Drawing.Size(386, 45);
            this.track_c.TabIndex = 12;
            this.track_c.TickFrequency = 8;
            this.track_c.Value = 1;
            this.track_c.Scroll += new System.EventHandler(this.track_c_Scroll);
            // 
            // PowerForm
            // 
            this.AcceptButton = this.button_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(512, 117);
            this.Controls.Add(this.ud_k);
            this.Controls.Add(this.ud_c);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.track_k);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.track_c);
            this.Name = "PowerForm";
            this.Text = "PowerForm";
            ((System.ComponentModel.ISupportInitialize)(this.ud_k)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ud_c)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.track_k)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.track_c)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown ud_k;
        private System.Windows.Forms.NumericUpDown ud_c;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar track_k;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar track_c;
    }
}