using ArrayWrapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace _4Pic.src
{
    public class TBitmap
    {
        public delegate void do_hnd(TBitmap image, int i);
        public delegate void do_hndp(TBitmap image, int i, Object[] param);
        public delegate IEnumerable<int> do_iter(TBitmap im);

        public const int R = 0, G = 1, B = 2, H = 3;
        public const int pixel_size = 4;
        const PixelFormat pixel_format = PixelFormat.Format32bppArgb;

        public byte[] rgb;
        public int[] yuv;
        public double[,] hist; // [i, channel]
        protected int width, height, size, count;

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public int Size { get { return size; } }
        public int Count { get { return count; } }

        private TBitmap(int w, int h) {
            width = w; height = h;
            size = w * h; count = size * pixel_size;
            rgb = new byte[count];
            yuv = new int[count];
            hist = new double[256, 4];
        }

        public TBitmap(TBitmap src) : this(src.width, src.height) {
            rgb = (byte[])src.rgb.Clone();
        }

        public TBitmap(Bitmap src) : this(src.Width, src.Height) {
            var data = src.LockBits(new Rectangle(0, 0, width, height),
                                    ImageLockMode.ReadOnly,
                                    PixelFormat.Format32bppArgb);
            Marshal.Copy(data.Scan0, rgb, 0, Count);
            src.UnlockBits(data);
        }

        public TBitmap clone() {
            return new TBitmap(this);
        }

        public Bitmap toBitmap() {
            Bitmap result = new Bitmap(width, height);
            var data = result.LockBits(new Rectangle(0, 0, width, height),
                                    ImageLockMode.WriteOnly,
                                    PixelFormat.Format32bppArgb);
            Marshal.Copy(rgb, 0, data.Scan0, count);
            result.UnlockBits(data);
            return result;
        }

        public TBitmap do_image(do_hnd f, do_iter iter, bool use_parallel = false) {
            var it = iter(this);
            if (use_parallel) {
                Parallel.ForEach(it, (x, _, i) => {
                    f(this, (int)i);
                });
            } else {
                foreach (int i in it) {
                    f(this, i);
                }
            }
            return this;
        }

        public TBitmap do_image(do_hndp f, do_iter iter, bool use_parallel = false, params Object[] p) {
            var it = iter(this);
            if (use_parallel) {
                Parallel.ForEach(it, (x, _, i) => {
                    f(this, (int)i, p);
                });
            } else {
                foreach (int i in it) {
                    f(this, i, p);
                }
            }
            return this;
        }

        public static IEnumerable<int> Range(int start, int count, int step = 1) {
            for (int it = start; it < count; it += step) yield return it;
        }

        public static IEnumerable<int> imIter(TBitmap im) {
            return Range(0, im.Count, pixel_size);
        }

        public static IEnumerable<int> hIter(TBitmap im) {
            return Range(0, 256);
        }
    }
}
