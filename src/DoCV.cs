using System.Drawing;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using System.Collections.Generic;
using System.Collections;

namespace _4Pic.src
{
    using FigTable = List<ArrayList>;

    public static class DoCV
    {
        private static double PERCENT = 0.02;

        private static FigTable table = new FigTable() {
            new ArrayList {"Triangle",  new MCvScalar(0x00, 0xFF, 0x00)},
            new ArrayList {"Square",    new MCvScalar(0x00, 0x00, 0xFF)},
            new ArrayList {"Trapezoid", new MCvScalar(0x00, 0x00, 0xFF)},
            new ArrayList {"Rectangle", new MCvScalar(0x00, 0x00, 0xFF)},
            new ArrayList {"Pentagon",  new MCvScalar(0x90, 0x90, 0x90)},
            new ArrayList {"Hexagon",   new MCvScalar(0x90, 0x90, 0x90)},
            new ArrayList {"Circle",    new MCvScalar(0xFF, 0xA0, 0xA0)},
            new ArrayList {"Ellipse",   new MCvScalar(0xFF, 0xA0, 0xA0)}
        };
        enum Fig
        {
            triangle, square, trapezoid, rectangle,
            pentagon, hexagon, circle, ellipse
        }

        public static void HighlightFigure(IInputOutputArray im, int x, int y,
            VectorOfVectorOfPoint con, int i, int fig) {
            string name = (string)table[fig][0];
            MCvScalar color = (MCvScalar)table[fig][1];

            CvInvoke.DrawContours(im, con, i, color);
            CvInvoke.PutText(im, name, new Point(x, y),
                       FontFace.HersheyPlain, 1, color);
        }

        public static TBitmap RecognizeFigures(TBitmap src) {
            var result = new Image<Bgr, byte>(src.toBitmap());
            var gray = result
                .Convert<Gray, byte>()
                .SmoothGaussian(11)
                .ThresholdBinary(new Gray(244), new Gray(255))
                ;
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(gray, contours, hierarchy, RetrType.Tree,
                ChainApproxMethod.ChainApproxSimple);

            int w = result.Width, h = result.Height;
            int dw = (int)(w * PERCENT), dh = (int)(h * PERCENT);
            int w1 = w - dw, h1 = h - dh;

            for (int i = 0; i < contours.Size; i++) {
                var con = contours[i];
                double area = CvInvoke.ContourArea(con);
                double perimeter = CvInvoke.ArcLength(con, true);
                VectorOfPoint approx = new VectorOfPoint();
                CvInvoke.ApproxPolyDP(con, approx, 0.01 * perimeter, true);
                Rectangle rect = CvInvoke.BoundingRectangle(approx);
                if (rect.Left < dw || rect.Top < dh ||
                    rect.Left > w1 || rect.Top > h1)
                    continue;
                if (rect.Width < dw || rect.Height < dh ||
                    rect.Width > w1 || rect.Height > h1)
                    continue;

                Moments moments = CvInvoke.Moments(con);
                int x = (int)(moments.M10 / moments.M00);
                int y = (int)(moments.M01 / moments.M00);

                if (approx.Size == 3) {
                    HighlightFigure(result, x, y, contours, i, (int)Fig.triangle);
                } else if (approx.Size == 4) {
                    double aspectRatio = (double)rect.Width / rect.Height;
                    if (0.95 <= aspectRatio && aspectRatio <= 1.05) {
                        HighlightFigure(result, x, y, contours, i, (int)Fig.square);
                    } else if (1.2 <= aspectRatio && aspectRatio <= 2.0) {
                        HighlightFigure(result, x, y, contours, i, (int)Fig.rectangle);
                    } else {
                        HighlightFigure(result, x, y, contours, i, (int)Fig.trapezoid);
                    }
                } else if (approx.Size == 5) {
                    HighlightFigure(result, x, y, contours, i, (int)Fig.pentagon);
                } else if (approx.Size == 6) {
                    HighlightFigure(result, x, y, contours, i, (int)Fig.hexagon);
                } else if (approx.Size > 6) {
                    // PI*R^2 / (2*PI*R)^2 = 1/4*PI = 0.079577
                    double t = area / (perimeter * perimeter);
                    if (0.07 <= t && t <= 0.087) {
                        HighlightFigure(result, x, y, contours, i, (int)Fig.circle);
                    } else {
                        HighlightFigure(result, x, y, contours, i, (int)Fig.ellipse);
                    }
                }
            }
            return new TBitmap(result.Bitmap);
        }

        public static TBitmap ApplyScale(TBitmap src, double scale)
        {
            if (scale == 1.00)
                return src;
            var im = new Image<Bgr, byte>(src.toBitmap());
            var result = im.Resize(scale, Inter.Lanczos4);
            return new TBitmap(result.Bitmap);
        }

        public static TBitmap ApplyClaheContrast(TBitmap src)
        {
            // For rgb from: https://stackoverflow.com/questions/25008458/how-to-apply-clahe-on-rgb-color-images
            var bgr = new Image<Bgr, byte>(src.toBitmap());
            var lab = bgr.Convert<Lab, byte>();
            var clache = new Image<Gray, byte>(bgr.Size);
            CvInvoke.CLAHE(lab[0], 2, new Size(8, 8), clache);
            lab[0] = clache;
            var result = lab.Convert<Bgr, byte>();
            return new TBitmap(result.Bitmap);
        }
    }
}
