using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace _4Pic.src
{
    public class PictureBoxAdapter
    {
        PictureBox box;
        List<TBitmap> data;
        int current = -1;
        public TBitmap Current { get { return data[current]; } }

        public bool CurrentIsFirst() { return current <= 0; }
        public bool CurrentIsLast() { return current >= data.Count - 1; }

        public Action onDraw { get; set; }

        public PictureBoxAdapter(PictureBox _box)
        {
            data = new List<TBitmap>();
            box = _box;
        }

        public TBitmap add_new()
        {
            add(Current.clone());
            return Current;
        }

        public PictureBoxAdapter add(TBitmap bitmap, bool draw_current = true)
        {
            if (current < data.Count - 1)
                data.RemoveRange(current + 1, data.Count - 1);
            data.Add(bitmap);
            current = data.Count - 1;
            if (draw_current)
                DrawCurrent();
            return this;
        }
        public PictureBoxAdapter add(Bitmap it)
        {
            return add(new TBitmap(it));
        }

        public void HistClear() { data.Clear(); }

        public int HistSize { get { return data.Count; } }

        public TBitmap UndoAll()
        {
            current = 0;
            return DrawCurrent();
        }

        public TBitmap Undo()
        {
            if (current > 0)
                current--;
            return DrawCurrent();
        }

        public TBitmap Redo()
        {
            if (current < data.Count - 1)
                current++;
            return DrawCurrent();
        }

        public TBitmap RedoAll()
        {
            current = data.Count - 1;
            return DrawCurrent();
        }

        private double scale = 1.00;
        public double Scale
        {
            get { return scale; }
            set { scale = (value > 0) ? value : 1.00; }
        }

        public TBitmap Draw(TBitmap bitmap)
        {
            var scaled_im = DoCV.ApplyScale(bitmap.toBitmap(), scale);
            box.Image = scaled_im;
            onDraw();
            return new TBitmap(scaled_im);
        }
        public TBitmap Draw(Bitmap bitmap)
        {
            return Draw(new TBitmap(bitmap));
        }

        public TBitmap DrawCurrent()
        {
            return (data.Count > 0) ? Draw(Current) : null;
        }

        public void SaveImage(string path)
        {
            Current.toBitmap()
                .Save(path, Tools.ParseImageFormat(
                    Path.GetExtension(path).Substring(1)
                ));
        }
    }
}
