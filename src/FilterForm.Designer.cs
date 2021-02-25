namespace _4Pic.src
{
    partial class FilterForm
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
            this.textbox = new System.Windows.Forms.TextBox();
            this.combo_type = new System.Windows.Forms.ComboBox();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_preview = new System.Windows.Forms.Button();
            this.dialog_open = new System.Windows.Forms.OpenFileDialog();
            this.button_save = new System.Windows.Forms.Button();
            this.button_open = new System.Windows.Forms.Button();
            this.dialog_save = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // textbox
            // 
            this.textbox.Location = new System.Drawing.Point(12, 39);
            this.textbox.Multiline = true;
            this.textbox.Name = "textbox";
            this.textbox.Size = new System.Drawing.Size(240, 200);
            this.textbox.TabIndex = 0;
            // 
            // combo_type
            // 
            this.combo_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_type.FormattingEnabled = true;
            this.combo_type.Items.AddRange(new object[] {
            "Средний",
            "Медианный"});
            this.combo_type.Location = new System.Drawing.Point(12, 12);
            this.combo_type.Name = "combo_type";
            this.combo_type.Size = new System.Drawing.Size(193, 21);
            this.combo_type.TabIndex = 1;
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(142, 278);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(110, 23);
            this.button_cancel.TabIndex = 6;
            this.button_cancel.Text = "Отмена";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // button_ok
            // 
            this.button_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_ok.Location = new System.Drawing.Point(12, 278);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(110, 23);
            this.button_ok.TabIndex = 5;
            this.button_ok.Text = "ОК";
            this.button_ok.UseVisualStyleBackColor = true;
            // 
            // button_preview
            // 
            this.button_preview.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_preview.Location = new System.Drawing.Point(12, 249);
            this.button_preview.Name = "button_preview";
            this.button_preview.Size = new System.Drawing.Size(240, 23);
            this.button_preview.TabIndex = 7;
            this.button_preview.Text = "Предпросмотр";
            this.button_preview.UseVisualStyleBackColor = true;
            this.button_preview.Click += new System.EventHandler(this.button_preview_Click);
            // 
            // dialog_open
            // 
            this.dialog_open.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";
            // 
            // button_save
            // 
            this.button_save.BackgroundImage = global::_4Pic.Properties.Resources.save;
            this.button_save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_save.Location = new System.Drawing.Point(231, 12);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(21, 21);
            this.button_save.TabIndex = 8;
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_open
            // 
            this.button_open.BackgroundImage = global::_4Pic.Properties.Resources.open;
            this.button_open.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_open.Location = new System.Drawing.Point(211, 12);
            this.button_open.Name = "button_open";
            this.button_open.Size = new System.Drawing.Size(21, 21);
            this.button_open.TabIndex = 2;
            this.button_open.UseVisualStyleBackColor = true;
            this.button_open.Click += new System.EventHandler(this.button_open_Click);
            // 
            // dialog_save
            // 
            this.dialog_save.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 312);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.button_preview);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.button_open);
            this.Controls.Add(this.combo_type);
            this.Controls.Add(this.textbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FilterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filter";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textbox;
        private System.Windows.Forms.ComboBox combo_type;
        private System.Windows.Forms.Button button_open;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_preview;
        private System.Windows.Forms.OpenFileDialog dialog_open;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.SaveFileDialog dialog_save;
    }
}