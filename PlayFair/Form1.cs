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
        Button[,] arrayMatrix = new Button[6, 6];
        List<string> alphabet = new List<string>();
        public Form1()
        {
            InitializeComponent();
            createMatrix(5);
            textBox3.KeyPress += new KeyPressEventHandler(matrix5_KeyPress);// default rule for matrix 5x5
            //check khi onclick radio btn
            radioButton1.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            radioButton2.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
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
           // MessageBox.Show(kq);
            return kq;
        }
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
        void modifyMatrix(Button[,] a,int x,int row,int col,string s)
        {
            bool firstLoop = false;
            int dem = 0;
            List<string> newList = new List<string>(alphabet);
            foreach (char c in s)
                {
                newList.Remove(c.ToString());
                }
            for (int i = row; i < x; i++)
            {
                if (!firstLoop)
                {
                    for (int j = col; j < x; j++)
                    {
                        a[i, j].Text = newList.ElementAt(dem++);
                        //MessageBox.Show(a[i, j].Text + "\n(row,col): " + i + "," + j);
                    }
                    firstLoop = true;
                }
                else
                {
                    for (int j = 0; j < x; j++)
                    {
                        a[i, j].Text = newList.ElementAt(dem++);
                       // MessageBox.Show(a[i, j].Text + "\n(row,col): " + i + "," + j);
                    }
                }
            }

        }

        string plainTextModify(int x)
        {
            if (textBox1.Text != string.Empty/* && textBox3.Text != string.Empty*/)
            {
                //to uppercase 
                string eString = textBox1.Text.ToUpper();
                eString = RemoveUnicode(eString);
                if (x == 5) {
                    eString = Regex.Replace(eString,@"[\W\d]", "");//replace all num and non-letter
                    eString = Regex.Replace(eString, @"[jJ]", "I");//replace j to i
                }
                if (x == 6) { eString = Regex.Replace(eString, @"[\W]", ""); }//replace all non-letter
                //odd string
                if(eString.Length %2 != 0)
                {
                    eString += 'X';
                }
                return eString;
            }
            return "";
        }

        void replaceOnMatrix(int x,string a)
        {
            a = distinctString(a);
            int size = 0;
            int h = 0, t = 0;
            int indexCol=0;
            int indexRow=0;
            while (size < a.Length)
            {
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        if (t >= x) { t = 0; h++; }
                        if (size >= a.Length) break;
                        string temp1 = a[size].ToString();
                        foreach (string temp2 in alphabet)
                        {
                           if (temp1.Equals(temp2))
                            {
                                 indexCol = alphabet.IndexOf(temp2);//cot se bi xoa
                                indexRow = Convert.ToInt32(char.Parse(temp2));
                                arrayMatrix[h, t++].Text = temp1;
                                while (indexCol > (x-1)) //get index Column
                                {
                                    indexCol = indexCol - x;
                                }
                               switch (x)
                                {
                                    case 5:
                                        {
                                            if(indexRow>=65 && indexRow <= 69) { indexRow = 0; }
                                            if (indexRow >= 70 && indexRow <= 75) { indexRow = 1; }//k xet j
                                            if (indexRow >= 76 && indexRow <= 80) { indexRow = 2; }
                                            if (indexRow >= 81 && indexRow <= 85) { indexRow = 3; }
                                            if (indexRow >= 86) { indexRow = 4; }
                                            break;
                                        }
                                    case 6:
                                        {
                                            if (indexRow >= 65 && indexRow <=70) { indexRow = 0; }
                                            if (indexRow >= 71 && indexRow <= 76) { indexRow = 1; }
                                            if (indexRow >= 77 && indexRow <= 82) { indexRow = 2; }
                                            if (indexRow >= 83 && indexRow <= 88) { indexRow = 3; }
                                            if (indexRow >= 89 || indexRow >=48 && indexRow<52) { indexRow = 4; }
                                            if(indexRow >= 52 && indexRow <=57) { indexRow = 5; }
                                            break;
                                        }
                                }
                                size++;
                            }
                        }
                    }
                }
            }
            modifyMatrix(arrayMatrix, x, h, t, a);
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
                    textBox3.Text = "";
                    textBox2.Text = "";
                    textBox3.KeyPress += new KeyPressEventHandler(matrix5_KeyPress);
                }
                else if (((RadioButton)sender) == radioButton2)
                {
                    panel1.Controls.Clear();
                    createMatrix(6);
                    checkString(6);
                    check = 6;
                    textBox3.Text = "";
                    textBox2.Text = "";
                    textBox3.KeyPress += new KeyPressEventHandler(matrix6_KeyPress);
                }
            }
        }


        void createMatrix(int x)
        {
            int charNum = 65;
            int offset = 0;
            alphabet = new List<string>();
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
                        alphabet.Add(btn.Text);
                    }
                    else if(charNum == 74 && x == 5)
                    {
                        charNum = 75;
                        btn.Text = Encoding.ASCII.GetString(new byte[] { (byte)charNum });
                        alphabet.Add(btn.Text);
                    }
                    else {
                        btn.Text = Encoding.ASCII.GetString(new byte[] { (byte)charNum });
                        alphabet.Add(btn.Text);
                    }
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
                            textBox3.Text = Regex.Replace(textBox3.Text, @"[0-9jJ]", "");
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
            textBox3.Text = RemoveUnicode(textBox3.Text);
            textBox3.MaxLength = 36;
            textBox3.SelectionStart = textBox3.Text.Length;//cursor to bottom
            textBox3.Text = textBox3.Text.ToUpper();
            
        }

        void matrix5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //only word except j
            if ((new Regex(@"[^jJ\W\d]")).IsMatch(e.KeyChar.ToString()))
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

        int checkRow(char a,int x)
        {
            string str = a.ToString();
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    if(arrayMatrix[i,j].Text == str)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        int checkCol(char a, int x)
        {
            string str = a.ToString();
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    if (arrayMatrix[i, j].Text == str)
                    {
                        return j;
                    }
                }
            }
            return -1;
        }
        string Encrypt(int x,string a)
        {
            string kq = "";
            int r0 = checkRow(a[0],x);
            int c0 = checkCol(a[0],x);
            int r1 = checkRow(a[1],x);
            int c1 = checkCol(a[1],x);
            //If both the letters are in the same column: Take the letter below each one (going back to the top if at the bottom).
            //MessageBox.Show(a+"(" + r0 + "," + c0 + ")" + "\n" + "(" + r1 + "," + c1 + ")");
            if (c0 == c1)
            {
                if ((r0 + 1) >= x) { r0 = -1; }
                if ((r1 + 1) >= x) { r1 = -1; }
                kq += arrayMatrix[r0 + 1, c0].Text;
                kq += arrayMatrix[r1 + 1, c0].Text;
               // MessageBox.Show(arrayMatrix[r0 + 1, c0].Text+ arrayMatrix[r1 + 1, c0].Text);
            }
            //If both the letters are in the same row: Take the letter to the right of each one 
            //(going back to the leftmost if at the rightmost position).
            else if (r0 == r1)
            {
                if ((c0 + 1) >= x) { c0 = -1; }
                if ((c1 + 1) >= x) { c1 = -1; }
                kq += arrayMatrix[r0 , c0+1].Text;
                kq += arrayMatrix[r0, c1+1].Text;
               // MessageBox.Show(arrayMatrix[r0, c0 + 1].Text+arrayMatrix[r0, c1 + 1].Text);
            }
            //If neither of the above rules is true: Form a rectangle with the two letters and take the 
            //letters on the horizontal opposite corner of the rectangle.
            else
            {
                //chieu dai hcn
                int hC = Math.Abs(c1 - c0);
                //chieu rong hcn
                //int hC = Math.Abs(c1 - c0);
                if((c0 + hC) >= x) {
                    c0 = -c0;
                    kq += arrayMatrix[Math.Abs(r0), Math.Abs(c0 + hC)].Text;
                    kq += arrayMatrix[Math.Abs(r1), Math.Abs(c1+hC)].Text;
                }
                else if ((c1 + hC) >= x)
                {
                    c1 = -c1;
                    kq += arrayMatrix[Math.Abs(r0), Math.Abs(c0 + hC)].Text;
                    kq += arrayMatrix[Math.Abs(r1), Math.Abs(c1 + hC)].Text;
                }
                else
                {
                    kq += arrayMatrix[Math.Abs(r0), Math.Abs(c0 + hC)].Text;
                    kq += arrayMatrix[Math.Abs(r1), Math.Abs(c1 - hC)].Text;
                }
                //MessageBox.Show(arrayMatrix[Math.Abs(r0 + hR), Math.Abs(c0 + hC)].Text+arrayMatrix[Math.Abs(r1 + hR), Math.Abs(c1 + hC)].Text);
            }
            return kq;
        }

        string Decrypt(int x, string a)
        {
            string kq = "";
            int r0 = checkRow(a[0], x);
            int c0 = checkCol(a[0], x);
            int r1 = checkRow(a[1], x);
            int c1 = checkCol(a[1], x);
            //If both the letters are in the same column: Take the letter below each one (going back to the top if at the bottom).
            //MessageBox.Show(a+"(" + r0 + "," + c0 + ")" + "\n" + "(" + r1 + "," + c1 + ")");
            if (c0 == c1)
            {
                if ((r0 - 1) <0) { r0 = x; }
                if ((r1 - 1) <0) { r1 = x; }
                kq += arrayMatrix[r0 - 1, c0].Text;
                kq += arrayMatrix[r1 - 1, c0].Text;
                // MessageBox.Show(arrayMatrix[r0 + 1, c0].Text+ arrayMatrix[r1 + 1, c0].Text);
            }
            //If both the letters are in the same row: Take the letter to the right of each one 
            //(going back to the leftmost if at the rightmost position).
            else if (r0 == r1)
            {
                if ((c0 - 1) <0) { c0 = x; }
                if ((c1 - 1) <0) { c1 = x; }
                kq += arrayMatrix[r0, c0 - 1].Text;
                kq += arrayMatrix[r0, c1 - 1].Text;
                // MessageBox.Show(arrayMatrix[r0, c0 + 1].Text+arrayMatrix[r0, c1 + 1].Text);
            }
            //If neither of the above rules is true: Form a rectangle with the two letters and take the 
            //letters on the horizontal opposite corner of the rectangle.
            else
            {
                //chieu dai hcn
                int hC = Math.Abs(c1 - c0);
                //chieu rong hcn
                //int hC = Math.Abs(c1 - c0);
                if ((c0 + hC) >= x)
                {
                    c0 = -c0;
                    kq += arrayMatrix[Math.Abs(r0), Math.Abs(c0 + hC)].Text;
                    kq += arrayMatrix[Math.Abs(r1), Math.Abs(c1 + hC)].Text;
                }
                else if ((c1 + hC) >= x)
                {
                    c1 = -c1;
                    kq += arrayMatrix[Math.Abs(r0), Math.Abs(c0 + hC)].Text;
                    kq += arrayMatrix[Math.Abs(r1), Math.Abs(c1 + hC)].Text;
                }
                else
                {
                    kq += arrayMatrix[Math.Abs(r0), Math.Abs(c0 - hC)].Text;
                    kq += arrayMatrix[Math.Abs(r1), Math.Abs(c1 + hC)].Text;
                }
                //MessageBox.Show(arrayMatrix[Math.Abs(r0 + hR), Math.Abs(c0 + hC)].Text+arrayMatrix[Math.Abs(r1 + hR), Math.Abs(c1 + hC)].Text);
            }
            return kq;
        }

        List<string> splitPair(string text)
        {
            int curr = 0;
            List<string> cypherText = new List<string>();
            while (curr != text.Length)
            {
                string s = "";
                int dem = 0;
                for (; curr < text.Length; curr++)
                {
                    if (dem >= 2) break;
                    s += text[curr];
                    dem++;
                }
                cypherText.Add(s);
            }
            return cypherText;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //chinh sua cyphertext ve dung dang
            string text = plainTextModify(check);
            //chia nho theo pair cyphertext
            List<string> cypherText = splitPair(text);
            string t = "";
            foreach(string a in cypherText)
            {
               t+=Encrypt(check,a);
            }
            textBox2.Text = t;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //chinh sua cyphertext ve dung dang
            string text = plainTextModify(check);
            //chia nho theo pair cyphertext
            List<string> cypherText = splitPair(text);
            string t = "";
            foreach (string a in cypherText)
            {
                t += Decrypt(check, a);
            }
            textBox2.Text = t;
        }
    }
}
