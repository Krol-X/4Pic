using System;
using System.Linq;
using System.Windows.Forms;
using static _4Pic.src.DoHnd;
using static _4Pic.src.TBitmap;

namespace _4Pic.src
{
    public partial class BinForm : Form {
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
            change(GetOtsu(srcimage));
        }

        private void track_thr_Scroll(object sender, EventArgs e) {
            change(track_thr.Value);
        }

        private void ud_bin_ValueChanged(object sender, EventArgs e) {
            change((int)ud_bin.Value);
        }

        private void change(int threshold) {
            ud_bin.Value = track_thr.Value = threshold;

            var im = srcimage.clone();
            if (DoHnd.USE_HSV) {
                image = im.do_image(hsv_fromrgb, imIter, true)
                    .do_image(binary_hsv, imIter, threshold, true);
            } else {
                image = im.do_image(yuv_fromrgb, imIter, true)
                    .do_image(binary, imIter, threshold, true);
            }
        }

        public class TOtsuArg
        {
            public double m1 = 0, m2 = 0, nt = 0, mt = 0, q;
            public double[] disp;

            public TOtsuArg(double hist0) {
                q = hist0;
                disp = new double[256];
            }
        }

        private byte GetOtsu(TBitmap im) {
            im.do_image(yuv_fromrgb, hIter, true)
                .do_image(hist_clear, hIter, true)
                .do_image(hist_hue, hIter, true)
                .do_image(hist_divide, hIter, true);
            TOtsuArg otsu = new TOtsuArg(im.hist[0, H]);

            im.do_image(binary_otsu1, hIter, otsu, true);
            otsu.mt /= otsu.nt;
            im.do_image(binary_otsu2, hIter, otsu, true);
            var lst = otsu.disp.ToList();
            return (byte)lst.FindIndex(x => x == lst.Max());
        }
    }
}
