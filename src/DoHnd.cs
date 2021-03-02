using _4Pic;
using System;
using System.Collections.Generic;
using System.Drawing;
using static _4Pic.src.TBitmap;

namespace _4Pic.src
{
    public static class DoHnd
    {
        static bool BRI_WIKI = false;
        public static bool USE_HSB = false;

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

        private enum hsb_ : int { V, Vmin, Vdec, Vinc };
        private readonly static int[,] hsb_rgb = {
            {(int)hsb_.V,    (int)hsb_.Vinc, (int)hsb_.Vmin},
            {(int)hsb_.Vdec, (int)hsb_.V,    (int)hsb_.Vmin},
            {(int)hsb_.Vmin, (int)hsb_.V,    (int)hsb_.Vinc},
            {(int)hsb_.Vmin, (int)hsb_.Vdec, (int)hsb_.V},
            {(int)hsb_.Vinc, (int)hsb_.Vmin, (int)hsb_.V},
            {(int)hsb_.V,    (int)hsb_.Vmin, (int)hsb_.Vdec}
        };

        // parallel = true
        public static void rgb_fromhsb(TBitmap im, int i) {
            var rgb = im.rgb; var hsb = im.hsb; i <<= 2;
            double h = hsb[i + 0], s = hsb[i + 1] / 255, b = hsb[i + 2];
            int Hi = (int)(h / 60 % 6);
            var Vmin = (100 - s) * b / 100;
            var a = (b - Vmin) * (h % 60) / 60;
            var Vinc = Vmin + a;
            var Vdec = b - a;
            byte[] result = { (byte)b, (byte)Vmin, (byte)Vdec, (byte)Vinc };
            rgb[i + 0] = result[hsb_rgb[Hi, 0]];
            rgb[i + 1] = result[hsb_rgb[Hi, 1]];
            rgb[i + 2] = result[hsb_rgb[Hi, 2]];
        }

        // parallel = true
        public static void hsb_fromrgb(TBitmap im, int i) {
            var rgb = im.rgb; var hsb = im.hsb; i <<= 2;
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
            hsb[i + 0] = h;
            hsb[i + 1] = (max == 0) ? 0 : 1 - min / max;
            hsb[i + 2] = max;
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
        public static void hist_hue_hsb(TBitmap im, int i) {
            i <<= 2;
            im.hist[(int)im.hsb[i + 2], H]++;
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
        public static void grayscale_hsb(TBitmap im, int i) {
            var rgb = im.rgb; i <<= 2;
            rgb[i + 0] = rgb[i + 1] = rgb[i + 2] = (byte)im.hsb[i + 2];
        }

        #endregion

        #region Binary handlers

        // parallel = true
        public static void binary(TBitmap im, int i, int thr) {
            var rgb = im.rgb; i <<= 2;
            rgb[i + 0] = rgb[i + 1] = rgb[i + 2] =
                (byte)((im.yuv[i + 0] < (int)thr) ? 255 : 0);
        }
        public static void binary_hsb(TBitmap im, int i, int thr) {
            var rgb = im.rgb; i <<= 2;
            rgb[i + 0] = rgb[i + 1] = rgb[i + 2] =
                (byte)((im.hsb[i + 2] < (int)thr) ? 255 : 0);
        }

        #endregion

        #region Brightness, contrast and historam handlers

        // parallel = true
        public static void brightness(TBitmap im, int i, int bri) {
            var yuv = im.yuv; var hsb = im.hsb; i <<= 2;
            yuv[i + 0] = Tools.FixByte(yuv[i + 0] + bri);
        }
        public static void brightness_hsb(TBitmap im, int i, double bri) {
            var yuv = im.yuv; var hsb = im.hsb; i <<= 2;
            hsb[i + 2] = hsb[i + 2] + bri;
        }

        // parallel = true
        public static void contrast(TBitmap im, int i, double con) {
            var rgb = im.rgb; i <<= 2;
            rgb[i + 0] = Tools.FixByte(con * (rgb[i + 0] - MID_GRAY) + MID_GRAY);
            rgb[i + 1] = Tools.FixByte(con * (rgb[i + 1] - MID_GRAY) + MID_GRAY);
            rgb[i + 2] = Tools.FixByte(con * (rgb[i + 2] - MID_GRAY) + MID_GRAY);
        }
        public static void contrast_hsb(TBitmap im, int i, double con) {
            var hsb = im.hsb; i <<= 2;
            hsb[i + 1] = hsb[i + 1] + con;
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

        public struct TDrawSpec
        {
            public int h_width, h_height;
            public MinMax<double> mm;
            public SolidBrush brush;
            public Graphics graph;

            public TDrawSpec(MinMax<double> m, int hw, int hh, SolidBrush b, Graphics g) {
                mm = m; h_width = hw; h_height = hh;
                brush = b; graph = g;
            }
        }

        // parallel = false
        public static void hist_draw(TBitmap im, int i, TDrawSpec spec) {
            var hist = im.hist;
            var w = spec.h_width; var h = spec.h_height;
            int value = (int)(hist[i, H] / spec.mm.max * h);
            spec.graph.FillRectangle(spec.brush, i * w, h - value, w, value);
        }

        #endregion

        #region Filtration

        public class TFilterData
        {
            public TBitmap src;
            public double[] mat;
            public int width, height;
            public double sum;
        }

        public static void filter_simple(TBitmap im, int i, TFilterData data) {
            var yuv = data.src.yuv;
            int w = im.width, h = im.height;
            int iy = i / w, ix = i % w;
            int dw = data.width >> 1, dh = data.height >> 1;
            int cou = 0;
            double result = 0;

            for (int y = -dh; y <= dh; y++) {
                int yy = iy + y;
                if (yy < 0) continue;
                if (yy >= h) break;

                for (int x = -dw; x <= dw; x++) {
                    int xx = ix + x;
                    if (xx < 0) continue;
                    if (xx >= w) break;
                    int ii = yy * w + xx;
                    double value = data.mat[cou++];
                    result += value * yuv[ii << 2];
                }
            }
            im.yuv[i << 2] = Tools.FixByte(result);
        }

        // parallel = false
        public static void filter_average(TBitmap im, int i, TFilterData data) {
            var yuv = data.src.yuv;
            int w = im.width, h = im.height;
            int iy = i / w, ix = i % w;
            int dw = data.width >> 1, dh = data.height >> 1;
            int cou = 0;
            double result = 0;

            for (int y = -dh; y <= dh; y++) {
                int yy = iy + y;
                if (yy < 0) continue;
                if (yy >= h) break; 

                for (int x = -dw; x <= dw; x++) {
                    int xx = ix + x;
                    if (xx < 0) continue;
                    if (xx >= w) break;
                    int ii = yy * w + xx;
                    double value = data.mat[cou++];
                    result += value * yuv[ii << 2];
                }
            }
            im.yuv[i << 2] = Tools.FixByte(result / data.sum);
        }

        // todo: fixit
        // parallel = false
        public static void filter_median(TBitmap im, int i, TFilterData data) {
            var yuv = data.src.yuv;
            int w = im.width, h = im.height;
            int iy = i / w, ix = i % w;
            int dw = data.width >> 1, dh = data.height >> 1;
            int cou = 0;
            List<double> lst = new List<double>();

            for (int y = -dh; y <= dh; y++) {
                int yy = iy + y;
                if (yy < 0 || yy >= h) {
                    lst.Add(0);
                    continue;
                }

                for (int x = -dw; x <= dw; x++) {
                    int xx = ix + x;
                    if (xx < 0 || xx >= w) {
                        lst.Add(0);
                        continue;
                    }
                    int ii = yy * w + xx;
                    double value = data.mat[cou++];
                    lst.Add(value * yuv[ii << 2]);
                }
            }
            lst.Sort();
            im.yuv[i << 2] = Tools.FixByte(lst[dw + data.width * dh]);
        }

        #endregion
    }
}
