using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace _4Pic.src
{
    public class TBitmap
    {
        public delegate void do_iyhnd(ref byte[] rgb, ref int[] yuv, int i);
        public delegate void do_h1hnd(ref double[,] hist, ref double[,] d1, int i);
		public delegate void do_iyhhnd(ref byte[] rgb, ref int[] yuv, ref double[,] hist, int i);
		public delegate void do_hnd(ref byte[] rgb, ref int[] yuv, ref double[,] hist,
		                            ref double[,] d1, int i);

        private const double Kr = 0.299, Kb = 0.114;
        private const double Kg = 1 - Kr - Kb;
        static bool BRI_WIKI = false;

        protected byte[] image;
        protected int[] yuv;
        protected double[,] hist;
        protected double[,] hist_d1;
        protected int width, height;
        protected int count;
        protected const int pixel_size = 4;

        #region TBitmap

        public TBitmap(Bitmap src) {
			width = src.Width; height = src.Height;
			count = width * height * pixel_size;
			var data = src.LockBits(new Rectangle(0, 0, width, height),
                                    ImageLockMode.ReadOnly,
                                    PixelFormat.Format32bppArgb);

            image = new byte[count];
            yuv = new int[count];
            hist = new double[4, 256];
            //hist_d1 = new double[4, 256];

            Marshal.Copy(data.Scan0, image, 0, count);
			src.UnlockBits(data);
        }

        public TBitmap(TBitmap src, bool upd_yuv=false, bool upd_hist=false) {       
            width = src.width;
            height = src.height;
            count = src.count;
            image = (byte[])src.image.Clone();
            yuv = new int[count];
            hist = new double[4, 256];
            //hist_d1 = new double[4, 256];
            if (upd_yuv) update_yuv();
            if (upd_hist) update_hist();
        }
		
		public Bitmap toBitmap() {
			Bitmap result = new Bitmap(width, height);
            var data = result.LockBits(new Rectangle(0, 0, width, height),
                                    ImageLockMode.WriteOnly,
                                    PixelFormat.Format32bppArgb);
            Marshal.Copy(image, 0, data.Scan0, count);
            result.UnlockBits(data);
            return result;
        }

        #endregion

        #region Update YUV

        public void update_yuv() {
            do_image(calc_yuv);
        }

        private static void calc_yuv(ref byte[] rgb, ref int[] yuv, int i) {
            double r = rgb[i + 0], g = rgb[i + 1], b = rgb[i + 2];
            if (BRI_WIKI) {
                double y = Kr * r + Kg * g + Kb * b;
                yuv[i + 0] = (int)y;
                yuv[i + 1] = (int)(b - y);
                yuv[i + 2] = (int)(r - y);
            } else {
                yuv[i + 0] = (int)(0.299 * r + 0.587 * g + 0.114 * b);
                yuv[i + 1] = (int)(-0.147 * r - 0.289 * g + 0.436 * b);
                yuv[i + 2] = (int)(0.615 * r - 0.515 * g - 0.100 * b);
            }
        }

        #endregion

        #region Update RGB

        public void update_rgb() {
            do_image(calc_rgb);
        }

        private static void calc_rgb(ref byte[] rgb, ref int[] yuv, int i) {
            double y = yuv[i + 0], u = yuv[i + 1], v = yuv[i + 2];
            if (BRI_WIKI) {
                rgb[i + 0] = Tools.FixByte(y + v);
                rgb[i + 1] = Tools.FixByte((y - Kr * v + Kb * u) / Kg);
                rgb[i + 2] = Tools.FixByte(y + u);
            } else {
                rgb[i + 0] = Tools.FixByte(y + 1.140 * v);
                rgb[i + 1] = Tools.FixByte(y - 0.394 * u - 0.581 * v);
                rgb[i + 2] = Tools.FixByte(y + 2.032 * u);
            }
        }

        #endregion

        #region Update Histogram

        private static void calc_hist_rgb(ref byte[] rgb, ref int[] yuv, ref double[,] hist, int i) {
            hist[0, rgb[i + 0]]++;
            hist[1, rgb[i + 1]]++;
            hist[2, rgb[i + 2]]++;
        }

        private static void calc_hist_y(ref byte[] rgb, ref int[] yuv, ref double[,] hist, int i) {
            hist[3, yuv[i + 0]]++;
        }

        private static void calc_hist_d1(ref double[,] hist, ref double[,] d1, int i) {
            if (i == 0) {
                d1[0, 0] = d1[1, 0] = d1[2, 0] = d1[3, 0] = 0;
            } else {
                //hist_d1[0, i] = src[0, i] - src[0, i - 1];
                //d1[1, i] = src[1, i] - src[1, i - 1];
                //d1[2, i] = src[2, i] - src[2, i - 1];
                d1[3, i] = hist[3, i] - hist[3, i - 1];
            }
        }

        private void calc_hist(ref double[,] hist, ref double[,] d1, int i) {
            //src[0, i] /= (double)count;
            //src[1, i] /= (double)count;
            //src[2, i] /= (double)count;
            hist[3, i] /= (double)count;
        }

        public void update_hist() {
            do_image(calc_hist_y);
            //do_image(hnd_calc_hist_rgb);
            do_image(calc_hist);
            //do_hist(false, calc_hist_d1);
        }

        #endregion

        #region Do-functions

        public TBitmap do_image(do_iyhnd f, bool use_parallel = false) {
            if (use_parallel) {
                throw new NotImplementedException();
            } else {
                for (int i = 0; i < count; i += pixel_size) {
                    f(ref image, ref yuv, i);
                }
            }
            return this;
        }

        public TBitmap do_image(do_h1hnd f, bool use_parallel = false) {
            if (use_parallel) {
                throw new NotImplementedException();
            } else {
                for (int i = 0; i < 256; i++) {
                    f(ref hist, ref hist_d1, i);
                }
            }
            return this;
        }
		
		public TBitmap do_image(do_iyhhnd f, bool use_parallel = false) {
            if (use_parallel) {
                throw new NotImplementedException();
            } else {
                for (int i = 0; i < count; i += pixel_size) {
                    f(ref image, ref yuv, ref hist, i);
                }
            }
            return this;
        }
		
		public TBitmap do_image(do_hnd f, bool use_parallel = false) {
            if (use_parallel) {
                throw new NotImplementedException();
            } else {
                for (int i = 0; i < count; i += pixel_size) {
                    f(ref image, ref yuv, ref hist, ref hist_d1, i);
                }
            }
            return this;
        }

        #endregion
    }
}
