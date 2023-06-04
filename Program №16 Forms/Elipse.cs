using System;
using Pain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Program__16_Forms.Form1;
using System.Windows.Forms;
using System.Drawing;

namespace Program__16_Forms
{
    internal class Elipse : Figure
    {
        Figure figure;
        public Elipse(int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }
        public Elipse()
        {
            this.x = 0;
            this.y = 0;
            this.w = 0;
            this.h = 0;
        }
        public override void Draw()
        {
            Graphics g = Graphics.FromImage(Init.bitmap);
            g.DrawEllipse(Init.pen, this.x, this.y, this.w, this.h);
            Init.pictureBox.Image = Init.bitmap;
        }

        public override void MoveTo(int x, int y)
        {
            if (!((this.x + x < 0 && this.y + y < 0)
                || (this.y + y < 0)
                || (this.x + x > Init.pictureBox.Width && this.y + y < 0)
                || (this.x + this.w + x > Init.pictureBox.Width)
                || (this.x + x > Init.pictureBox.Width && this.y + y > Init.pictureBox.Height)
                || (this.y + this.h + y > Init.pictureBox.Height)
                || (this.x + x < 0 && this.y + y > Init.pictureBox.Height) || (this.x + x < 0)))
            {
                this.x += x;
                this.y += y;

                Graphics g = Graphics.FromImage(Init.bitmap);
                ShapeContainer.figureList.Remove(figure);

                g.Clear(Color.White);

                Init.pictureBox.Image = Init.bitmap;
                foreach (Figure f in ShapeContainer.figureList)
                {
                    f.Draw();
                }
                ShapeContainer.figureList.Add(figure);
            }
        }

    }

    class Circle : Elipse
    {
        Figure figure;
        public Circle(int x, int y, int w) : base(x, y, w, w)
        {
            this.x = x;
            this.y = y;
            this.w = w;
        }
        public Circle()
        {
            this.x = 0;
            this.y = 0;
            this.w = 0;
        }
        public override void Draw()
        {
            Graphics g = Graphics.FromImage(Init.bitmap);
            g.DrawEllipse(Init.pen, this.x, this.y, this.w, this.w);
            Init.pictureBox.Image = Init.bitmap;
        }

        public override void MoveTo(int x, int y)
        {
            if (!((this.x + x < 0 && this.y + y < 0)
                || (this.y + y < 0)
                || (this.x + x > Init.pictureBox.Width && this.y + y < 0)
                || (this.x + this.w + x > Init.pictureBox.Width)
                || (this.x + x > Init.pictureBox.Width && this.y + y > Init.pictureBox.Width)
                || (this.y + this.w + y > Init.pictureBox.Width)
                || (this.x + x < 0 && this.y + y > Init.pictureBox.Width) || (this.x + x < 0)))
            {
                this.x += x;
                this.y += y;

                Graphics g = Graphics.FromImage(Init.bitmap);
                ShapeContainer.figureList.Remove(figure);

                g.Clear(Color.White);

                Init.pictureBox.Image = Init.bitmap;
                foreach (Figure f in ShapeContainer.figureList)
                {
                    f.Draw();
                }
                ShapeContainer.figureList.Add(figure);
            }
        }
    }
    class Circle2 : Circle
    {
        Figure figure;
        public Circle2(int w, int y, int x, string name) : base(x, y, w)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.name = name;
        }
        public Circle2()
        {
            this.x = 0;
            this.y = 0;
            this.w = 0;
        }
        public override void Draw()
        {
            Graphics g = Graphics.FromImage(Init.bitmap);
            g.DrawEllipse(Init.pen, this.x, this.y, this.w, this.w);
            Init.pictureBox.Image = Init.bitmap;
        }

        public override void MoveTo(int x, int y)
        {
            if (!((this.x + x < 0 && this.y + y < 0)
                || (this.y + y < 0)
                || (this.x + x > Init.pictureBox.Width && this.y + y < 0)
                || (this.x + this.w + x > Init.pictureBox.Width)
                || (this.x + x > Init.pictureBox.Width && this.y + y > Init.pictureBox.Width)
                || (this.y + this.w + y > Init.pictureBox.Width)
                || (this.x + x < 0 && this.y + y > Init.pictureBox.Width) || (this.x + x < 0)))
            {
                this.x += x;
                this.y += y;

                Graphics g = Graphics.FromImage(Init.bitmap);
                ShapeContainer.figureList.Remove(figure);

                g.Clear(Color.White);

                Init.pictureBox.Image = Init.bitmap;
                foreach (Figure f in ShapeContainer.figureList)
                {
                    if (f != null)
                    f.Draw();
                }
                ShapeContainer.figureList.Add(figure);
            }
        }
    }
}
