using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using System.Runtime.InteropServices;
using System.Drawing;

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

        public static byte FixPix(byte pix) {
            return Math.Max((byte)0, Math.Min((byte)255, pix));
        }
        public static byte FixPix(double pix) {
            return FixPix((byte)pix);
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
}
