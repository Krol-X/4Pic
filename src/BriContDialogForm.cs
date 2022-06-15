using System;
using System.Drawing;
using System.Windows.Forms;
using static _4Pic.src.DoHnd;
using static _4Pic.src.TBitmap;

namespace _4Pic.src
{
    public partial class BriConDialogForm : Form
    {
        private PictureBoxAdapter adapter;
        private TBitmap curimage;
        public TBitmap image
        {
            get { return curimage; }
            set {
                curimage = value;
                adapter.Draw(curimage);
            }
        }

        private static Color COLOR_BACK = Color.Black;
        private static SolidBrush BRUSH_H = new SolidBrush(Color.LightGray);



        public BriConDialogForm(Form owner, PictureBoxAdapter adapter) {
            InitializeComponent();
            this.Owner = owner;
            this.adapter = adapter;
            on_change(track_bri.Value, track_con.Value);
        }

        private void track_bri_Scroll(object sender, EventArgs e) {
            on_change(track_bri.Value, track_con.Value);
        }
        private void track_cont_Scroll(object sender, EventArgs e) {
            on_change(track_bri.Value, track_con.Value);
        }

        private void ud_bri_ValueChanged(object sender, EventArgs e) {
            on_change((int)ud_bri.Value, (int)ud_con.Value);
        }

        private void ud_con_ValueChanged(object sender, EventArgs e) {
            on_change((int)ud_bri.Value, (int)ud_con.Value);
        }



        private void on_change(int bri, int con) {
            ud_bri.Value = track_bri.Value = bri;
            ud_con.Value = track_con.Value = con;

            bool USE_hsv = DoHnd.USE_HSV;

            TBitmap im = adapter.Current.clone();
            if (USE_hsv) {
                im.do_image(hsv_fromrgb, imIter, true);

                // Brightness
                im.do_image(brightness_hsv, imIter, (double)bri/256, true);

                // Contrast
                double c = con / 256;
                im.do_image(contrast_hsv, imIter, c, true)
                    .do_image(rgb_fromhsv, imIter, true);

            } else {
                im.do_image(yuv_fromrgb, imIter, true);

                // Brightness
                im.do_image(brightness, imIter, bri, true);
                im.do_image(rgb_fromyuv, imIter, true);

                // Contrast
                var c = 259.0 * (con + 255.0) / (255.0 * (259.0 - con));
                im.do_image(contrast, imIter, c, true)
                    .do_image(yuv_fromrgb, imIter, true);
            }

            image = im;

            // Histogram
            Tools.CalcHist(im);
            var pt = chart_hist.Series["main"].Points;
            pt.Clear();
            for (int i=0; i < 256; i++) {
                pt.AddY(im.hist[i, 3]);
            }
        }
    }
}
