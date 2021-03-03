using System;
using System.Windows.Forms;
using static _4Pic.src.DoHnd;
using static _4Pic.src.TBitmap;

namespace _4Pic.src
{
    public partial class PowerForm : Form
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



        public PowerForm(Form owner, TBitmap image) {
            InitializeComponent();
            this.Owner = owner;
            this.srcimage = image;
            on_change(track_k.Value, track_c.Value);
        }

        private void track_c_Scroll(object sender, EventArgs e) {
            on_change(track_k.Value, track_c.Value);
        }

        private void track_k_Scroll(object sender, EventArgs e) {
            on_change(track_k.Value, track_c.Value);
        }

        private void ud_c_ValueChanged(object sender, EventArgs e) {
            on_change((int)ud_k.Value, (int)ud_c.Value);
        }

        private void ud_k_ValueChanged(object sender, EventArgs e) {
            on_change((int)ud_k.Value, (int)ud_c.Value);
        }



        private void on_change(int k, int c) {
            ud_k.Value = track_k.Value = k;
            ud_c.Value = track_c.Value = c;

            TBitmap im = srcimage.clone();
            TPowerArg arg = new TPowerArg(k, c);

            image = im.do_image(yuv_fromrgb, imIter, true)
                .do_image(power, imIter, arg, true)
                .do_image(rgb_fromyuv, imIter, true);
        }
    }
}
