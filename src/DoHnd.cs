using System;
using System.Windows;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

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
            hist[rgb[i + 0], TBitmap.R]++;
            hist[rgb[i + 1], TBitmap.G]++;
            hist[rgb[i + 2], TBitmap.B]++;
        }

        // parallel = true
        public static void hist_hue(TBitmap im, int i) {
            var yuv = im.yuv; var hist = im.hist;
            hist[yuv[i + 0], TBitmap.H]++;
        }

        #endregion

        #region Binary handlers

        // parallel = true
        public static void to_binary(TBitmap im, int i, Object[] p) {
            var rgb = im.rgb; var yuv = im.yuv; var thr = (byte)p[0];
            rgb[i + 0] = rgb[i + 1] = rgb[i + 2] = (byte)(yuv[i + 0] < thr ? 255 : 0);
        }

        #endregion

        #region Brightness, contrast and historam handlers

        // parallel = true
        public static void brightness(TBitmap im, int i, Object[] p) {
            var yuv = im.yuv; var x = (int)p[0];
            yuv[i + 0] = Tools.FixByte(yuv[i + 0] + x);
        }

        // parallel = true
        public static void contrast(TBitmap im, int i, Object[] p) {
            var rgb = im.rgb; var x = (int)p[0];
            rgb[i + 0] = Tools.FixByte(x * (rgb[i + 0] - 128) + 128);
            rgb[i + 1] = Tools.FixByte(x * (rgb[i + 1] - 128) + 128);
            rgb[i + 2] = Tools.FixByte(x * (rgb[i + 2] - 128) + 128);
        }

        // parallel = false
        public static void hist_minmax(TBitmap im, int i, Object[] p) {
            var hist = im.hist; var mm = (MinMax<double>)p[0];
            if (hist[3, i] < mm.min) { mm.min_i = i; mm.min = hist[3, i]; }
            if (hist[3, i] > mm.max) { mm.max_i = i; mm.max = hist[3, i]; }
        }

        // parallel = false
        public static void hist_draw(TBitmap im, int i, Object[] p) {
            var hist = im.hist; var mm = (MinMax<double>)p[0];
            var size = (Point)p[1]; var brush = (SolidBrush)p[2];
            var g = (Graphics)p[3];
            // todo: Point for doubles?
            int value = (int)(hist[i, TBitmap.H] / mm.max * size.Y);
            g.FillRectangle(brush, i * size.X, size.Y - value, size.X, value);
        }

        #endregion
    }
}
