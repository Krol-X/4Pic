using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using _4Pic.src;
using static _4Pic.src.DoHnd;
using static _4Pic.src.TBitmap;

namespace _4Pic
{
    public static class Tools
    {
        public static ImageFormat ParseImageFormat(string str) {
            ImageFormat result;
            try {
                result = (ImageFormat)typeof(ImageFormat)
                        .GetProperty(str, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase)
                        .GetValue(str, null);
            } catch {
                result = ImageFormat.Bmp;
            }
            return result;
        }

        public static byte FixByte(byte pix) {
            return Math.Max((byte)0, Math.Min((byte)255, pix));
        }
        public static byte FixByte(double pix) {
            return (byte)Math.Max(0, Math.Min(255, pix));
        }

        public static int Min(params int[] values) {
            var result = values[0];
            foreach (var x in values) {
                result = Math.Min(x, result);
            }
            return result;
        }
        public static double Min(params double[] values) {
            var result = values[0];
            foreach (var x in values) {
                result = Math.Min(x, result);
            }
            return result;
        }

        public static int Max(params int[] values) {
            var result = values[0];
            foreach (var x in values) {
                result = Math.Max(x, result);
            }
            return result;
        }
        public static double Max(params double[] values) {
            var result = values[0];
            foreach (var x in values) {
                result = Math.Max(x, result);
            }
            return result;
        }

        public static IEnumerable<int> Range(int start, int count, int step = 1) {
            for (int it = start; it < count; it += step) yield return it;
        }

        public static TBitmap CalcHist(TBitmap im, bool calcsrc = false) {
            im.do_image(hist_clear, hIter, true);
            if (USE_HSV) {
                if (calcsrc) im.do_image(hsv_fromrgb, imIter, true);
                im.do_image(hist_hue_hsv, imIter, true);
            } else {
                if (calcsrc) im.do_image(yuv_fromrgb, imIter, true);
                im.do_image(hist_hue, imIter, true);
            }
            im.do_image(hist_divide, hIter, true);    
            return im;
        }
    }

    public class StackX<T> : List<T>
    {
        public void Push(params T[] elements) {
            foreach (var x in elements) {
                this.Add(x);
            }
        }

        public T Pop() {
            var i = Count;
            var x = this.ElementAt<T>(i);
            this.RemoveAt(i);
            return x;
        }

        public T Peek() {
            return this.ElementAt<T>(Count);
        }
    }

    public class MinMax<T>
    {
        public T min, max;
        public int min_i, max_i;
    }
}
