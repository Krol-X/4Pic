using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace _4Pic
{
    public unsafe struct RGBA
    {
        public byte R, G, B;

        public RGBA(byte r, byte g, byte b) : this() {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public RGBA(double r, double g, double b) : this((byte)r, (byte)g, (byte)b) { }
        public RGBA(UInt32 pix) : this((byte)pix, (byte)(pix >> 8), (byte)(pix >> 16)) { }

        public UInt32 PIX() {
            return (UInt32)((R << 16) | (G << 8) | B);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct Image32Bit
    {
        [FieldOffset(0)]
        public RGBA pixel;
        [FieldOffset(0)]
        public byte[] bytes;
    }

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

    public class LockBitmap
    {
        Bitmap source = null;
        IntPtr Iptr = IntPtr.Zero;
        BitmapData bitmapData = null;

        public byte[] Pixels { get; set; }
        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Stride { get; private set; }

        public LockBitmap(Bitmap source) {
            this.source = source;
        }

        /// <summary>
        /// Lock bitmap data
        /// </summary>
        public void LockBits() {
            try {
                // Get width and height of bitmap
                Width = source.Width;
                Height = source.Height;

                // Create rectangle to lock
                Rectangle rect = new Rectangle(0, 0, Width, Height);

                // get source bitmap pixel format size
                Depth = System.Drawing.Bitmap.GetPixelFormatSize(source.PixelFormat);

                // Check if bpp (Bits Per Pixel) is 8, 24, or 32
                if (Depth != 8 && Depth != 24 && Depth != 32) {
                    throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");
                }

                // Lock bitmap and return bitmap data
                bitmapData = source.LockBits(rect, ImageLockMode.ReadWrite,
                                             source.PixelFormat);

                Stride = bitmapData.Stride;
                int bytes = Math.Abs(Stride) * Height;

                // create byte array to copy pixel values
                Pixels = new byte[bytes];
                Iptr = bitmapData.Scan0;

                // Copy data from pointer to array
                Marshal.Copy(Iptr, Pixels, 0, Pixels.Length);
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Unlock bitmap data
        /// </summary>
        public void UnlockBits() {
            try {
                // Copy data from byte array to pointer
                Marshal.Copy(Pixels, 0, Iptr, Pixels.Length);

                // Unlock bitmap data
                source.UnlockBits(bitmapData);
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Get the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color GetPixel(int x, int y) {
            return GetPixel(((y * Stride/3) + x) * (Depth / 8));
        }

        /// <summary>
        /// Get the color of the specified pixel
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public Color GetPixel(int idx) {
            Color clr = Color.Empty;

            // Get color components count
            int cCount = Depth / 8;

            if (idx > Pixels.Length - cCount)
                throw new IndexOutOfRangeException();

            if (Depth == 32) // For 32 bpp get Red, Green, Blue and Alpha
            {
                byte b = Pixels[idx];
                byte g = Pixels[idx + 1];
                byte r = Pixels[idx + 2];
                byte a = Pixels[idx + 3]; // a
                clr = Color.FromArgb(a, r, g, b);
            }
            if (Depth == 24) // For 24 bpp get Red, Green and Blue
            {
                byte b = Pixels[idx];
                byte g = Pixels[idx + 1];
                byte r = Pixels[idx + 2];
                clr = Color.FromArgb(r, g, b);
            }
            if (Depth == 8)
            // For 8 bpp get color value (Red, Green and Blue values are the same)
            {
                byte c = Pixels[idx];
                clr = Color.FromArgb(c, c, c);
            }
            return clr;
        }

        /// <summary>
        /// Set the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void SetPixel(int x, int y, Color color) {
            SetPixel(((y * Stride / 3) + x) * (Depth / 8), color);
        }

        /// <summary>
        /// Set the color of the specified pixel
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="color"></param>
        public void SetPixel(int idx, Color color) {
            // Get color components count
            int cCount = Depth / 8;

            if (Depth == 32) // For 32 bpp set Red, Green, Blue and Alpha
            {
                Pixels[idx] = color.B;
                Pixels[idx + 1] = color.G;
                Pixels[idx + 2] = color.R;
                Pixels[idx + 3] = color.A;
            }
            if (Depth == 24) // For 24 bpp set Red, Green and Blue
            {
                Pixels[idx] = color.B;
                Pixels[idx + 1] = color.G;
                Pixels[idx + 2] = color.R;
            }
            if (Depth == 8)
            // For 8 bpp set color value (Red, Green and Blue values are the same)
            {
                Pixels[idx] = color.B;
            }
        }
    }
}
