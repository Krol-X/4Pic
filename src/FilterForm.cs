using System;
using System.IO;
using System.Windows.Forms;

namespace _4Pic.src
{
    public partial class FilterForm : Form
    {
        private TBitmap srcimage;
        public TBitmap image;

        private int filter_type;
        private double[,] mat;

        #region Filter

        public FilterForm(Form owner, TBitmap image) {
            InitializeComponent();
            this.Owner = owner;
            this.srcimage = image;
        }

        private void button_ok_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
        }
        private void button_cancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }

        private void button_open_Click(object sender, EventArgs e) {
            if (dialog_open.ShowDialog() == DialogResult.OK) {
                try {
                    var txt = File.ReadAllLines(dialog_open.FileName);
                    textbox.Lines = txt;
                } catch {
                    MessageBox.Show("Невозможно открыть файл!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_save_Click(object sender, EventArgs e) {
            if (dialog_save.ShowDialog() == DialogResult.OK) {
                try {
                    File.WriteAllLines(dialog_save.FileName, textbox.Lines);
                } catch {
                    MessageBox.Show("Невозможно сохранить файл!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_preview_Click(object sender, EventArgs e) {
            // ...
            ((MainForm)Owner).MainCanvas.Image = image.toBitmap();
        }

        #endregion


    }
}
