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
            track_change(track_bri.Value, track_con.Value);
        }

        private void button_ok_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
        }
        private void button_cancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }

        private void track_bri_Scroll(object sender, EventArgs e) {
            track_change(track_bri.Value, track_con.Value);
        }
        private void track_cont_Scroll(object sender, EventArgs e) {
            track_change(track_bri.Value, track_con.Value);
        }



        private void track_change(int bri, int con) {
            label_bri.Text = bri.ToString();
            label_con.Text = con.ToString();

            TBitmap im = srcimage.clone();

            // Brightness
            im.do_image(brightness, imIter, bri, true)
                .do_image(rgb_fromyuv, imIter, true);

            // Contrast
            con = (int)(259.0 * (con + 255.0) / (255.0 * (259.0 - con)));
            im.do_image(contrast, imIter, con, true)
                .do_image(yuv_fromrgb, imIter, true);

            image = im;

            // Histogram
            MinMax<double> mm = new MinMax<double>();

            im.do_image(hist_clear, hIter, true)
                .do_image(hist_hue, imIter, true)
                .do_image(hist_minmax, hIter, mm);

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
