using System;
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
        const int PARTS_N = 8;

        public delegate void do_hnd(TBitmap image, int i);
        public delegate void do_hnd<T>(TBitmap image, int i, T param);
        public delegate IEnumerable<int> do_iter(TBitmap im);

        public readonly static int R = 0, G = 1, B = 2, H = 3;
        public const int pixel_size = 4;
        const PixelFormat pixel_format = PixelFormat.Format32bppArgb;

        public byte[] rgb;
        public int[] yuv;
        public double[,] hist; // [i, channel]
        public readonly int width, height, size, count;

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
            Marshal.Copy(data.Scan0, rgb, 0, count);
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
                if (iter == imIter && size > PARTS_N) {
                    int m = size / PARTS_N, n = size % PARTS_N;
                    Tools.Range(0, PARTS_N).AsParallel().ForAll(j =>
                    //for (int j=0; j<PARTS_N; j++)
                    {
                        int k = j * m;
                        it = Tools.Range(0, m);

                        it.AsParallel().ForAll(i => {
                            f(this, k + i);
                        });
                    }
                    );
                    if (n != 0) {
                        int k = PARTS_N * m;
                        it = Tools.Range(0, n);

                        it.AsParallel().ForAll(i => {
                            f(this, k + i);
                        });
                    }
                } else {
                    it.AsParallel().ForAll(i => {
                        f(this, i);
                    });
                }
            } else {
                foreach (int i in it) {
                    f(this, i);
                }
            }
            return this;
        }

        public TBitmap do_image<T>(do_hnd<T> f, do_iter iter, T p, bool use_parallel = false) {
            var it = iter(this);
            do_hnd ff = (im, i) => { f(im, i, p); };
            return do_image(ff, iter, use_parallel);
        }

        public static IEnumerable<int> imIter(TBitmap im) {
            return Tools.Range(0, im.size);
        }

        public static IEnumerable<int> hIter(TBitmap im) {
            return Tools.Range(0, 256);
        }
    }
}
