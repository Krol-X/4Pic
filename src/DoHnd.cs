using System;
using System.Drawing;
using static _4Pic.src.TBitmap;

namespace _4Pic.src
{
    public static class DoHnd
    {
        #region TBitmap handlers

        public const double Kr = 0.299, Kb = 0.114;
        public const double Kg = 1 - Kr - Kb;
        static bool BRI_WIKI = false;

        // parallel = true
        public static void rgb_fromyuv(TBitmap im, int i) {
            var rgb = im.rgb; var yuv = im.yuv;
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
            var rgb = im.rgb; var yuv = im.yuv;
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

        // parallel = true
        public static void hist_clear(TBitmap im, int i) {
            var hist = im.hist;
            hist[i, 0] = hist[i, 1] = hist[i, 2] = hist[i, 3] = 0;
        }

        // parallel = true
        public static void hist_rgb(TBitmap im, int i) {
            var rgb = im.rgb; var hist = im.hist;
            hist[rgb[i + 0], R]++;
            hist[rgb[i + 1], G]++;
            hist[rgb[i + 2], B]++;
        }

        // parallel = true
        public static void hist_hue(TBitmap im, int i) {
            var yuv = im.yuv; var hist = im.hist;
            hist[yuv[i + 0], H]++;
        }

        #endregion

        #region Basic operations

        // parallel = true
        public static void negative(TBitmap im, int i) {
            var rgb = im.rgb;
            rgb[i + 0] ^= 0xFF;
            rgb[i + 1] ^= 0xFF;
            rgb[i + 2] ^= 0xFF;
        }

        // parallel = true
        public static void grayscale(TBitmap im, int i) {
            var rgb = im.rgb; var yuv = im.yuv;
            rgb[i + 0] = rgb[i + 1] = rgb[i + 2] = (byte)yuv[i + 0];
        }

        #endregion

        #region Binary handlers

        // parallel = true
        public static void binary(TBitmap im, int i, object thr) {
            var rgb = im.rgb; var yuv = im.yuv;
            rgb[i + 0] = rgb[i + 1] = rgb[i + 2] =
                (byte)((yuv[i + 0] < (int)thr) ? 255 : 0);
        }

        #endregion

        #region Brightness, contrast and historam handlers

        // parallel = true
        public static void brightness(TBitmap im, int i, object bri) {
            var yuv = im.yuv;
            yuv[i + 0] = Tools.FixByte(yuv[i + 0] + (int)bri);
        }

        // parallel = true
        public static void contrast(TBitmap im, int i, object con) {
            var rgb = im.rgb; var x = (int)con;
            rgb[i + 0] = Tools.FixByte(x * (rgb[i + 0] - 128) + 128);
            rgb[i + 1] = Tools.FixByte(x * (rgb[i + 1] - 128) + 128);
            rgb[i + 2] = Tools.FixByte(x * (rgb[i + 2] - 128) + 128);
        }

        // parallel = false
        public static void hist_minmax(TBitmap im, int i, object p) {
            var hist = im.hist; var mm = (MinMax<double>)p;
            if (hist[i, H] < mm.min) { mm.min_i = i; mm.min = hist[i, H]; }
            if (hist[i, H] > mm.max) { mm.max_i = i; mm.max = hist[i, H]; }
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
        public static void hist_draw(TBitmap im, int i, object p) {
            var hist = im.hist; var spec = (TDrawSpec)p;
            var w = spec.h_width; var h = spec.h_height;
            int value = (int)(hist[i, H] / spec.mm.max * h);
            spec.graph.FillRectangle(spec.brush, i * w, h - value, w, value);
        }

        #endregion
    }
}
