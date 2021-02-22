using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using _4Pic.src;

namespace _4Pic
{
    public partial class MainForm : Form
    {
        private static string FILTER_WILDCARDS = "Поддерживаемые файлы|*.bmp;*.png;*.jpg;*.jpeg|Bitmap-файлы|*.bmp|PNG-файлы|*.png|JPEG-файлы|*.jpg;*.jpeg|Все файлы|*.*";
        Image image;

        public MainForm() {
            InitializeComponent();
            OpenDialog.Filter = SaveAsDialog.Filter = FILTER_WILDCARDS;
            FlowPanel.AutoScroll = true;
        }

        private void MainForm_MouseWheel(object sender, MouseEventArgs e) {
            int hb_h = SystemInformation.HorizontalScrollBarHeight,
                hb_w = SystemInformation.VerticalScrollBarWidth;
            Point xy = Cursor.Position,
                  area_xy = FlowPanel.PointToScreen(Point.Empty);
            int h = FlowPanel.Height - hb_h;
            Rectangle rect = new Rectangle(
                area_xy.X, area_xy.Y + h,
                area_xy.X + FlowPanel.Width, area_xy.Y + FlowPanel.Height
            );
            if (rect.Contains(xy)) {
                var hprop = FlowPanel.HorizontalScroll;
                int x = hprop.Value + (e.Delta > 0 ? -1 : 1) * hprop.SmallChange * 10;
                FlowPanel.AutoScrollPosition = new Point(x, FlowPanel.VerticalScroll.Value);
            } else {
                var vprop = FlowPanel.VerticalScroll;
                int y = vprop.Value + (e.Delta > 0 ? -1 : 1) * vprop.SmallChange * 10;
                FlowPanel.AutoScrollPosition = new Point(FlowPanel.HorizontalScroll.Value, y);
            }
        }

  
        #region MainMenu_File Handlers

        private void MainMenu_Open_Click(object sender, EventArgs e) {
            OpenDialog.ShowDialog();
        }

        private void MainMenu_SaveAs_Click(object sender, EventArgs e) {
            SaveAsDialog.ShowDialog();
        }

        private void MainMenu_Exit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void MainMenu_Run_Click(object sender, EventArgs e) {
            //
        }

        ///////////////////////////////////////////////////////////////////////
      
        private void OpenDialog_FileOk(object sender, CancelEventArgs e) {
            try {
                var im = Image.FromFile(OpenDialog.FileName);
                image = im;
                MainCanvas.Image = image;
            } catch {
                MessageBox.Show("Невозможно открыть файл!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAsDialog_FileOk(object sender, CancelEventArgs e) {
            var str = SaveAsDialog.FileName;
            MainCanvas.Image.Save(str, Tools.ParseImageFormat(Path.GetExtension(str).Substring(1)));
        }

        #endregion


        #region MainMenu_Image Handlers

        private unsafe void MainMenu_tonegative_Click(object sender, EventArgs e) {
            if (MainCanvas.Image != null) {
                Engine.do_pixel((Bitmap)image, pix => Color.FromArgb(pix.ToArgb() ^ 0xFFFFFF));
                MainCanvas.Image = image;
            }
        }

        private unsafe void MainMenu_tograyscale_Click(object sender, EventArgs e) {
            if (MainCanvas.Image != null) {
                Engine.do_pixel((Bitmap)image, pix => {
                    byte hue = (byte)Math.Max(0, Math.Min(255, pix.R * 0.299 + pix.G * 0.587 + pix.B * 0.114));
                    return Color.FromArgb(hue, hue, hue);
                });
                MainCanvas.Image = image;
            }
        }

        private unsafe void MainMenu_tosepia_Click(object sender, EventArgs e) {

        }

        private void MainMenu_tobinary_Click(object sender, EventArgs e) {

        }

        private void MainMenu_bri_con_Click(object sender, EventArgs e) {

        }

        private void MainMenu_filter_Click(object sender, EventArgs e) {

        }

        #endregion
    }
}
