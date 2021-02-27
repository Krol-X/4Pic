using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

using _4Pic.src;
using static _4Pic.src.DoHnd;
using static _4Pic.src.TBitmap;


namespace _4Pic
{
    public partial class MainForm : Form
    {
        static string FILTER_WILDCARDS = "Поддерживаемые файлы|*.bmp;*.png;*.jpg;*.jpeg|Bitmap-файлы|*.bmp|PNG-файлы|*.png|JPEG-файлы|*.jpg;*.jpeg|Все файлы|*.*";
        TBitmap image = null;

        #region MainForm

        public MainForm() {
            InitializeComponent();
            OpenDialog.Filter = SaveAsDialog.Filter = FILTER_WILDCARDS;
            FlowPanel.AutoScroll = true;
            MainMenu_update();
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


        void MainMenu_update() {
            bool enabled = image != null;
            MainMenu_SaveAs.Enabled = enabled;
            MainMenu_Image.Enabled = enabled;
            MainMenu_tonegative.Enabled = enabled;
            MainMenu_tograyscale.Enabled = enabled;
            MainMenu_tobinary.Enabled = enabled;
            MainMenu_bri_con.Enabled = enabled;
            MainMenu_filter.Enabled = enabled;
            MainMenu_Script.Enabled = enabled;
        }

        #endregion

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

        ///////////////////////////////////////////////////////////////////////

        private void OpenDialog_FileOk(object sender, CancelEventArgs e) {
            try {
                Image im = Image.FromFile(OpenDialog.FileName);
                image = new TBitmap((Bitmap)im);
                MainCanvas.Image = im;
            } catch {
                MessageBox.Show("Невозможно открыть файл!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MainMenu_update();
        }

        private void SaveAsDialog_FileOk(object sender, CancelEventArgs e) {
            var str = SaveAsDialog.FileName;
            MainCanvas.Image.Save(str, Tools.ParseImageFormat(Path.GetExtension(str).Substring(1)));
        }

        #endregion

        #region MainMenu_Image Handlers

        private void MainMenu_tonegative_Click(object sender, EventArgs e) {
            if (MainCanvas.Image != null) {
                MainCanvas.Image = image.do_image(negative, imIter, true).toBitmap();
            }
        }

        private void MainMenu_tograyscale_Click(object sender, EventArgs e) {
            if (MainCanvas.Image != null) {
                MainCanvas.Image = image
                    .do_image(yuv_fromrgb, imIter, true)
                    .do_image(grayscale, imIter)
                    .toBitmap();
            }
        }

        private void MainMenu_tobinary_Click(object sender, EventArgs e) {
            BinForm form = new BinForm(this, image);
            if (form.ShowDialog() == DialogResult.OK) {
                image = form.image;
            } else {
                MainCanvas.Image = image.toBitmap();
            }
        }

        private void MainMenu_bri_con_Click(object sender, EventArgs e) {
            BriConForm form = new BriConForm(this, image);
            if (form.ShowDialog() == DialogResult.OK) {
                image = form.image;
            } else {
                MainCanvas.Image = image.toBitmap();
            }
        }

        private void MainMenu_filter_Click(object sender, EventArgs e) {
            FilterForm form = new FilterForm(this, image);
            if (form.ShowDialog() == DialogResult.OK) {
                image = form.image;
            } else {
                MainCanvas.Image = image.toBitmap();
            }
        }

        private void MainMenu_figures_Click(object sender, EventArgs e) {
            var new_im = DoCV.RecognizeFigures(image.toBitmap());
            image = new TBitmap(new_im);
            MainCanvas.Image = new_im;
        }

        #endregion
    }
}
