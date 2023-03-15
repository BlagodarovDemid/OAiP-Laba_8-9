using System;
using Pain;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;
using System.Globalization;
using System.Collections;
using System.Linq.Expressions;

namespace Program__16_Forms
{
    public partial class Form1 : Form
    {
        public static class Init
        {
            public static Bitmap bitmap;
            public static PictureBox pictureBox;
            public static Pen pen;
        }

        Bitmap bitmap;
        Pen pen;
        
        Figure figure;
        Rectangle rectangle;
        Square square;
        Elipse elipse;
        Circle circle;
        Polygon polygon;
        Triangle triangle;
        IdFigure idFigure;

        int i = 0;
        int count = 1;
        private int numPoints;
        private PointF[] pointFs;
        private bool flag = false;
        private bool triag;

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            pen = new Pen(Color.Black, 5);
            Init.bitmap = bitmap;
            Init.pictureBox = pictureBox1;
            Init.pen = pen;
            button5.Visible = false;
        }

        public void Clear()
        {
            Graphics g = Graphics.FromImage(Init.bitmap);
            g.Clear(Color.White);
            listBox1.Items.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            pictureBox1.Refresh();
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

        private void button1_Click(object sender, EventArgs e)// Draw and AddShape
        {
            if (comboBox1.SelectedIndex == 0)
            {
                rectangle = new Rectangle(int.Parse(textBox1.Text), int.Parse(textBox2.Text), int.Parse(textBox3.Text), int.Parse(textBox4.Text));
                ShapeContainer.AddFigure(this.rectangle);
                button5.Visible = false;
                listBox1.Items.Clear();
                try
                {
                    foreach (Figure figure in ShapeContainer.figureList)
                    {
                        listBox1.Items.Add(figure);
                        rectangle.Draw();
                        textBox2.Enabled = true;
                        textBox3.Enabled = true;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (comboBox1.SelectedIndex == 1)
            {
                square = new Square(int.Parse(textBox1.Text), int.Parse(textBox2.Text), int.Parse(textBox3.Text));
                ShapeContainer.AddFigure(square);
                button5.Visible = false;
                listBox1.Items.Clear();
                try
                {
                    foreach (Figure figure in ShapeContainer.figureList)
                    {
                        listBox1.Items.Add(figure);
                        square.Draw();
                        textBox2.Enabled = true;
                        textBox3.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (comboBox1.SelectedIndex == 2)
            {
                elipse = new Elipse(int.Parse(textBox1.Text), int.Parse(textBox2.Text), int.Parse(textBox3.Text), int.Parse(textBox4.Text));
                ShapeContainer.AddFigure(elipse);
                button5.Visible = false;
                listBox1.Items.Clear();
                try
                {
                    foreach (Figure figure in ShapeContainer.figureList)
                    {
                        listBox1.Items.Add(figure);
                        elipse.Draw();
                        textBox2.Enabled = true;
                        textBox3.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (comboBox1.SelectedIndex == 3)
            {
                circle = new Circle(int.Parse(textBox1.Text), int.Parse(textBox2.Text), int.Parse(textBox3.Text));
                ShapeContainer.AddFigure(circle);
                button5.Visible = false;
                listBox1.Items.Clear();
                try
                {
                    foreach (Figure figure in ShapeContainer.figureList)
                    {
                        listBox1.Items.Add(figure);
                        circle.Draw();
                        textBox2.Enabled = true;
                        textBox3.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }                        
            if (comboBox1.SelectedIndex == 4)
            {
                polygon = new Polygon(pointFs);
                ShapeContainer.AddFigure(polygon);               
                label4.Visible = true;
                label4.Text = "";
                listBox1.Items.Clear();
                try
                {
                    foreach (Figure figure in ShapeContainer.figureList)
                    {
                        listBox1.Items.Add(figure);
                        polygon.Draw();
                        textBox2.Enabled = true;
                        textBox3.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (comboBox1.SelectedIndex == 5)
            {
                triangle = new Triangle(pointFs);
                ShapeContainer.AddFigure(triangle);
                label4.Visible = true;
                label4.Text = "";
                listBox1.Items.Clear();
                try
                {
                    foreach (Figure figure in ShapeContainer.figureList)
                    {
                        listBox1.Items.Add(figure);
                        triangle.Draw();
                        textBox2.Enabled = true;
                        textBox3.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (comboBox1.SelectedIndex == 6)
            {
                idFigure = new IdFigure(int.Parse(textBox1.Text), int.Parse(textBox2.Text), int.Parse(textBox3.Text), int.Parse(textBox4.Text));
                ShapeContainer.AddFigure(this.idFigure);
                button5.Visible = false;
                listBox1.Items.Clear();
                try
                {
                    foreach (Figure figure in ShapeContainer.figureList)
                    {
                        listBox1.Items.Add(figure);
                        idFigure.Draw();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)// MoveTo
        {
            try
            {
                if(listBox1.SelectedItem == null)
                {
                    MessageBox.Show("Null");
                }
                else if(listBox1.SelectedItem == this.rectangle)
                {
                    this.rectangle = (Rectangle)listBox1.SelectedItem;
                    this.rectangle.MoveTo(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
                }
                else if (listBox1.SelectedItem == this.square)
                {
                    this.square = (Square)listBox1.SelectedItem;
                    this.square.MoveTo(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
                }
                else if (listBox1.SelectedItem == this.elipse)
                {
                    this.elipse = (Elipse)listBox1.SelectedItem;
                    this.elipse.MoveTo(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
                }
                else if (listBox1.SelectedItem == this.circle)
                {
                    this.circle = (Circle)listBox1.SelectedItem;
                    this.circle.MoveTo(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
                }
                else if (listBox1.SelectedItem == this.polygon)
                {
                    this.polygon = (Polygon)listBox1.SelectedItem;
                    this.polygon.MoveTo(int.Parse(textBox2.Text), int.Parse(textBox3.Text));
                }
                else if (listBox1.SelectedItem == this.triangle)
                {
                    this.triangle = (Triangle)listBox1.SelectedItem;
                    this.triangle.MoveTo(int.Parse(textBox2.Text), int.Parse(textBox3.Text));
                }
                else if (listBox1.SelectedItem == this.idFigure)
                {
                    this.idFigure = (IdFigure)listBox1.SelectedItem;
                    this.idFigure.MoveTo(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)// Clear
        {
            Clear();
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
/*            double a;
            while (!(double.TryParse(textBox1.Text, out a)))
            {
                MessageBox.Show("Введите число");
                textBox1.Clear();
                return;
            }*/
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
/*            double a;
            while (!(double.TryParse(textBox1.Text, out a)))
            {
                MessageBox.Show("Введите число");
                textBox1.Clear();
                return;
            }*/
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
/*            double a;
            while (!(double.TryParse(textBox1.Text, out a)))
            {
                MessageBox.Show("Введите число");
                textBox1.Clear();
                return;
            }*/
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
/*            double a;
            while (!(double.TryParse(textBox1.Text, out a)))
            {
                MessageBox.Show("Введите число");
                textBox1.Clear();
                return;
            }*/
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)// CreatePoint
        {
            if (flag == false)
            {
                numPoints = int.Parse(textBox1.Text);
                this.pointFs = new PointF[numPoints];
                flag = true;
                textBox1.Enabled = false;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                label2.Enabled = false;
                label3.Enabled = false;
                label1.Text = $"Введите координаты {count}-й точки: ";
            }
            else
            {
                if (i < numPoints - 1)
                {
                    count++;
                    label1.Text = $"Введите координаты {count}-й точки: ";
                    pointFs[i].X = float.Parse(textBox2.Text);
                    pointFs[i].Y = float.Parse(textBox3.Text);
                    i++;
                    textBox2.Clear();
                    textBox3.Clear();
                }
                else
                {
                    pointFs[i].X = float.Parse(textBox2.Text);
                    pointFs[i].Y = float.Parse(textBox3.Text);
                    button5.Enabled = false;
                    button1.Enabled = true;

                    label1.Text = "Фигура собрана!";
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    flag = false;
                }
            }

            if (triag == false)
            {
                
                numPoints = 3;
                this.pointFs = new PointF[numPoints];
                triag = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                label2.Enabled = false;
                label3.Enabled = false;
                if (i < numPoints - 1)
                {
                    count++;
                    label1.Text = $"Введите координаты {count}-й точки: ";
                    pointFs[i].X = float.Parse(textBox2.Text);
                    pointFs[i].Y = float.Parse(textBox3.Text);
                    i++;
                    textBox2.Clear();
                    textBox3.Clear();
                }
                else
                {
                    pointFs[i].X = float.Parse(textBox2.Text);
                    pointFs[i].Y = float.Parse(textBox3.Text);
                    button5.Enabled = false;
                    button1.Enabled = true;

                    label1.Text = "Фигура собрана!";
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    triag = false;
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                triag = true;
                label1.Text = "X";
                label2.Text = "Y";
                label3.Text = "W";
                label4.Text = "H";
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                button5.Visible = false;
                label5.Visible = false;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                triag = true;
                label1.Text = "X";
                label2.Text = "Y";
                label3.Text = "W";
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = false;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = false;
                button5.Visible = false;
                label5.Visible = false;
            }
            if (comboBox1.SelectedIndex == 2)
            {
                triag = true;
                label1.Text = "X";
                label2.Text = "Y";
                label3.Text = "W";
                label4.Text = "H";
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                button5.Visible = false;
                label5.Visible = false;
            }
            if (comboBox1.SelectedIndex == 3)
            {
                triag = true;
                label1.Text = "X";
                label2.Text = "Y";
                label3.Text = "R";
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = false;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = false;
                button5.Visible = false;
                label5.Visible = false;
            }
            if (comboBox1.SelectedIndex == 4)
            {
                triag = true;

                label1.Text = "Введите количество точек:";
                label2.Text = "X";
                label3.Text = "Y";
                textBox1.Enabled = true;
                textBox2.Enabled = false;
                textBox3.Enabled = false;

                button5.Visible = true;
                button5.Enabled = true;
                label5.Visible = false;
                label4.Visible = false;
                textBox4.Visible = false;
            }
            if (comboBox1.SelectedIndex == 5)
            {
                triag = false;
                
                label5.Text = "Кол-во точек: 3";
                label1.Text = $"Введите координаты {count}-й точки: ";
                textBox1.Text = "3";
                label2.Text = "X";
                label3.Text = "Y";

                textBox1.Enabled = false;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                button5.Visible = true;
                label5.Visible = true;
                label4.Visible = false;
                textBox4.Visible = false;
            }
            if (comboBox1.SelectedIndex == 6)
            {
                triag = false;
                
                label1.Text = "X";
                label2.Text = "Y";
                label3.Text = "W";
                label4.Text = "H";
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox1.Text = "";
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                button5.Visible = false;
                label5.Visible = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)// Delete
        {
            try
            {
                if (listBox1.SelectedItem == null)
                {
                    MessageBox.Show("Null");
                }
                else
                {
                    Figure figure;
                    figure = (Figure)listBox1.SelectedItem;
                    DeleteF(figure, true);
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    listBox1.ResetText();
                    listBox1.Update();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}