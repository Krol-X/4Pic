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
        private TBitmap srcimage, curimage;
        public TBitmap image
        {
            get { return curimage; }
            set {
                curimage = value;
                ((MainForm)Owner).MainCanvas.Image = curimage.toBitmap();
            }
        }



        public BinForm(Form owner, TBitmap image) {
            InitializeComponent();
            this.Owner = owner;
            this.srcimage = image;
            track_change(track_thr.Value, checkbox_adaptive.Checked);
        }

        private void button_ok_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
        }
        private void button_cancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }

        private void track_thr_Scroll(object sender, EventArgs e) {
            track_change(track_thr.Value, checkbox_adaptive.Checked);
        }



        private void track_change(int threshold, bool adaptive) {
            label_thr.Text = threshold.ToString();

            TBitmap im = srcimage.clone();
            if (adaptive) {
                throw new NotImplementedException();
            } else {
                image = im.do_image(DoHnd.to_binary, TBitmap.imIter, true, threshold);
            }
        }
    }
}
