namespace _4Pic
{
    partial class MainForm
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
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.MainMenu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Script = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Run = new System.Windows.Forms.ToolStripMenuItem();
            this.MainCanvas = new System.Windows.Forms.PictureBox();
            this.OpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveAsDialog = new System.Windows.Forms.SaveFileDialog();
            this.FlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainCanvas)).BeginInit();
            this.FlowPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_File,
            this.MainMenu_Script});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(560, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
            // 
            // MainMenu_File
            // 
            this.MainMenu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_Open,
            this.MainMenu_SaveAs,
            this.MainMenu_Exit});
            this.MainMenu_File.Name = "MainMenu_File";
            this.MainMenu_File.Size = new System.Drawing.Size(48, 20);
            this.MainMenu_File.Text = "Файл";
            // 
            // MainMenu_Open
            // 
            this.MainMenu_Open.Name = "MainMenu_Open";
            this.MainMenu_Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MainMenu_Open.Size = new System.Drawing.Size(202, 22);
            this.MainMenu_Open.Text = "Открыть";
            this.MainMenu_Open.Click += new System.EventHandler(this.MainMenu_Open_Click);
            // 
            // MainMenu_SaveAs
            // 
            this.MainMenu_SaveAs.Name = "MainMenu_SaveAs";
            this.MainMenu_SaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.MainMenu_SaveAs.Size = new System.Drawing.Size(202, 22);
            this.MainMenu_SaveAs.Text = "Сохранить как...";
            this.MainMenu_SaveAs.Click += new System.EventHandler(this.MainMenu_SaveAs_Click);
            // 
            // MainMenu_Exit
            // 
            this.MainMenu_Exit.Name = "MainMenu_Exit";
            this.MainMenu_Exit.Size = new System.Drawing.Size(202, 22);
            this.MainMenu_Exit.Text = "Выйти";
            this.MainMenu_Exit.Click += new System.EventHandler(this.MainMenu_Exit_Click);
            // 
            // MainMenu_Script
            // 
            this.MainMenu_Script.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_Run});
            this.MainMenu_Script.Name = "MainMenu_Script";
            this.MainMenu_Script.Size = new System.Drawing.Size(59, 20);
            this.MainMenu_Script.Text = "Скрипт";
            // 
            // MainMenu_Run
            // 
            this.MainMenu_Run.Name = "MainMenu_Run";
            this.MainMenu_Run.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.MainMenu_Run.Size = new System.Drawing.Size(164, 22);
            this.MainMenu_Run.Text = "Выполнить...";
            this.MainMenu_Run.Click += new System.EventHandler(this.MainMenu_Run_Click);
            // 
            // MainCanvas
            // 
            this.MainCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainCanvas.Location = new System.Drawing.Point(3, 3);
            this.MainCanvas.Name = "MainCanvas";
            this.MainCanvas.Size = new System.Drawing.Size(32, 32);
            this.MainCanvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.MainCanvas.TabIndex = 1;
            this.MainCanvas.TabStop = false;
            // 
            // OpenDialog
            // 
            this.OpenDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenDialog_FileOk);
            // 
            // SaveAsDialog
            // 
            this.SaveAsDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveAsDialog_FileOk);
            // 
            // FlowPanel
            // 
            this.FlowPanel.Controls.Add(this.MainCanvas);
            this.FlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlowPanel.Location = new System.Drawing.Point(0, 24);
            this.FlowPanel.Name = "FlowPanel";
            this.FlowPanel.Size = new System.Drawing.Size(560, 347);
            this.FlowPanel.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 371);
            this.Controls.Add(this.FlowPanel);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "4Pic";
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseWheel);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainCanvas)).EndInit();
            this.FlowPanel.ResumeLayout(false);
            this.FlowPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_File;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Open;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_SaveAs;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Exit;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Script;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Run;
        private System.Windows.Forms.PictureBox MainCanvas;
        private System.Windows.Forms.OpenFileDialog OpenDialog;
        private System.Windows.Forms.SaveFileDialog SaveAsDialog;
        private System.Windows.Forms.FlowLayoutPanel FlowPanel;
    }
}

