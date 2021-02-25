﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace _4Pic.src
{
    public partial class BriConForm : Form
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

        private const int HIST_COLW = 2, HIST_COLH = 100;
        private static Color BACK_COLOR = Color.Black;
        private static SolidBrush HIST_BRUSH = new SolidBrush(Color.LightGray);
        private static SolidBrush HIST_MAX_BRUSH = new SolidBrush(Color.Red);

        private int brightness;
        private double contrast;
        private Bitmap hist_image;
        private Graphics hist_g;
        protected int hmin_i, hmax_i;
        protected double hmin, hmax;

        #region BriConForm

        public BriConForm(Form owner, TBitmap image) {
            InitializeComponent();
            this.Owner = owner;
            this.srcimage = image;
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

        #endregion

        #region Do-BriCon

        private void set_brightness(ref byte[] rgb, ref int[] yuv, int i) {
            yuv[i + 0] = Tools.FixByte(yuv[i + 0] + brightness);
        }

        private void set_contrast(ref byte[] rgb, ref int[] yuv, int i) {
            rgb[i + 0] = Tools.FixByte(contrast * (rgb[i + 0] - 128) + 128);
            rgb[i + 1] = Tools.FixByte(contrast * (rgb[i + 1] - 128) + 128);
            rgb[i + 2] = Tools.FixByte(contrast * (rgb[i + 2] - 128) + 128);
        }

        private void calc_hminmax(ref double[,] hist, ref double[,] d1, int i) {
            if (hist[3, i] < hmin) { hmin_i = i; hmin = hist[3, i]; }
            if (hist[3, i] > hmax) { hmax_i = i; hmax = hist[3, i]; }
        }

        private void draw_hist(ref double[,] hist, ref double[,] d1, int i) {
            int value = (int)((hist[3, i] - hmin) / hmax * HIST_COLH);
            SolidBrush pen;
            if (i == hmax_i) { pen = HIST_MAX_BRUSH; } else { pen = HIST_BRUSH; }
            hist_g.FillRectangle(pen, i * HIST_COLW, HIST_COLH - value, HIST_COLW, value);
        }

        

        private void track_change(int bri, int con) {
            label_bri.Text = bri.ToString();
            label_con.Text = con.ToString();
            brightness = bri;

            TBitmap im = new TBitmap(srcimage, true);
            
            // Brightness
            im.do_image(set_brightness);
            im.update_rgb();

            // Contrast
            contrast = 259.0 * (con + 255.0) / (255.0 * (259.0 - con));
            im.do_image(set_contrast);
            im.update_yuv();

            image = im;

            // Histogram
            hist_image = new Bitmap(HIST_COLW * 256, HIST_COLH);
            hist_g = Graphics.FromImage(hist_image);
            im.update_hist();

            hmin_i = hmax_i = 0;
            hmin = double.MaxValue; hmax = 0;
            im.do_image(calc_hminmax);
            hmax -= hmin;

            hist_g.Clear(BACK_COLOR);
            im.do_image(draw_hist);
            HistCanvas.Image = hist_image;
        }

        #endregion
    }
}
