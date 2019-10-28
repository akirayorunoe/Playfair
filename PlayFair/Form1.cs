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
        public Form1()
        {
            InitializeComponent();
            createMatrix(5);
            textBox3.KeyPress += new KeyPressEventHandler(matrix5_KeyPress);// default rule for matrix 5x5
            //check khi onclick radio btn
            radioButton1.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            radioButton2.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
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
                   textBox3.KeyPress += new KeyPressEventHandler(matrix5_KeyPress);
                }
                else if (((RadioButton)sender) == radioButton2)
                {
                    panel1.Controls.Clear();
                    createMatrix(6);
                    checkString(6);
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
             e.Handled = true;
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
    }
}
