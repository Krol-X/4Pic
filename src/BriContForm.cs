using System;
using System.Drawing;
using System.Windows.Forms;
using static _4Pic.src.DoHnd;
using static _4Pic.src.TBitmap;

namespace _4Pic.src
{
    public partial class BriConForm : Form
    {
        private TBitmap srcimage, curimage;
        public TBitmap image
        {
            get { return curimage; }
            set {
                curimage = value;
                ((MainForm)Owner).MainCanvas.Image = curimage.toBitmap();
            }
        }

        private static Color COLOR_BACK = Color.Black;
        private static SolidBrush BRUSH_H = new SolidBrush(Color.LightGray);



        public BriConForm(Form owner, TBitmap image) {
            InitializeComponent();
            this.Owner = owner;
            this.srcimage = image;
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

            bool USE_HSB = DoHnd.USE_HSB;

            TBitmap im = srcimage.clone();
            if (USE_HSB) {
                im.do_image(hsb_fromrgb, imIter, true);

                // Brightness
                im.do_image(brightness_hsb, imIter, (double)bri/256, true);

                // Contrast
                double c = con / 256;
                im.do_image(contrast_hsb, imIter, c, true)
                    .do_image(rgb_fromhsb, imIter, true);

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
            MinMax<double> mm = new MinMax<double>() {
                min = double.MaxValue
            };

            im.do_image(hist_clear, hIter, true);
            if (USE_HSB) {
                im.do_image(hist_hue_hsb, imIter, true);
            } else {
                im.do_image(hist_hue, imIter, true);
            }
            im.do_image(hist_minmax, hIter, mm);

            int HIST_COLW = HistCanvas.Width / 256;
            int HIST_COLH = HistCanvas.Height;
            var h_im = new Bitmap(HIST_COLW * 256, HIST_COLH);
            var g = Graphics.FromImage(h_im);
            var spec = new TDrawSpec(mm, HIST_COLW, HIST_COLH, BRUSH_H, g);

            g.Clear(COLOR_BACK);
            im.do_image(hist_draw, hIter, spec);

            HistCanvas.Image = h_im;
        }
    }
}
