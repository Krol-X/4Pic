using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static _4Pic.src.DoHnd;
using static _4Pic.src.TBitmap;

namespace _4Pic.src
{
    public partial class FilterDialogForm : Form
    {
        private PictureBoxAdapter adapter;
        private TBitmap curimage;
        private FFT fft;

        public TBitmap image
        {
            get { return curimage; }
            set {
                curimage = value;
                adapter.Draw(curimage);
            }
        }



        public FilterDialogForm(Form owner, PictureBoxAdapter adapter) {
            InitializeComponent();
            this.Owner = owner;
            this.adapter = adapter;
            combo_type.SelectedIndex = 0;
        }

        private void button_ok_Click(object sender, EventArgs e) {
            if (change(cb_freq.Checked)) {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button_open_Click(object sender, EventArgs e) {
            if (dialog_open.ShowDialog() == DialogResult.OK) {
                try {
                    var txt = File.ReadAllLines(dialog_open.FileName);
                    textbox.Lines = txt;
                } catch {
                    MessageBox.Show("Невозможно открыть файл!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_save_Click(object sender, EventArgs e) {
            if (dialog_save.ShowDialog() == DialogResult.OK) {
                try {
                    File.WriteAllLines(dialog_save.FileName, textbox.Lines);
                } catch {
                    MessageBox.Show("Невозможно сохранить файл!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private TFilterData getMatrix(string[] src) {
            TFilterData result = new TFilterData();
            List<double> data = new List<double>();
            int w = 0;
            foreach (string line in src) {
                var nums = Regex.Replace(line, @"\s+", " ").Trim().Split(' ');
                int cou = 0;
                foreach (string num in nums) {
                    try {
                        var n = Convert.ToDouble(num);
                        data.Add(n);
                        cou++;
                    } catch {
                        MessageBox.Show("Неверный формат числа!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                if (w == 0) w = cou;
                if (cou != w) {
                    MessageBox.Show("Количество строк должно быть одинаковым!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            result.width = w;
            result.height = w == 0? 0: data.Count / w;
            result.mat = data.ToArray();
            result.sum = 0;
            data.ForEach(x => result.sum += x);
            return result;
        }

        private void button_preview_Click(object sender, EventArgs e) {
            change(cb_freq.Checked);
        }

        private bool change(bool fourier) {
            status_label.Text = "Идёт обработка...";
            this.Update();
            var mat = getMatrix(textbox.Lines);
            if (mat == null) return false;
            if (mat.width < 3 || mat.height < 3) {
                MessageBox.Show("Слишком маленький размер матрицы!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if ((mat.width | mat.height & 1) == 1) {
                MessageBox.Show("Количество линий и столбцов должно быть нечётным!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int i = combo_type.SelectedIndex;
            TBitmap srcfft;
            var im = adapter.Current.clone();
            if (fourier) {
                
                fft = new FFT(im.toBitmap());
                fft.ForwardFFT();
                fft.FFTShift();
                // todo: codeit
                fft.FFTShift();
                fft.InverseFFT();
                image = new TBitmap((Bitmap)fft.Obj);
                //im = srcfft.clone();
                //mat.src = srcfft;


            } else {
                mat.src = im;

                do_hnd<TFilterData>[] filter = { filter_simple, filter_average, filter_median };
                image = im.do_image<TFilterData>(filter[i], imIter, mat, true);
            }    
            status_label.Text = "Завершено";
            return true;
        }
    }
}
