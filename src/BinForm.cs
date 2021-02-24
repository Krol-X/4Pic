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
    public partial class BinForm : Form
    {
        private Bitmap srcimage, _image;
        public Bitmap image
        {
            get { return _image; }
            set {
                ((MainForm)Owner).MainCanvas.Image = _image = value;
            }
        }

        public BinForm(Form owner, Image image) {
            InitializeComponent();
            this.Owner = owner;
            this.srcimage = (Bitmap)image;
            track_change(track_thr.Value);
        }

        private void button_ok_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
        }

        private void button_cancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }

        private void track_thr_Scroll(object sender, EventArgs e) {
            track_change(track_thr.Value);
        }

        private void track_change(int thr) {
            label_thr.Text = thr.ToString();
            image = new Binary(srcimage, thr, false).Image;
        }

        public class Binary
        {
            public Bitmap Image;
            private int Thr;

            void binary(ref byte[] im, int i) {
                var sum = (im[i + 0] + im[i + 1] + im[i + 2]) / 3;
                im[i + 0] = im[i + 1] = im[i + 2] = (byte)(sum < Thr ? 255 : 0);
            }

            public Binary(Bitmap srcimage, int thr) {
                Thr = thr;
       
                Image = Engine.do_pixel((Bitmap)srcimage.Clone(), binary);
            }

            public static int getByOtsu(Bitmap srcimage) {
                int result = 0;
                // TODO: codeit
                return result;
            }
        }
    }
}
