using System;
using System.Windows.Forms;
using static _4Pic.src.DoHnd;
using static _4Pic.src.TBitmap;

namespace _4Pic.src
{
    public partial class BinForm : Form
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



        public BinForm(Form owner, TBitmap image) {
            InitializeComponent();
            this.Owner = owner;
            this.srcimage = image;
            change(track_thr.Value, checkbox_adaptive.Checked);
        }

        private void track_thr_Scroll(object sender, EventArgs e) {
            change(track_thr.Value, checkbox_adaptive.Checked);
        }

        private void ud_bin_ValueChanged(object sender, EventArgs e) {
            change((int)ud_bin.Value, checkbox_adaptive.Checked);
        }

        private void change(int threshold, bool adaptive) {
            ud_bin.Value = track_thr.Value = threshold;

            var im = srcimage.clone();
            if (adaptive) {
                throw new NotImplementedException();
            } else {
                if (DoHnd.USE_HSB) {
                    image = im.do_image(hsb_fromrgb, imIter, true)
                        .do_image(binary_hsb, imIter, threshold, true);
                } else {
                    image = im.do_image(yuv_fromrgb, imIter, true)
                        .do_image(binary, imIter, threshold, true);
                }
            }
        }
    }
}
