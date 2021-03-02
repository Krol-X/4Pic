using System.Drawing;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;

namespace _4Pic.src
{
    public static class DoCV
    {
        public static void PutText(IInputOutputArray im, int x, int y, string s) {
            CvInvoke.PutText(im, s, new Point(x, y),
                       FontFace.HersheyPlain, 1, new MCvScalar(0, 0, 255), 1);
        }

        public static Bitmap RecognizeFigures(Bitmap src) {
            var im = new Image<Bgr, byte>(src);
            var gray = im
                .Convert<Gray, byte>()
                .SmoothGaussian(11)
                .ThresholdBinary(new Gray(244), new Gray(255))
                ;
            var contours = new VectorOfVectorOfPoint();
            var hierarchy = new Mat();
            CvInvoke.FindContours(gray, contours, hierarchy, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);

            for (int i = 0; i < contours.Size; i++) {
                double area = CvInvoke.ContourArea(contours[i]);
                double perimeter = CvInvoke.ArcLength(contours[i], true);
                var approx = new VectorOfPoint();
                CvInvoke.ApproxPolyDP(contours[i], approx, 0.01 * perimeter, true);
                CvInvoke.DrawContours(im, contours, i, new MCvScalar(0, 0, 255), 1);

                Moments moments = CvInvoke.Moments(contours[i]);
                int x = (int)(moments.M10 / moments.M00);
                int y = (int)(moments.M01 / moments.M00);
                if (approx.Size == 3) {
                    PutText(im, x, y, "Triangle");
                } else if (approx.Size == 4) {
                    Rectangle rect = CvInvoke.BoundingRectangle(contours[i]);
                    double aspectRatio = (double)rect.Width / rect.Height;
                    if (0.95 <= aspectRatio && aspectRatio <= 1.05) {
                        PutText(im, x, y, "Square");
                    } else if (1.2 <= aspectRatio && aspectRatio <= 2.0) {
                        PutText(im, x, y, "Trapezoid");
                    } else {
                        PutText(im, x, y, "Rectangle");
                    }
                } else if (approx.Size == 5) {
                    PutText(im, x, y, "Pentagon");
                } else if (approx.Size == 6) {
                    PutText(im, x, y, "Hexagon");
                } else if (approx.Size > 6) {
                    // PI*R^2 / (2*PI*R)^2 = 1/4*PI = 0.079577
                    double t = area / (perimeter * perimeter);
                    PutText(im, x, y, (0.07 <= t && t <= 0.087) ? "Circle" : "Ellipse");
                }
            }
            return im.Bitmap;
        }  
    }
}
