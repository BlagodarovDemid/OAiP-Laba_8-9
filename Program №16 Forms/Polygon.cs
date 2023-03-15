using System;
using Pain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Program__16_Forms.Form1;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices.ComTypes;

namespace Program__16_Forms
{

    internal class Polygon : Figure
        {
            public PointF[] pointFs;

            Polygon polygon;
            Pen pen;

            public Polygon(PointF[] pointFs)
            {
                this.pointFs = pointFs;
            }
            public void DeleteF(Figure figure, bool flag = true)
            {
                if (flag == true)
                {
                    Graphics g = Graphics.FromImage(Init.bitmap);
                    ShapeContainer.RemoveFigure(figure);
                    g.Clear(Color.WhiteSmoke);
                    Init.pictureBox.Image = Init.bitmap;
                    foreach (Figure f in ShapeContainer.figureList)
                    {
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
                        f.Draw();
                    }
                    ShapeContainer.AddFigure(figure);
                }
            }
            public override void Draw()
            {
                Graphics g = Graphics.FromImage(Init.bitmap);
                g.DrawPolygon(Init.pen, pointFs);
                Init.pictureBox.Image = Init.bitmap;
            }

            public override void MoveTo(int x, int y)
            {
            bool mnog = false;
                try
                {
                    for (int j = 0; j < pointFs.Length; j++)
                    {
                        if (!((this.pointFs[j].X + x < 0 && this.pointFs[j].Y + y < 0)
                        || (this.pointFs[j].Y + y < 0)
                        || (this.pointFs[j].X + x > Init.pictureBox.Width && this.pointFs[j].Y + y < 0)
                        || (this.pointFs[j].X + this.w + x > Init.pictureBox.Width)
                        || (this.pointFs[j].X + x > Init.pictureBox.Width && this.pointFs[j].Y + y > Init.pictureBox.Height)
                        || (this.pointFs[j].Y + this.h + y > Init.pictureBox.Height)
                        || (this.pointFs[j].X + x < 0 && this.pointFs[j].Y + y > Init.pictureBox.Height) || (this.pointFs[j].X + x < 0)))
                        {
                            mnog=true;
                        } 
                        else
                        {
                            mnog=false;
                            break;
                        }
                    }
                    if (mnog)
                    {
                        for (int i = 0; i < pointFs.Length; i++)
                        {
                            pointFs[i].X += x;
                            pointFs[i].Y += y;
                        }
                    }

                    DeleteF(this, false);
                    this.Draw();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    class Triangle : Polygon
    {
        public new PointF[] pointFs;
        
        Polygon polygon;
        Pen pen;

        public Triangle(PointF[] pointFs) : base(pointFs)
        {
            this.pointFs = pointFs;
        }
        public override void Draw()
        {
            Graphics g = Graphics.FromImage(Init.bitmap);
            g.DrawPolygon(Init.pen, pointFs);
            Init.pictureBox.Image = Init.bitmap;
        }
        
        public override void MoveTo(int x, int y)
        {
            bool mnog = false;
            try
            {
                for (int j = 0; j < pointFs.Length; j++)
                {
                    if (!((this.pointFs[j].X + x < 0 && this.pointFs[j].Y + y < 0)
                    || (this.pointFs[j].Y + y < 0)
                    || (this.pointFs[j].X + x > Init.pictureBox.Width && this.pointFs[j].Y + y < 0)
                    || (this.pointFs[j].X + this.w + x > Init.pictureBox.Width)
                    || (this.pointFs[j].X + x > Init.pictureBox.Width && this.pointFs[j].Y + y > Init.pictureBox.Height)
                    || (this.pointFs[j].Y + this.h + y > Init.pictureBox.Height)
                    || (this.pointFs[j].X + x < 0 && this.pointFs[j].Y + y > Init.pictureBox.Height) || (this.pointFs[j].X + x < 0)))
                    {
                        mnog = true;
                    }
                    else
                    {
                        mnog = false;
                        break;
                    }
                }
                if (mnog)
                {
                    for (int i = 0; i < pointFs.Length; i++)
                    {
                        pointFs[i].X += x;
                        pointFs[i].Y += y;
                    }
                }

                DeleteF(this, false);
                this.Draw();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
