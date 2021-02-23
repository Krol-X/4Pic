using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace _4Pic.src
{
    public static class Engine
    {
        private const int PARTS_N = 24;
        private static bool USE_PARALLEL = false;
        public delegate void pixel_hnd(ref byte[] im, int off);

        public static Bitmap do_pixel(Bitmap image, pixel_hnd hnd) {
            int pixsize = 4, w = image.Width, h = image.Height;
            int bytes = w * h * pixsize;

            var imData = image.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite,
                                      PixelFormat.Format32bppArgb);  
            byte[] data = new byte[bytes];
            Marshal.Copy(imData.Scan0, data, 0, bytes);

            if (USE_PARALLEL) {
                var partsz = bytes / PARTS_N;
                // FIXIT: some bug???
                Parallel.For(0, PARTS_N, j => {
                    var pos = j * partsz;
                    for (int i = 0; i < partsz; i++) {
                        hnd(ref data, pos + i);
                    }
                });
                if (bytes % PARTS_N != 0) {
                    var pos = PARTS_N * partsz;
                    var bound = bytes - pos - pixsize + 1;
                    for (int i = 0; i < bound; i++) {
                        hnd(ref data, pos + i);
                    }
                }
            } else {
                for (int i = 0; i < bytes; i += pixsize) {
                    hnd(ref data, i);
                }
            }

            Marshal.Copy(data, 0, imData.Scan0, bytes);
            image.UnlockBits(imData);
            return image;
        }
    }
}
