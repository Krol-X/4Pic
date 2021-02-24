using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _4Pic.src
{
    public partial class BriConForm : Form
    {
        private int HIST_COLW = 2;
        private const int HIST_COLH = 100;
        private Bitmap srcimage, _image;
        public Bitmap image
        {
            get { return _image; }
            set {
                ((MainForm)Owner).MainCanvas.Image = _image = value;
            }
        }

        public BriConForm(Form owner, Image image) {
            InitializeComponent();
            this.Owner = owner;
            this.srcimage = (Bitmap)image;
            track_change(track_bri.Value, track_con.Value);
        }

        private void button_ok_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
        }

        private void button_cancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }

        private void track_bri_Scroll(object sender, EventArgs e) {
            track_change(track_bri.Value, track_con.Value);
        }
        private void track_cont_Scroll(object sender, EventArgs e) {
            track_change(track_bri.Value, track_con.Value);
        }
        private void track_change(int bri, int con) {
            label_bri.Text = bri.ToString();
            label_con.Text = con.ToString();
            var main = new BriCon(srcimage, bri, con);
            image = main.Image; 
            HistCanvas.Image = MakeHistImage(main, HistCanvas.Width);
        }

        private Bitmap MakeHistImage(BriCon main, int width) {
            var hist = main.Histogram;   
            int min_i = main.Min_i, max_i = main.Max_i;
            int w = 256 * HIST_COLW;
            int ofs;
            ofs = (width - w) >> 1;
            Bitmap result = new Bitmap(width, HIST_COLH);
            Graphics g = Graphics.FromImage(result);
            g.Clear(Color.LightGray);
            g.FillRectangle(new SolidBrush(Color.Black), ofs, 0, 256 * HIST_COLW, HIST_COLH);
            SolidBrush usualPen = new SolidBrush(Color.DarkBlue);
            SolidBrush minPen = new SolidBrush(Color.White);
            SolidBrush maxPen = new SolidBrush(Color.Red);

            double min = main.Min;
            double max_0 = main.Max - min;
            for (int i = 0; i < 256; i++) {
                int value = (int)((hist[i] - min) / max_0 * HIST_COLH);
                SolidBrush pen;
                if (i == min_i) { pen = minPen; } else
                if (i == max_i) { pen = maxPen; } else { pen = usualPen; }
                g.FillRectangle(pen, ofs + i * HIST_COLW, HIST_COLH - value, HIST_COLW, value);
            }
            return result;
        }

        //private void BriConForm_MouseMove(object sender, MouseEventArgs e) {
        //    Point xy = Cursor.Position;
        //    Rectangle rect = new Rectangle(Left, Top, Width, Height);
        //    Opacity = rect.IntersectsWith(new Rectangle(xy.X, xy.Y, 1, 1)) ? 100 : 50;
        //    Console.WriteLine("{0}:{1} {2}:{3}", Left, Top, xy.X, xy.Y);
        //}
    }

    public class BriCon
    {
        private const double K_R = 0.299;
        private const double K_G = 0.587;
        private const double K_B = 0.114;
        public double[] Histogram;
        public int Min_i, Max_i;
        public double Min, Max;
        public Bitmap Image;

        private int Bri, Con;

        void brightness(ref byte[] im, int i) {
            im[i + 0] = Tools.FixPix(Bri + im[i + 0]);
            im[i + 1] = Tools.FixPix(Bri + im[i + 1]);
            im[i + 2] = Tools.FixPix(Bri + im[i + 2]);
        }

        void contrast(ref byte[] im, int i) {
            //im[i + 0] = Con
        }

        void histgray(ref byte[] im, int i) {
            byte hue = (byte)Math.Min(255, im[i + 0] * K_R + im[i + 1] * K_G + im[i + 2] * K_B);
            Histogram[hue]++;
        }

        public BriCon(Bitmap srcimage, int bri, int con) {
            Bri = bri; Con = con;

            Image = Engine.do_pixel((Bitmap)srcimage.Clone(), brightness);

            Histogram = new double[256];
            Engine.do_pixel(Image, histgray);

            double size = Image.Width * Image.Height;
            Min_i = Max_i = 0;
            Min = double.MaxValue;
            Max = 0;
            for (var i = 0; i < 256; i++) {
                Histogram[i] = Histogram[i] / size;
                if (Histogram[i] < Min) { Min_i = i; Min = Histogram[i]; }
                if (Histogram[i] > Max) { Max_i = i; Max = Histogram[i]; }
            }
        }
    }
}
