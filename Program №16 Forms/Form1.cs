﻿using System;
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
using System.Xml.Linq;

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
        int RecCount = 0;
        private int numPoints;
        private PointF[] pointFs;
        private bool flag = false;
        private bool triag;

        int circleX = 0;
        int circleY = 0;
        int circleR = 0;

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            pen = new Pen(Color.Black, 5);
            Init.bitmap = bitmap;
            Init.pictureBox = pictureBox1;
            Init.pen = pen;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button6.Visible = false;
            button5.Visible = false;
            label5.Visible = false;
            comboBox1.Visible = false;
            listBox1.Visible = false;
            textBox5.Text = "C(figure_name,x,y,a) || M(figure_name,dx,dy) || D(figure_name)";
        }

        public class Operand
	    {
        	public object value;
            public Operand(object NewValue)
	        {
        	    this.value = NewValue;
            }
        }
        public class OperatorMethod
	    {
        	public delegate void EmptyOperatorMethod();
	        public delegate void UnaryOperatorMethod(object operand);
	        public delegate void BinaryOperatorMethod(object operand1, object operand2);
	        public delegate void TrinaryOperatorMethod(object operand1, object operand2, object operand3);
	    }
        public class Operator : OperatorMethod
        {
            public char symbolOperator;
            public EmptyOperatorMethod operatorMethod = null;
            public BinaryOperatorMethod binaryOperator = null;
            public TrinaryOperatorMethod trinaryOperator = null;
            public Operator(EmptyOperatorMethod operatorMethod, char symbolOperator)
            {
                this.operatorMethod = operatorMethod;
                this.symbolOperator = symbolOperator;
            }
            public Operator(BinaryOperatorMethod binaryOperator, char symbolOperator)
            {
                this.binaryOperator = binaryOperator;
                this.symbolOperator = symbolOperator;
            }
            public Operator(TrinaryOperatorMethod trinaryOperator, char symbolOperator)
            {
                this.trinaryOperator = trinaryOperator;
                this.symbolOperator = symbolOperator;
            }
            public Operator(char symbolOperator)
            {
                this.symbolOperator = symbolOperator;
            }
        }
        public static class OperatorContainer
        {
            public static List<Operator> operators = new List<Operator>();
            static OperatorContainer()
            {
                operators.Add(new Operator('C'));
                operators.Add(new Operator('M'));
                operators.Add(new Operator('D'));
                operators.Add(new Operator(','));
                operators.Add(new Operator('('));
                operators.Add(new Operator(')'));
            }
            public static Operator FindOperator(char s)
            {
                foreach (Operator op in operators)
                {
                    if (op.symbolOperator == s)
                    {
                        return op;
                    }
                }
                return null;
            }
        }

        private Stack<Operator> operators = new Stack<Operator>();
        private Stack<Operand> operands = new Stack<Operand>();
        private bool IsNotOperation(char item)
        {
            if (!(item == 'C' || item == 'M' || item == 'D' || item == ',' || item == '(' || item == ')'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void SelectingPerformingOperation(Operator op)
        {
            if (textBox5.Text[0] == 'C')
            {
                int r = int.Parse(Convert.ToString(operands.Pop().value));
                int y = int.Parse(Convert.ToString(operands.Pop().value));
                int x = int.Parse(Convert.ToString(operands.Pop().value));
                string name = Convert.ToString(operands.Pop().value);
                circleR = r;
                circleY = y;
                circleX = x;
                if (x + r < Init.pictureBox.Width && y + r < Init.pictureBox.Height)
                {
                    circle = new Circle2(r, y, x, name);
                    op = new Operator(this.circle.Draw, 'C');
                    ShapeContainer.AddFigure(this.circle);
                    listBox2.Items.Add("Команда: " + Convert.ToString(textBox5.Text) + " корректная");
                    listBox2.Items.Add("Окружность " + circle.name + " отрисована");
                    op.operatorMethod();
                }
                else
                {
                    listBox2.Items.Add("Превышено ограничение поля");
                }
            }
            if (textBox5.Text[0] == 'M')
            {
                try
                {
                    int y = Convert.ToInt32(Convert.ToString(operands.Pop().value));
                    int x = Convert.ToInt32(Convert.ToString(operands.Pop().value));
                    string name = Convert.ToString(operands.Pop().value);
                    if (ShapeContainer.FindFigure(name) == null)
                    {
                        listBox2.Items.Add("Команда: " + textBox5.Text + " некорректна");
                        listBox2.Items.Add("Пример записи: M(figure_name,dx,dy)");
                    }
                    else
                    {
                        if (!((circleX + x < 0 && circleY + y < 0)
                        || (circleY + y < 0)
                        || (circleX + x > Init.pictureBox.Width && circleY + y < 0)
                        || (circleX + circleR + x > Init.pictureBox.Width)
                        || (circleX + x > Init.pictureBox.Width && circleY + y > Init.pictureBox.Width)
                        || (circleY + circleR + y > Init.pictureBox.Width)
                        || (circleX + x < 0 && circleY + y > Init.pictureBox.Width) || (circleX + x < 0)))
                        {
                            ShapeContainer.FindFigure(name).MoveTo(x, y);
                            circleX += x;
                            circleY += y;
                            listBox2.Items.Add("Фигура: " + this.circle.name + " перемещена");
                            listBox2.Items.Add("На X: " + Convert.ToString(x) + " Y: " + Convert.ToString(y));
                            listBox2.Items.Add("X: " + Convert.ToString(circleX) + " Y: " + Convert.ToString(circleY));
                        }
                    }
                }
                catch
                {
                    listBox2.Items.Add("Команда: " + textBox5.Text + " некорректна");
                    listBox2.Items.Add("Пример записи: M(figure_name,dx,dy)");
                }
            }
            if (textBox5.Text[0] == 'D')
            {
                try
                {
                    string name = Convert.ToString(operands.Pop().value);
                    if (ShapeContainer.FindFigure(name) == null)
                    {
                        listBox2.Items.Add("Команда: " + textBox5.Text + " некорректна");
                        listBox2.Items.Add("Пример записи: D(figure_name)");
                    }
                    else
                    {
                        DeleteF(ShapeContainer.FindFigure(name), true);
                        listBox2.Items.Add("Удалена окружность: " + this.circle.name);
                    }
                }
                catch
                {
                    listBox2.Items.Add("Команда: " + textBox5.Text + " некорректна");
                    listBox2.Items.Add("Пример записи: D(figure_name)");
                }
            }
        }
        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            flag = true;
            if (e.KeyCode == Keys.Enter)
            {
                for (int i = 0; i < textBox5.Text.Length; i++)
                {
                    if (IsNotOperation(textBox5.Text[i]))
                    {
                        if (!(Char.IsDigit(textBox5.Text[i])))
                        {
                            this.operands.Push(new Operand(textBox5.Text[i]));
                            continue;
                        }
                        else if (Char.IsDigit(textBox5.Text[i]))
                        {
                            if (Char.IsDigit(textBox5.Text[i + 1]))
                            {
                                if (flag)
                                {
                                    this.operands.Push(new Operand((textBox5.Text[i])));
                                }
                                this.operands.Push(new Operand(Convert.ToInt32(Convert.ToString(this.operands.Pop().value)) * 10 + Convert.ToInt32(Convert.ToString(textBox5.Text[i + 1]))));
                                flag = false;
                                continue;
                            }
                            else 
                            if (textBox5.Text[i + 1] == ',' || textBox5.Text[i + 1] == ')' && Char.IsDigit(textBox5.Text[i]))
                            {
                                flag = true;
                                continue;
                            }
                        }
                    }
                    else if (textBox5.Text[i] == 'C')
                    {
                        if (this.operators.Count == 0)
                        {
                            this.operators.Push(OperatorContainer.FindOperator(textBox5.Text[i]));
                        }
                    }
                    else if (textBox5.Text[i] == 'M')
                    {
                        if (this.operators.Count == 0)
                        {
                            this.operators.Push(OperatorContainer.FindOperator(textBox5.Text[i]));
                        }
                    }
                    else if (textBox5.Text[i] == 'D')
                    {
                        if (this.operators.Count == 0)
                        {
                            this.operators.Push(OperatorContainer.FindOperator(textBox5.Text[i]));
                        }
                    }
                    else if (textBox5.Text[i] == '(')
                    {
                        this.operators.Push(OperatorContainer.FindOperator(textBox5.Text[i]));
                    }
                    else if (textBox5.Text[i] == ')')
                    {
                        do
                        {
                            if (operators.Peek().symbolOperator == '(')
                            {
                                operators.Pop();
                                break;
                            }
                            if (operators.Count == 0)
                            {
                                break;
                            }
                        }
                        while (operators.Peek().symbolOperator != '(');
                    }
                }
                if (operators.Peek() != null)
                {

                    this.SelectingPerformingOperation(operators.Peek());

                }
                else
                {
                    listBox2.Items.Add("Введенной команды " + Convert.ToString(textBox5.Text) + " не существует" + "\n");
                    //MessageBox.Show("Введенной операции не существует");
                }
                try
                {

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                }
            }

        }

        public void Clear()
        {
            Graphics g = Graphics.FromImage(Init.bitmap);
            g.Clear(Color.WhiteSmoke);
            listBox1.Items.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            pictureBox1.Refresh();
            ShapeContainer.Clear();
            i = 0;
            count = 1;
            circleX = 0;
            circleY = 0;
            circleR = 0;
            RecCount = 0;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            label1.Text = "X";
            label2.Text = "Y";
            label3.Text = "W";
            label4.Text = "H";
            comboBox1.SelectedIndex = 0;
            button5.Enabled = true;
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
                    if(f != null)
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
            button5.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)// Draw and AddShape
        {
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            button5.Enabled = true;
            if (comboBox1.SelectedIndex == 0)
            {
                RecCount += 1;
                rectangle = new Rectangle(int.Parse(textBox1.Text), int.Parse(textBox2.Text), int.Parse(textBox3.Text), int.Parse(textBox4.Text),Convert.ToString("Rectangle №" + RecCount.ToString()));
                rectangle.name = "Rectangle №" + RecCount.ToString();
                ShapeContainer.AddFigure(rectangle,rectangle.name);
                button5.Visible = false;
                listBox1.Items.Clear();
                try
                {
                    foreach (Figure figure in ShapeContainer.figureList)
                    {
                        if (figure != null) { }
                        listBox1.Items.Add(rectangle);
                        rectangle.Draw();
                    }
                }
                catch (Exception ex)
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
                this.pointFs = this.pointFs;
                Figure figure = (Figure)listBox1.SelectedItem;
                if (figure == null)
                {
                    MessageBox.Show("Null");
                    throw new Exception();
                }
                if (comboBox1.SelectedIndex == 4 || comboBox1.SelectedIndex == 5) { figure.MoveTo(int.Parse(textBox2.Text), int.Parse(textBox3.Text)); }
                figure.MoveTo(int.Parse(textBox1.Text), int.Parse(textBox2.Text),figure.name);
/*                else if(listBox1.SelectedItem == this.rectangle.name)
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
                }*/
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
                i = 0;
                count = 1;
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
                i = 0;
                count = 1;
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
                i = 0;
                count = 1;
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
                i = 0;
                count = 1;
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
                i = 0;
                count = 1;
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
                textBox2.Text = "";
                textBox3.Text = "";
            }
            if (comboBox1.SelectedIndex == 5)
            {
                triag = false;
                
                label5.Text = "Кол-во точек: 3";
                label1.Text = $"Введите координаты {count}-й точки: ";
                i = 0;
                count = 1;
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
                textBox2.Text = "";
                textBox3.Text = "";
            }
            if (comboBox1.SelectedIndex == 6)
            {
                triag = false;
                
                label1.Text = "X";
                label2.Text = "Y";
                label3.Text = "W";
                label4.Text = "H";
                i = 0;
                count = 1;
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "История команд";
            label1.Visible = true;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button6.Visible = false;
            button5.Visible = false;
            label5.Visible = false;
            comboBox1.Visible = false;

            listBox1.Visible = false;
            listBox2.Visible = true;
            textBox5.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "X";
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button6.Visible = true;
            button5.Visible = true;
            label5.Visible = true;
            comboBox1.Visible = true;

            listBox1.Visible = true;
            listBox2.Visible = false;
            textBox5.Visible = false;
        }
    }
}