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

        /*
        Mat fftshift(Mat src) {
            Mat cpy = new Mat();
            src.CopyTo(cpy);
            return ret;
        }

        // reverse the swapping of fftshift. (-> reverse the quadrant swapping)
        Mat ifftshift(Mat src) {

            // create copy to not mess up the original matrix (ret is only a "window" over the provided matrix)
            Mat cpy;
            mat.copyTo(cpy);

            // crop the spectrum, if it has an odd number of rows or columns
            Mat ret = cpy(Rect(0, 0, cpy.cols & -2, cpy.rows & -2));

            // rearrange the quadrants of Fourier image so that the origin is at the image center
            int cx = ret.cols / 2;
            int cy = ret.rows / 2;
            Mat q0(ret, Rect(0, 0, cx, cy));   // Top-Left - Create a ROI per quadrant
            Mat q1(ret, Rect(cx, 0, cx, cy));  // Top-Right
            Mat q2(ret, Rect(0, cy, cx, cy));  // Bottom-Left
            Mat q3(ret, Rect(cx, cy, cx, cy)); // Bottom-Right

            Mat tmp; // swap quadrants (Bottom-Right with Top-Left)
            q3.copyTo(tmp);
            q0.copyTo(q3);
            tmp.copyTo(q0);
            q2.copyTo(tmp); // swap quadrant (Bottom-Left with Top-Right)
            q1.copyTo(q2);
            tmp.copyTo(q1);

            return ret;
        }
        

        public static Bitmap LoFreqFilter(Bitmap src) {
            var im = new Image<Bgr, byte>(src);
            var mat = im.Mat;

            int a = CvInvoke.GetOptimalDFTSize(mat.Rows);
            int b = CvInvoke.GetOptimalDFTSize(mat.Cols);

            var extended = new Mat();
            CvInvoke.CopyMakeBorder(mat, extended, 0, a - mat.Rows, 0, b - mat.Cols, BorderType.Constant, new MCvScalar(0));

            extended.ConvertTo(extended, DepthType.Cv32F);
            var vec = new VectorOfMat(extended, new Mat(extended.Size, DepthType.Cv32F, 1));
            var complex = new Mat();
            CvInvoke.Merge(vec, complex);

            CvInvoke.Dft(complex, complex, DxtType.Forward, 0);
            CvInvoke.Split(complex, vec);
            vec[0].ConvertTo(vec[0], DepthType.Cv8U);
            vec[1].ConvertTo(vec[1], DepthType.Cv8U);

        }
        */
    }
}
