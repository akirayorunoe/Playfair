using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
                }
                else if (((RadioButton)sender) == radioButton2)
                {
                    panel1.Controls.Clear();
                    createMatrix(6);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        
       
        void createMatrix(int x)
        {
            int offset = 0;
            for(int i=0;i< x;i++)
            {
                //Moi lan xuong dong tao oldBtn cach oldBtn old 30
                Button oldBtn = new Button() {Width=0,Height=0,Location = new Point( 0, offset*30 ) };
                for (int j = 0; j < x; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Cons.MATRIX_WIDTH,
                        Height = Cons.MATRIX_HEIGHT,
                        Location = new Point(oldBtn.Location.X + oldBtn.Width, oldBtn.Location.Y)//toa do btn moi = X old + width old
                    };
                    //panel add collection
                    panel1.Controls.Add(btn);
                    //reassign button old = btn vua tao
                    oldBtn = btn;
                }
                offset++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                string temp = openFileDialog1.FileName;
                textBox1.Text=File.ReadAllText(temp);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, textBox2.Text);
            }
        }
    }
}
