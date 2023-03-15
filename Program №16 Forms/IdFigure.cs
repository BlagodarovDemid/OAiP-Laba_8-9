using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pain;
using static Program__16_Forms.Form1;

namespace Program__16_Forms
{
    class IdFigure : Figure
    {
        Figure figure;
        private int numPoints = 3;
        public new PointF[] pointFs;

        public IdFigure(int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }
        public IdFigure()
        {
            this.x = 0;
            this.y = 0;
            this.w = 0;
            this.h = 0;
        }

        public override void Draw()
        {
            Graphics g = Graphics.FromImage(Init.bitmap);
            this.pointFs = new PointF[numPoints];
            g.DrawRectangle(Init.pen, this.x, this.y + this.h/3, this.w, this.h - this.h/3);
            g.DrawEllipse(Init.pen, (this.x + this.w / 3 + this.w/12), (this.y + this.h / 2), this.w / 5, this.w / 5);
            pointFs[0].X = this.x;
            pointFs[0].Y = this.y + this.h/3;
            pointFs[1].X = this.x + this.w;
            pointFs[1].Y = this.y + this.h/3;
            pointFs[2].X = this.x + this.w / 2;
            pointFs[2].Y = this.y;
            g.DrawPolygon(Init.pen, pointFs);
            Init.pictureBox.Image = Init.bitmap;
        }
        public override void MoveTo(int x, int y)
        {
            for (int i = 0; i < numPoints; i++)
            {
                if (pointFs[i].X > Init.bitmap.Width || pointFs[i].Y > Init.bitmap.Height || pointFs[i].X < 0 || pointFs[i].Y < 0)
                {
                    pointFs[i].X += -x;
                    pointFs[i].Y += -y;
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

    }
}
