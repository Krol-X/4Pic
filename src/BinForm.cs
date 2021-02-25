﻿using System;
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

        private int threshold;
        private bool adaptive;

        #region BinForm

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

        #endregion

        #region Do-binary

        private void track_change(int thr, bool adap) {
            label_thr.Text = thr.ToString();
            threshold = thr;
            adaptive = adap;

            TBitmap im = new TBitmap(srcimage, true);
            if (adaptive) {
                // TODO: codeit
            } else {
                image = im.do_image(to_binary);
            }
        }            

        void to_binary(ref byte[] rgb, ref int[] yuv, int i) {
            rgb[i + 0] = rgb[i + 1] = rgb[i + 2] = (byte)(yuv[i + 0] < threshold ? 255 : 0);
        }

        #endregion
    }
}
