using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Pain
{
    abstract public class Figure
    {
        public int x;
        public int y;
        public int w;
        public int h;
        public string name;
        abstract public void Draw();
        abstract public void MoveTo(int x, int y);
        public void Delete()
        {
            foreach (Figure f in ShapeContainer.figureList)
            {
                f.Draw();
            }
        }
        public void Move()
        {
            this.x += x;
            this.y += y;
        }
    }
    public class ShapeContainer
    {
        public static List<Figure> figureList = new List<Figure>();
        public static void AddFigure(Figure figure)
        {
            figureList.Add(figure);
        }
        public static void RemoveFigure(Figure figure)
        {
            figureList.Remove(figure);
        }
        public static Figure FindFigure(string name)
        {
            foreach (Figure find in figureList)
            {
                if (find.name == name)
                {
                    return find;
                }
            }
            return null;
        }
    }

}
