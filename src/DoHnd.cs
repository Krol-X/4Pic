using System;
using System.Collections.Generic;
using static _4Pic.src.BinForm;
using static _4Pic.src.TBitmap;

namespace _4Pic.src
{
    public static class DoHnd
    {
        static bool BRI_WIKI = false;
        public static bool USE_HSV = false;

        const int MID_GRAY = 159;
        public const double Kr = 0.299, Kb = 0.114;
        public const double Kg = 1 - Kr - Kb;

        #region TBitmap handlers

        // parallel = true
        public static void rgb_fromyuv(TBitmap im, int i) {
            var rgb = im.rgb; var yuv = im.yuv; i <<= 2;
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

        // parallel = true
        public static void yuv_fromrgb(TBitmap im, int i) {
            var rgb = im.rgb; var yuv = im.yuv; i <<= 2;
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

        private enum hsv_ : int { V, Vmin, Vdec, Vinc };
        private readonly static int[,] hsv_rgb = {
            {(int)hsv_.V,    (int)hsv_.Vinc, (int)hsv_.Vmin},
            {(int)hsv_.Vdec, (int)hsv_.V,    (int)hsv_.Vmin},
            {(int)hsv_.Vmin, (int)hsv_.V,    (int)hsv_.Vinc},
            {(int)hsv_.Vmin, (int)hsv_.Vdec, (int)hsv_.V},
            {(int)hsv_.Vinc, (int)hsv_.Vmin, (int)hsv_.V},
            {(int)hsv_.V,    (int)hsv_.Vmin, (int)hsv_.Vdec}
        };

        // parallel = true
        public static void rgb_fromhsv(TBitmap im, int i) {
            var rgb = im.rgb; var hsv = im.hsv; i <<= 2;
            double h = hsv[i + 0], s = hsv[i + 1] / 255, b = hsv[i + 2];
            int Hi = (int)(h / 60 % 6);
            var Vmin = (100 - s) * b / 100;
            var a = (b - Vmin) * (h % 60) / 60;
            var Vinc = Vmin + a;
            var Vdec = b - a;
            byte[] result = { (byte)b, (byte)Vmin, (byte)Vdec, (byte)Vinc };
            rgb[i + 0] = result[hsv_rgb[Hi, 0]];
            rgb[i + 1] = result[hsv_rgb[Hi, 1]];
            rgb[i + 2] = result[hsv_rgb[Hi, 2]];
        }

        // parallel = true
        public static void hsv_fromrgb(TBitmap im, int i) {
            var rgb = im.rgb; var hsv = im.hsv; i <<= 2;
            int r = rgb[i + 0], g = rgb[i + 1], b = rgb[i + 2];
            var min = Tools.Min(r, g, b);
            var max = Tools.Max(r, g, b);
            double h = 0;
            if (min != max) {
                if (max == r) {
                    h = 60 * (g - b) / (max - min) + ((g < b) ? 360 : 0);
                } else if (max == g) {
                    h = 60 * (b - r) / (max - min) + 120;
                } else {
                    h = 60 * (r - g) / (max - min) + 240;
                }
            }
            hsv[i + 0] = h;
            hsv[i + 1] = (max == 0) ? 0 : 1 - min / max;
            hsv[i + 2] = max;
        }

        // parallel = true
        public static void hist_clear(TBitmap im, int i) {
            var hist = im.hist;
            hist[i, 0] = hist[i, 1] = hist[i, 2] = hist[i, 3] = 0;
        }

        // parallel = true
        public static void hist_rgb(TBitmap im, int i) {
            var rgb = im.rgb; var hist = im.hist; i <<= 2;
            hist[rgb[i + 0], R]++;
            hist[rgb[i + 1], G]++;
            hist[rgb[i + 2], B]++;
        }

        // parallel = true
        public static void hist_hue(TBitmap im, int i) {
            i <<= 2;
            im.hist[im.yuv[i + 0], H]++;
        }
        public static void hist_hue_hsv(TBitmap im, int i) {
            i <<= 2;
            im.hist[(int)im.hsv[i + 2], H]++;
        }

        public static void hist_divide(TBitmap im, int i) {
            var hist = im.hist;
            var count = im.byte_count;

            hist[i, R] /= (double)count;
            hist[i, G] /= (double)count;
            hist[i, B] /= (double)count;
            hist[i, H] /= (double)count;
        }

        // parallel = false
        public static void hist_minmax(TBitmap im, int i, MinMax<double> mm) {
            var hist = im.hist;
            if (hist[i, H] < mm.min) {
                mm.min_i = i; mm.min = hist[i, H];
            }
            if (hist[i, H] > mm.max) {
                mm.max_i = i; mm.max = hist[i, H];
            }
        }

        #endregion

        #region Basic operations

        // parallel = true
        public static void negative(TBitmap im, int i) {
            var rgb = im.rgb; i <<= 2;
            rgb[i + 0] ^= 0xFF;
            rgb[i + 1] ^= 0xFF;
            rgb[i + 2] ^= 0xFF;
        }

        // parallel = true
        public static void grayscale(TBitmap im, int i) {
            var rgb = im.rgb; i <<= 2;
            rgb[i + 0] = rgb[i + 1] = rgb[i + 2] = (byte)im.yuv[i + 0];
        }
        public static void grayscale_hsv(TBitmap im, int i) {
            var rgb = im.rgb; i <<= 2;
            rgb[i + 0] = rgb[i + 1] = rgb[i + 2] = (byte)im.hsv[i + 2];
        }

        public class TPowerArg
        {
            public double c, k;

            public TPowerArg(int k, int c) {
                this.k = k;
                this.c = c;
            }
        }
        
        // parallel = true
        public static void power(TBitmap im, int i, TPowerArg arg) {
            var yuv = im.yuv; i <<= 2;
            yuv[i + 0] = Tools.FixByte(arg.c * Math.Pow(yuv[i + 0], arg.k));
        }

        #endregion

        #region Binary handlers

        // parallel = true
        public static void binary(TBitmap im, int i, int thr) {
            var rgb = im.rgb; i <<= 2;
            rgb[i + 0] = rgb[i + 1] = rgb[i + 2] =
                (byte)((im.yuv[i + 0] < (int)thr) ? 255 : 0);
        }
        public static void binary_hsv(TBitmap im, int i, int thr) {
            var rgb = im.rgb; i <<= 2;
            rgb[i + 0] = rgb[i + 1] = rgb[i + 2] =
                (byte)((im.hsv[i + 2] < (int)thr) ? 255 : 0);
        }

        public static void binary_otsu1(TBitmap im, int i, TOtsuArg otsu) {
            var h = im.hist[i, H];
            otsu.nt += h;
            otsu.mt += h * i;
        }

        public static void binary_otsu2(TBitmap im, int i, TOtsuArg otsu) {
            if (i == 0) return;
            var h = im.hist[i, H];
            otsu.m1 = otsu.q * otsu.m1 + i * h;
            otsu.q += h;
            if (otsu.q == 0 || otsu.q == 1) return;
            double nq = 1 - otsu.q, nm = otsu.m1 - otsu.m2;
            otsu.m1 /= otsu.q;
            otsu.m2 = (otsu.mt - otsu.q * otsu.m1) / nq;
            otsu.disp[i] = Math.Sqrt(otsu.q * nq * nm * nm);
        }

        #endregion

        #region Brightness, contrast handlers

        // parallel = true
        public static void brightness(TBitmap im, int i, int bri) {
            var yuv = im.yuv; var hsv = im.hsv; i <<= 2;
            yuv[i + 0] = Tools.FixByte(yuv[i + 0] + bri);
        }
        public static void brightness_hsv(TBitmap im, int i, double bri) {
            var yuv = im.yuv; var hsv = im.hsv; i <<= 2;
            hsv[i + 2] = hsv[i + 2] + bri;
        }

        // parallel = true
        public static void contrast(TBitmap im, int i, double con) {
            var rgb = im.rgb; i <<= 2;
            rgb[i + 0] = Tools.FixByte(con * (rgb[i + 0] - MID_GRAY) + MID_GRAY);
            rgb[i + 1] = Tools.FixByte(con * (rgb[i + 1] - MID_GRAY) + MID_GRAY);
            rgb[i + 2] = Tools.FixByte(con * (rgb[i + 2] - MID_GRAY) + MID_GRAY);
        }
        public static void contrast_hsv(TBitmap im, int i, double con) {
            var hsv = im.hsv; i <<= 2;
            hsv[i + 1] = hsv[i + 1] + con;
        }

        public static void linear_correction(TBitmap im, int i, MinMax<double> mm) {
            var yuv = im.yuv;
            yuv[i + 0] = (int)((yuv[i + 0] - mm.min) * 255.0 / (mm.max - mm.min));
        }

        #endregion

        #region Filtration handlers

        public class TFilterData
        {
            public TBitmap src;
            public double[] mat;
            public int width, height;
            public double sum;
        }

        // parallel = true
        public static void filter_simple(TBitmap im, int i, TFilterData data) {
            var rgb = data.src.rgb;
            int w = im.width, h = im.height;
            int iy = i / w, ix = i % w;
            int dw = data.width >> 1, dh = data.height >> 1;
            int cou = 0;
            double r = 0, g = 0, b = 0;

            for (int y = -dh; y <= dh; y++) {
                int yy = iy + y;
                if (yy < 0) continue;
                if (yy >= h) break;

                for (int x = -dw; x <= dw; x++) {
                    int xx = ix + x;
                    if (xx < 0) continue;
                    if (xx >= w) break;
                    int ii = (yy * w + xx) << 2;

                    double koef = data.mat[cou++];
                    r += koef * rgb[ii + 0];
                    g += koef * rgb[ii + 1];
                    b += koef * rgb[ii + 2];
                }
            }
            i <<= 2;
            im.rgb[i + 0] = Tools.FixByte(r);
            im.rgb[i + 1] = Tools.FixByte(g);
            im.rgb[i + 2] = Tools.FixByte(b);
        }

        // parallel = true
        public static void filter_average(TBitmap im, int i, TFilterData data) {
            var rgb = data.src.rgb;
            int w = im.width, h = im.height;
            int iy = i / w, ix = i % w;
            int dw = data.width >> 1, dh = data.height >> 1;
            int cou = 0;
            double r = 0, g = 0, b = 0;

            for (int y = -dh; y <= dh; y++) {
                int yy = iy + y;
                if (yy < 0) continue;
                if (yy >= h) break; 

                for (int x = -dw; x <= dw; x++) {
                    int xx = ix + x;
                    if (xx < 0) continue;
                    if (xx >= w) break;
                    int ii = (yy * w + xx) << 2;

                    double koef = data.mat[cou++];
                    r += koef * rgb[ii + 0];
                    g += koef * rgb[ii + 1];
                    b += koef * rgb[ii + 2];
                }
            }
            i <<= 2;
            im.rgb[i + 0] = Tools.FixByte(r / data.sum);
            im.rgb[i + 1] = Tools.FixByte(g / data.sum);
            im.rgb[i + 2] = Tools.FixByte(b / data.sum);
        }

        // parallel = true
        public static void filter_median(TBitmap im, int i, TFilterData data) {
            var rgb = data.src.rgb;
            int w = im.width, h = im.height;
            int iy = i / w, ix = i % w;
            int dw = data.width >> 1, dh = data.height >> 1;
            int cou = 0;
            var r = new List<double>();
            var g = new List<double>();
            var b = new List<double>();

            for (int y = -dh; y <= dh; y++) {
                int yy = iy + y;
                if (yy < 0 || yy >= h) {
                    r.Add(0); g.Add(0); b.Add(0);
                    continue;
                }

                for (int x = -dw; x <= dw; x++) {
                    int xx = ix + x;
                    if (xx < 0 || xx >= w) {
                        r.Add(0); g.Add(0); b.Add(0);
                        continue;
                    }
                    int ii = (yy * w + xx) << 2;

                    double koef = data.mat[cou++];
                    r.Add(koef * rgb[ii + 0]);
                    g.Add(koef * rgb[ii + 1]);
                    b.Add(koef * rgb[ii + 2]);
                }
            }
            r.Sort(); g.Sort(); b.Sort();
            i <<= 2;
            int med = dw + data.width * dh;
            im.rgb[i + 0] = Tools.FixByte(r[med]);
            im.rgb[i + 1] = Tools.FixByte(g[med]);
            im.rgb[i + 2] = Tools.FixByte(b[med]);
        }

        #endregion
    }
}
