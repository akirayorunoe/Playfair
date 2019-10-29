using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayFair
{
    public partial class Form1 : Form
    {
        int check=5;
        string tempString="";
        Button[,] arrayMatrix = new Button[6, 6];
        public Form1()
        {
            InitializeComponent();
            createMatrix(5);
            textBox3.KeyPress += new KeyPressEventHandler(matrix5_KeyPress);// default rule for matrix 5x5
            //check khi onclick radio btn
            radioButton1.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            radioButton2.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            //for(int i=0;i<5;i++)
            //{
            //    for(int j=0;j<5;j++)
            //    {
            //        s += arrayMatrix[i, j].Text.ToString();
            //    }
            //    s += "\n";
            //}
            //MessageBox.Show(s);
        }
        string distinctString(string a)
        {
            int cNum = 0;
            string kq = "";
            List<char> list = new List<char>();
            foreach (int c in a)
            {
                list.Add(a[cNum++]);

            }
            List<char> distinct = list.Distinct().ToList();
            kq = string.Join("", distinct.ToArray());
            MessageBox.Show(kq);
            return kq;
        }
        void replaceOnMatrix(int x,string a)
        {
            distinctString(a);
            //int size = 0;
            //int h = 0, t = 0;
            //while (size <= a.Length)
            //{
            //    for (int i = 0; i < x; i++)
            //    {
            //        for (int j = 0; j < x; j++)
            //        {
            //            if (t >= x) { t = 0; h++; }
            //            string temp1 = a[size].ToString();
            //            string temp2 = arrayMatrix[i, j].Text;
            //            MessageBox.Show(temp1+","+temp2);
            //            if (temp1==temp2)
            //            {
            //                tempString += arrayMatrix[i, j].Text;
            //                arrayMatrix[h, t++].Text = tempString;
            //                size++;
            //            }
            //        }
            //    }
            //}
        }
        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            // Do stuff only if the radio button is checked (or the action will run twice).
            if (((RadioButton)sender).Checked)
            {
                if (((RadioButton)sender) == radioButton1)
                {
                    //clear panel
                    panel1.Controls.Clear();
                    createMatrix(5);
                    checkString(5);
                    check = 5;
                   textBox3.KeyPress += new KeyPressEventHandler(matrix5_KeyPress);
                }
                else if (((RadioButton)sender) == radioButton2)
                {
                    panel1.Controls.Clear();
                    createMatrix(6);
                    checkString(6);
                    check = 6;
                    textBox3.KeyPress += new KeyPressEventHandler(matrix6_KeyPress);
                }
            }
        }


        void createMatrix(int x)
        {
            int charNum = 65;
            int offset = 0;
            for (int i = 0; i < x; i++)
            {
                //Moi lan xuong dong tao oldBtn cach oldBtn old 30
                Button oldBtn = new Button() { Width = 0, Height = 0, Location = new Point(0, offset * 30) };
                for (int j = 0; j < x; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Cons.MATRIX_WIDTH,
                        Height = Cons.MATRIX_HEIGHT,
                        Location = new Point(oldBtn.Location.X + oldBtn.Width, oldBtn.Location.Y)//toa do btn moi = X old + width old
                    };
                    if (charNum > 90)
                    {
                        charNum = 48;//0
                        btn.Text = Encoding.ASCII.GetString(new byte[] { (byte)charNum });
                    }
                    else { btn.Text = Encoding.ASCII.GetString(new byte[] { (byte)charNum }); }
                    arrayMatrix[i,j] = btn;
                    //panel add collection
                    panel1.Controls.Add(btn);
                    //reassign button old = btn vua tao
                    oldBtn = btn;
                    charNum++;
                }
                offset++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string temp = openFileDialog1.FileName;
                textBox1.Text = File.ReadAllText(temp);
            }
        }
        //Kiem tra key co number co o matrix 6x6 k
        void checkString(int x)
        {
            if (textBox3.Text != string.Empty)
            {
                switch (x)
                {
                    case 5:
                        {
                            textBox3.Text = Regex.Replace(textBox3.Text, @"[0-9]", "");
                            break;
                        }
                    case 6:
                        {
                            break;
                        }
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, textBox2.Text);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.MaxLength = 36;
            textBox3.SelectionStart = textBox3.Text.Length;//cursor to bottom
            textBox3.Text = textBox3.Text.ToUpper();
            
        }

        void matrix5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //only word
            if ((new Regex(@"^[a-zA-Z]+$")).IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == (char)8)//accept press backspace
            { e.Handled = false; }
            else
            { e.Handled = true; }
        }

        void matrix6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //only word and number
            if ((new Regex(@"^\w+|[\d]")).IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == (char)8)//accept press backspace
            { e.Handled = false; }
            else
                e.Handled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != string.Empty)
                replaceOnMatrix(check, textBox3.Text);
        }
    }
}
