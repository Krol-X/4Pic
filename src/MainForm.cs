using System;
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
        static double[] IMG_SCALES = { 0.10, 0.25, 0.50, 1.00, 1.25, 1.50, 2.00 };
        static int DEFAULT_SCALE = 3;
        static string FILTER_WILDCARDS = "Поддерживаемые файлы|*.bmp;*.png;*.jpg;*.jpeg|Bitmap-файлы|*.bmp|PNG-файлы|*.png|JPEG-файлы|*.jpg;*.jpeg|Все файлы|*.*";
        PictureBoxAdapter adapter;



        #region MainForm

        public MainForm()
        {
            InitializeComponent();
            OpenDialog.Filter = SaveAsDialog.Filter = FILTER_WILDCARDS;
            FlowPanel.AutoScroll = true;
            adapter = new PictureBoxAdapter(MainCanvas);
            adapter.onDraw = MainMenu_update;
            setScale(DEFAULT_SCALE);
        }

        private void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {
            int hb_h = SystemInformation.HorizontalScrollBarHeight,
                hb_w = SystemInformation.VerticalScrollBarWidth;
            Point xy = Cursor.Position,
                  area_xy = FlowPanel.PointToScreen(Point.Empty);
            int h = FlowPanel.Height - hb_h;
            Rectangle rect = new Rectangle(
                area_xy.X, area_xy.Y + h,
                area_xy.X + FlowPanel.Width, area_xy.Y + FlowPanel.Height
            );
            if (rect.Contains(xy))
            {
                var hprop = FlowPanel.HorizontalScroll;
                int x = hprop.Value + (e.Delta > 0 ? -1 : 1) * hprop.SmallChange * 10;
                FlowPanel.AutoScrollPosition = new Point(x, FlowPanel.VerticalScroll.Value);
            }
            else
            {
                var vprop = FlowPanel.VerticalScroll;
                int y = vprop.Value + (e.Delta > 0 ? -1 : 1) * vprop.SmallChange * 10;
                FlowPanel.AutoScrollPosition = new Point(FlowPanel.HorizontalScroll.Value, y);
            }
        }

        void MainMenu_update()
        {
            bool enabled = adapter.HistSize > 0;
            MainMenu_SaveAs.Enabled = enabled;
            MainMenu_Image.Enabled = enabled;
            MainMenu_tonegative.Enabled = enabled;
            MainMenu_tograyscale.Enabled = enabled;
            MainMenu_tobinary.Enabled = enabled;
            MainMenu_bri_con.Enabled = enabled;
            MainMenu_filter.Enabled = enabled;
            MainMenu_figures.Enabled = enabled;
            MainMenu_Script.Enabled = enabled;
            imgScaleComboBox.Enabled = enabled;

            var is_first = adapter.CurrentIsFirst();
            var is_last = adapter.CurrentIsLast();
            MainMenu_undoall.Enabled = !is_first;
            MainMenu_undo.Enabled = !is_first;
            MainMenu_redo.Enabled = !is_last;
            MainMenu_redoall.Enabled = !is_last;
        }

        #endregion

        #region MainMenu_File Handlers

        private void MainMenu_Open_Click(object sender, EventArgs e)
        {
            OpenDialog.ShowDialog();
        }

        private void MainMenu_SaveAs_Click(object sender, EventArgs e)
        {
            SaveAsDialog.ShowDialog();
        }

        private void MainMenu_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        ///////////////////////////////////////////////////////////////////////

        private void OpenDialog_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                Image im = Image.FromFile(OpenDialog.FileName);
                adapter.add((Bitmap)im);
            }
            catch
            {
                MessageBox.Show("Невозможно открыть файл!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAsDialog_FileOk(object sender, CancelEventArgs e)
        {
            var str = SaveAsDialog.FileName;
            adapter.SaveImage(str);
        }

        #endregion

        #region MainMenu_Image Handlers

        private void MainMenu_undoall_Click(object sender, EventArgs e)
        {
            adapter.UndoAll();
        }
        private void MainMenu_undo_Click(object sender, EventArgs e)
        {
            adapter.Undo();
        }
        private void MainMenu_redo_Click(object sender, EventArgs e)
        {
            adapter.Redo();
        }

        private void MainMenu_redoall_Click(object sender, EventArgs e)
        {
            adapter.RedoAll();
        }

        private void MainMenu_tonegative_Click(object sender, EventArgs e)
        {
            adapter.add_new()
                .do_image(negative, imIter, true);
            adapter.DrawCurrent();
        }

        private void MainMenu_tograyscale_Click(object sender, EventArgs e)
        {
            if (DoHnd.USE_HSV)
            {
                adapter.add_new()
                    .do_image(hsv_fromrgb, imIter, true)
                    .do_image(grayscale_hsv, imIter);
            }
            else
            {
                adapter.add_new()
                    .do_image(yuv_fromrgb, imIter, true)
                    .do_image(grayscale, imIter);
            }
            adapter.DrawCurrent();
        }

        private void MainMenu_tobinary_Click(object sender, EventArgs e)
        {
            BinDialogForm form = new BinDialogForm(this, adapter);
            if (form.ShowDialog() == DialogResult.OK)
                adapter.add(form.image);
            else
                adapter.DrawCurrent();
        }

        private void MainMenu_bri_con_Click(object sender, EventArgs e)
        {
            BriConDialogForm form = new BriConDialogForm(this, adapter);
            if (form.ShowDialog() == DialogResult.OK)
                adapter.add(form.image);
            else
                adapter.DrawCurrent();
        }

        private void MainMenu_power_Click(object sender, EventArgs e)
        {
            PowerDialogForm form = new PowerDialogForm(this, adapter);
            if (form.ShowDialog() == DialogResult.OK)
                adapter.add(form.image);
            else
                adapter.DrawCurrent();
        }

        private void MainMenu_lincor_Click(object sender, EventArgs e)
        {
            MinMax<double> mm = new MinMax<double>()
            {
                min = double.MaxValue
            };
            var image = adapter.Current.clone();

            Tools.CalcHist(image, true);
            image.do_image(hist_minmax, hIter, mm);
            if (USE_HSV)
            {
                throw new NotImplementedException();
            }
            else
            {
                image.do_image(linear_correction, imIter, mm, true)
                    .do_image(rgb_fromyuv, imIter, true);
            }
            adapter.add(image);
        }

        private void MainMenu_filter_Click(object sender, EventArgs e)
        {
            FilterDialogForm form = new FilterDialogForm(this, adapter);
            if (form.ShowDialog() == DialogResult.OK)
                adapter.add(form.image);
            else
                adapter.DrawCurrent();
        }

        private void MainMenu_figures_Click(object sender, EventArgs e)
        {
            var new_im = DoCV.RecognizeFigures(adapter.Current);
            adapter.add(new_im);
        }

        private void imgScaleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setScale(imgScaleComboBox.SelectedIndex);
        }

        #endregion

        #region Helpers

        private void setScale(int index)
        {
            imgScaleComboBox.SelectedIndex = index;
            var scale = IMG_SCALES[index];
            adapter.Scale = scale;
            adapter.DrawCurrent();
        }

        #endregion
    }
}
