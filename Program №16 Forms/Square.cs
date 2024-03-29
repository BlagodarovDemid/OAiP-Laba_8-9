﻿using System;
using Pain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static Program__16_Forms.Form1;
using System.Windows.Forms;

namespace Program__16_Forms
{
    class Rectangle : Figure
    {
        Figure figure;
        
        public Rectangle(int x, int y, int w, int h,string name = null)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.name = name;
        }
        public Rectangle()
        {
            this.x = 0;
            this.y = 0;
            this.w = 0;
            this.h = 0;
            this.name = "";
        }
        public void DeleteF(Figure figure, bool flag = true)
        {
            if (flag == true)
            {
                Graphics g = Graphics.FromImage(Init.bitmap);
                ShapeContainer.figureList.Remove(figure);
                g.Clear(Color.WhiteSmoke);
                Init.pictureBox.Image = Init.bitmap;
                foreach (Figure f in ShapeContainer.figureList)
                {
                    if (f != null)
                        f.Draw();
                }
            }
            else
            {
                Graphics g = Graphics.FromImage(Init.bitmap);
                ShapeContainer.RemoveFigure(figure);
                g.Clear(Color.WhiteSmoke);
                Init.pictureBox.Image = Init.bitmap;
                foreach (Figure f in ShapeContainer.figureList)
                {
                    if (f != null)
                        f.Draw();
                }
                ShapeContainer.AddFigure(figure);
            }
        }

        public override void Draw()
        {
            Graphics g = Graphics.FromImage(Init.bitmap);
            g.DrawRectangle(Init.pen, this.x, this.y, this.w, this.h);
            Init.pictureBox.Image = Init.bitmap;
        }

        public override void MoveTo(int x, int y, string name)
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

/*                Graphics g = Graphics.FromImage(Init.bitmap);
                ShapeContainer.figureList.Remove(figure);

                g.Clear(Color.White);

                Init.pictureBox.Image = Init.bitmap;
                foreach (Figure f in ShapeContainer.figureList)
                {
                    f.Draw();
                }*/
                //figure.name = this.name;
                //ShapeContainer.figureList.Add(figure);
                
                DeleteF(this, false);
                this.Draw();
            }
        }
    }

    internal class Square : Rectangle
    {
        Figure figure;
        public Square(int x, int y, int w) : base(x, y, w, w)
        {
            this.x = x;
            this.y = y;
            this.w = w;
        }
        public Square()
        {
            this.x = 0;
            this.y = 0;
            this.w = 0;
        }
        public override void Draw()
        {
            Graphics g = Graphics.FromImage(Init.bitmap);
            g.DrawRectangle(Init.pen, this.x, this.y, this.w, this.w);
            Init.pictureBox.Image = Init.bitmap;
        }
        public override void MoveTo(int x, int y, string name)
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
}
