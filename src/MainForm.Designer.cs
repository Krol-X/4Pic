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
            this.MainMenu_Image = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_undoall = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_undo = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_redo = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_redoall = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenu_tonegative = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_tograyscale = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_tobinary = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenu_bri_con = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_power = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_lincor = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_filter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenu_figures = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Script = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Run = new System.Windows.Forms.ToolStripMenuItem();
            this.imgScaleComboBox = new System.Windows.Forms.ToolStripComboBox();
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
            this.MainMenu_Image,
            this.MainMenu_Script,
            this.imgScaleComboBox});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(560, 27);
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
            this.MainMenu_File.Size = new System.Drawing.Size(48, 23);
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
            // MainMenu_Image
            // 
            this.MainMenu_Image.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_undoall,
            this.MainMenu_undo,
            this.MainMenu_redo,
            this.MainMenu_redoall,
            this.toolStripSeparator2,
            this.MainMenu_tonegative,
            this.MainMenu_tograyscale,
            this.MainMenu_tobinary,
            this.MainMenu_sep1,
            this.MainMenu_bri_con,
            this.MainMenu_power,
            this.MainMenu_lincor,
            this.MainMenu_filter,
            this.toolStripSeparator1,
            this.MainMenu_figures});
            this.MainMenu_Image.Name = "MainMenu_Image";
            this.MainMenu_Image.Size = new System.Drawing.Size(95, 23);
            this.MainMenu_Image.Text = "Изображение";
            // 
            // MainMenu_undoall
            // 
            this.MainMenu_undoall.Name = "MainMenu_undoall";
            this.MainMenu_undoall.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Z)));
            this.MainMenu_undoall.Size = new System.Drawing.Size(251, 22);
            this.MainMenu_undoall.Text = "Отменить всё";
            this.MainMenu_undoall.Click += new System.EventHandler(this.MainMenu_undoall_Click);
            // 
            // MainMenu_undo
            // 
            this.MainMenu_undo.Name = "MainMenu_undo";
            this.MainMenu_undo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.MainMenu_undo.Size = new System.Drawing.Size(251, 22);
            this.MainMenu_undo.Text = "Отменить";
            this.MainMenu_undo.Click += new System.EventHandler(this.MainMenu_undo_Click);
            // 
            // MainMenu_redo
            // 
            this.MainMenu_redo.Name = "MainMenu_redo";
            this.MainMenu_redo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.MainMenu_redo.Size = new System.Drawing.Size(251, 22);
            this.MainMenu_redo.Text = "Повторить";
            this.MainMenu_redo.Click += new System.EventHandler(this.MainMenu_redo_Click);
            // 
            // MainMenu_redoall
            // 
            this.MainMenu_redoall.Name = "MainMenu_redoall";
            this.MainMenu_redoall.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Y)));
            this.MainMenu_redoall.Size = new System.Drawing.Size(251, 22);
            this.MainMenu_redoall.Text = "Повторить всё";
            this.MainMenu_redoall.Click += new System.EventHandler(this.MainMenu_redoall_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(248, 6);
            // 
            // MainMenu_tonegative
            // 
            this.MainMenu_tonegative.Name = "MainMenu_tonegative";
            this.MainMenu_tonegative.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.MainMenu_tonegative.Size = new System.Drawing.Size(251, 22);
            this.MainMenu_tonegative.Text = "В негатив";
            this.MainMenu_tonegative.Click += new System.EventHandler(this.MainMenu_tonegative_Click);
            // 
            // MainMenu_tograyscale
            // 
            this.MainMenu_tograyscale.Name = "MainMenu_tograyscale";
            this.MainMenu_tograyscale.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.MainMenu_tograyscale.Size = new System.Drawing.Size(251, 22);
            this.MainMenu_tograyscale.Text = "В оттенки серого";
            this.MainMenu_tograyscale.Click += new System.EventHandler(this.MainMenu_tograyscale_Click);
            // 
            // MainMenu_tobinary
            // 
            this.MainMenu_tobinary.Name = "MainMenu_tobinary";
            this.MainMenu_tobinary.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.MainMenu_tobinary.Size = new System.Drawing.Size(251, 22);
            this.MainMenu_tobinary.Text = "Бинаризация";
            this.MainMenu_tobinary.Click += new System.EventHandler(this.MainMenu_tobinary_Click);
            // 
            // MainMenu_sep1
            // 
            this.MainMenu_sep1.Name = "MainMenu_sep1";
            this.MainMenu_sep1.Size = new System.Drawing.Size(248, 6);
            // 
            // MainMenu_bri_con
            // 
            this.MainMenu_bri_con.Name = "MainMenu_bri_con";
            this.MainMenu_bri_con.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.MainMenu_bri_con.Size = new System.Drawing.Size(251, 22);
            this.MainMenu_bri_con.Text = "Яркость и контрастность";
            this.MainMenu_bri_con.Click += new System.EventHandler(this.MainMenu_bri_con_Click);
            // 
            // MainMenu_power
            // 
            this.MainMenu_power.Name = "MainMenu_power";
            this.MainMenu_power.Size = new System.Drawing.Size(251, 22);
            this.MainMenu_power.Text = "Степенная функция";
            this.MainMenu_power.Click += new System.EventHandler(this.MainMenu_power_Click);
            // 
            // MainMenu_lincor
            // 
            this.MainMenu_lincor.Name = "MainMenu_lincor";
            this.MainMenu_lincor.Size = new System.Drawing.Size(251, 22);
            this.MainMenu_lincor.Text = "Линейная коррекция";
            this.MainMenu_lincor.Click += new System.EventHandler(this.MainMenu_lincor_Click);
            // 
            // MainMenu_filter
            // 
            this.MainMenu_filter.Name = "MainMenu_filter";
            this.MainMenu_filter.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.MainMenu_filter.Size = new System.Drawing.Size(251, 22);
            this.MainMenu_filter.Text = "Фильтрация";
            this.MainMenu_filter.Click += new System.EventHandler(this.MainMenu_filter_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(248, 6);
            // 
            // MainMenu_figures
            // 
            this.MainMenu_figures.Name = "MainMenu_figures";
            this.MainMenu_figures.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.MainMenu_figures.Size = new System.Drawing.Size(251, 22);
            this.MainMenu_figures.Text = "Распознать фигуры";
            this.MainMenu_figures.Click += new System.EventHandler(this.MainMenu_figures_Click);
            // 
            // MainMenu_Script
            // 
            this.MainMenu_Script.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_Run});
            this.MainMenu_Script.Name = "MainMenu_Script";
            this.MainMenu_Script.Size = new System.Drawing.Size(59, 23);
            this.MainMenu_Script.Text = "Скрипт";
            // 
            // MainMenu_Run
            // 
            this.MainMenu_Run.Name = "MainMenu_Run";
            this.MainMenu_Run.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.MainMenu_Run.Size = new System.Drawing.Size(164, 22);
            this.MainMenu_Run.Text = "Выполнить...";
            // 
            // imgScaleComboBox
            // 
            this.imgScaleComboBox.Items.AddRange(new object[] {
            "10%",
            "25%",
            "50%",
            "100%",
            "125%",
            "150%",
            "200%"});
            this.imgScaleComboBox.Name = "imgScaleComboBox";
            this.imgScaleComboBox.Size = new System.Drawing.Size(121, 23);
            this.imgScaleComboBox.SelectedIndexChanged += new System.EventHandler(this.imgScaleComboBox_SelectedIndexChanged);
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
            this.FlowPanel.Location = new System.Drawing.Point(0, 27);
            this.FlowPanel.Name = "FlowPanel";
            this.FlowPanel.Size = new System.Drawing.Size(560, 344);
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
        public System.Windows.Forms.PictureBox MainCanvas;
        private System.Windows.Forms.OpenFileDialog OpenDialog;
        private System.Windows.Forms.SaveFileDialog SaveAsDialog;
        private System.Windows.Forms.FlowLayoutPanel FlowPanel;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Image;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_tonegative;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_tograyscale;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_tobinary;
        private System.Windows.Forms.ToolStripSeparator MainMenu_sep1;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_bri_con;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_filter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_figures;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_lincor;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_power;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripComboBox imgScaleComboBox;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_undoall;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_undo;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_redo;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_redoall;
    }
}

