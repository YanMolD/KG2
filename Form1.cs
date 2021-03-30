using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Dots.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        }

        private Graphics G;

        private List<PointF> Arr = new List<PointF>();

        private Pen Gpen = new Pen(Color.Red);

        private Pen Dots = new Pen(Color.Blue);

        private int Fuctorial(int n)
        {
            int res = 1;
            for (int i = 1; i <= n; i++)
                res *= i;
            return res;
        }

        private float polinom(int i, int n, float t)
        {
            return (Fuctorial(n) / (Fuctorial(i) * Fuctorial(n - i))) *
                (float)Math.Pow(t, i) * (float)Math.Pow(1 - t, n - i);
        }

        private void Draw()
        {
            int j = 0;
            float step = 0.01f;

            PointF[] result = new PointF[101];

            for (float t = 1; t > 0; t -= step)
            {
                float ytmp = 0;
                float xtmp = 0;
                for (int i = 0; i < Arr.Count; i++)
                {
                    float b = polinom(i, Arr.Count - 1, t);
                    xtmp += Arr[i].X * b;
                    ytmp += Arr[i].Y * b;
                }
                result[j] = new PointF(xtmp, ytmp);
                j++;
            }
            for (int i = j; i < 101; i++)
                result[i] = result[j - 1];
            G.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);
            G.ScaleTransform(1, -1);
            pictureBox1.Refresh();
            G.DrawLines(Gpen, result);
            G.DrawLines(Dots, Arr.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox.Items.Count >= 3)
            {
                G = Graphics.FromHwnd(pictureBox1.Handle);
                G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Draw();
            }
            else
                MessageBox.Show("Недостаточно точек", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBoxX.Text != "") && Regex.IsMatch(textBoxX.Text, @"^[-+]?[\d]*[,]?[0-9]+(?:[eE][-+]?[\d]+)?$") &&
                (textBoxY.Text != "") && Regex.IsMatch(textBoxY.Text, @"^[-+]?[\d]*[,]?[0-9]+(?:[eE][-+]?[\d]+)?$"))
            {
                PointF newPoint = new PointF(float.Parse(textBoxX.Text), float.Parse(textBoxY.Text));
                listBox.Items.Add(newPoint);
                Arr.Add(newPoint);
            }
            else
                MessageBox.Show("Ошибка при вводе данных", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex >= 0)
            {
                Arr.RemoveAt(listBox.SelectedIndex);
                listBox.Items.RemoveAt(listBox.SelectedIndex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex >= 0)
            {
                textBoxX.Text = Convert.ToString(Arr.ElementAt(listBox.SelectedIndex).X);
                textBoxY.Text = Convert.ToString(Arr.ElementAt(listBox.SelectedIndex).Y);
                buttonChange.Visible = false;
                buttonAdd.Visible = false;
                buttonDelete.Enabled = false;
                buttonDraw.Enabled = false;
                buttonAccept.Visible = true;
                buttonDecline.Visible = true;
                listBox.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if ((textBoxX.Text != "") && Regex.IsMatch(textBoxX.Text, @"^[-+]?[\d]*[,]?[0-9]+(?:[eE][-+]?[\d]+)?$") &&
                (textBoxY.Text != "") && Regex.IsMatch(textBoxY.Text, @"^[-+]?[\d]*[,]?[0-9]+(?:[eE][-+]?[\d]+)?$"))
            {
                PointF newPoint = new PointF(float.Parse(textBoxX.Text), float.Parse(textBoxY.Text));

                Arr.RemoveAt(listBox.SelectedIndex);
                Arr.Insert(listBox.SelectedIndex, newPoint);
                listBox.Items.Insert(listBox.SelectedIndex, newPoint);
                listBox.Items.RemoveAt(listBox.SelectedIndex);
                listBox.Refresh();
                buttonChange.Visible = true;
                buttonAdd.Visible = true;
                buttonDelete.Enabled = true;
                buttonDraw.Enabled = true;
                buttonAccept.Visible = false;
                buttonDecline.Visible = false;
                listBox.Enabled = true;
            }
            else
                MessageBox.Show("Ошибка при вводе данных", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBoxX.Text = "";
            textBoxY.Text = "";
            buttonChange.Visible = true;
            buttonAdd.Visible = true;
            buttonDelete.Enabled = true;
            buttonDraw.Enabled = true;
            buttonAccept.Visible = false;
            buttonDecline.Visible = false;
            listBox.Enabled = true;
        }
    }
}