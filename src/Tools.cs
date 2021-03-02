using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;

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
