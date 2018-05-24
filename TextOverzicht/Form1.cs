using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace TextOverzicht
{


    public partial class Form1 : Form
    {
        public class CTuple
        {
            public CTuple(char item1, int item2)
            {
                Item1 = item1;
                Item2 = item2;
            }
            public char Item1 { get; set; }
            public int Item2 { get; set; }
        }

        int chars = 0;

        List<CTuple> keyList = new List<CTuple>();


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;

            if (!openFileDialog1.CheckFileExists)
            {
                return;
            }
            if (textBox1.Text.Length < 1)
            {
                return;
            }

            keyList = new List<CTuple>();
            chars = 0;
            ReadFile();
            DisplayText();
        }

        private void ReadFile()
        {
            var reader = new StreamReader(textBox1.Text);

            char read;    
            while (!reader.EndOfStream)
            {
                read = (char)reader.Read();
                chars++;

                bool found = false;
                for (int i = 0; i < keyList.Count; i++)
                {
                    if (keyList[i].Item1 == read)
                    {
                        keyList[i].Item2 += 1;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    keyList.Add(new CTuple(read,1));
                }
            }
        }

        private decimal Proc(decimal a, decimal b)
        {

            return (b / a) * 100;
        }

        private string Stretch(string target, int size)
        {
            if (target.Length > size)
            {
                return target.Substring(0, size);
            }
            else
            {
                while (target.Length < size)
                {
                    target += " ";
                }
                return target;
            }
        }

        private string SStretch(string target, int size)
        {
            if (target.Length > size)
            {
                return target.Substring(0, size-1) + ".";
            }
            else
            {
                while (target.Length < size)
                {
                    target += " ";
                }
                return target;
            }
        }


        private void DisplayText()
        {
            listBox1.Items.Clear();

            listBox1.Items.Add("File name:  " + textBox1.Text);
            listBox1.Items.Add("Characters: " + chars);
            listBox1.Items.Add("");
            listBox1.Items.Add(" |Percent|  |Amount|  |Character|");


            keyList.Sort((x, y) => y.Item2.CompareTo(x.Item2));

            foreach (var c in keyList)
            {
                listBox1.Items.Add(

                    " " + Stretch(Convert.ToString(Proc(chars, c.Item2)), 5) + "%   " +

                    "  (" + SStretch( Convert.ToString(c.Item2) , 6) + ")" +

                    "  \"" + c.Item1 + "\""
                    );
            }
        }
    }
}
