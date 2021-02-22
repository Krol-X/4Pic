using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace _4Pic.src
{
    public static class Engine
    {
        private const int MAX_THREADS = 24;
        public unsafe delegate Color pixel_hnd(Color pixel);

        public unsafe static void do_pixel(Bitmap image, pixel_hnd hnd) {
            int pixsz = Image.GetPixelFormatSize(image.PixelFormat) >> 3;
            var size = image.Width * image.Height * pixsz;
            var partsz = size / MAX_THREADS;
            var lastpartsz = size % MAX_THREADS;

            //BitmapData data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
            //int w = image.Width;

            //IntPtr ptr = data.Scan0;                                         
            //int bytes = image.Width * image.Height;
            LockBitmap bmp = new LockBitmap(image);
            bmp.LockBits();
            for (int j=0; j<bmp.Height; j++) {
                for (int i=0; i< bmp.Width; i++) {
                    bmp.SetPixel(i, j, hnd(bmp.GetPixel(i, j)));
                }
            }
            //for (int i = 0; i < bmp.Width*bmp.Height; i++) {
            //    bmp.SetPixel(i, hnd(bmp.GetPixel(i)));
            //}
            bmp.UnlockBits();

            //for (int j=0; j<image.Height; j++) {
            //    for (int i=0; i<image.Width; i++) {
            //        image.SetPixel(i, j, hnd(image.GetPixel(i, j)));
            //    }
            //}
            //for (int i = 0; i < size; i++) {
                //rgb[i] = hnd(rgb[i]);
            //}
            /*
            Parallel.For(0, MAX_THREADS, j => {
                var line = rgb + j * partsz;
                for (int i = 0; i < partsz; i++) {
                    line[i] = hnd(line[i]);
                }
            });
            if (lastpartsz != 0) {
                var line = rgb + MAX_THREADS * partsz;
                for (int i = 0; i < lastpartsz; i++) {
                    line[i] = hnd(line[i]);
                }
            }
            */

            // image.UnlockBits(data);
        }
    }
}
