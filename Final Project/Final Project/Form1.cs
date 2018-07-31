using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Final_Project
{
    public partial class Form1 : Form
    {
        private ArrayList negLib = new ArrayList();
        private ArrayList posLib = new ArrayList();

        Assembly _assembly;
        StreamReader _PosStreamReader;
        StreamReader _NegStreamReader;
        
        public Form1()
        {
            InitializeComponent();
        }

        int score = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("C:/Users/charmaineratcliff/Desktop/test.txt");
            sw.WriteLine(this.textBox1.Text);
            sw.Close();
            MessageBox.Show("It worked");
            textBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            StreamReader srf = new StreamReader(openFileDialog1.FileName);
            textBox1.Text = srf.ReadToEnd();
            srf.Close();
        }

        private void authorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Charmaine Ratcliff");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _assembly = Assembly.GetExecutingAssembly();
                _NegStreamReader = new StreamReader(_assembly.GetManifestResourceStream("Final_Project.negative-words-noheader.txt"));
                _PosStreamReader = new StreamReader(_assembly.GetManifestResourceStream("Final_Project.positive-words-noheader.txt"));

            }
            catch
            {
                MessageBox.Show("Error accessing resources!");
            }

            string negword;
            while ((negword = _NegStreamReader.ReadLine()) != null)
            {
                negLib.Add(negword);
            }
            _NegStreamReader.Close();

            string posword;
            while ((posword = _PosStreamReader.ReadLine()) != null)
            {
                posLib.Add(posword);
            }
            _PosStreamReader.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string raw = this.textBox1.Text;
            char[] sep = { ' ', ',', '.', '?', '!', '\n', '\r'};
            string[] words = raw.Split(sep);
            int nv = 0;
            int pv = 0;
            foreach(string i in words)
            {
                foreach (string j in negLib)
                    if (j == i)
                    {
                        nv++;
                        break;
                    }
                foreach (string k in posLib)
                        if (k == i)
                        {
                            pv++;
                            break;
                        }
             }

            try
            {
                score = ((pv - nv) / (pv + nv));

                if (score > 0)
                {
                    MessageBox.Show("The sentiment score is " + score.ToString() + ", which is positive.");
                }
                else if (score < 0)
                {
                    MessageBox.Show("The sentiment score is " + score.ToString() + ", which is negative.");
                }
                else
                {
                    MessageBox.Show("The sentiment score is " + score.ToString() + ", which is neutral.");
                }
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("Your text does not have any positive or negative words, please try again.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            string raw = this.textBox1.Text;
            char[] sep = { ' ', ',', '.', '?', '!', '\n', '\r' };
            string[] words = raw.Split(sep);

            foreach (string i in words)
            {
                foreach (string j in negLib)
                    if (j == i)
                    {
                        listBox2.Items.Add(j);
                        break;
                    }
                foreach (string k in posLib)
                    if (k == i)
                    {
                        listBox1.Items.Add(k);
                        break;
                    }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            PositiveWordsChart FP = new PositiveWordsChart();

            string raw = this.textBox1.Text;
            char[] sep = { ' ', ',', '.', '?', '!', '\n', '\r' };
            string[] words = raw.Split(sep);

            foreach (string i in words)
            {
                foreach (string j in negLib)
                    if (j == i)
                    {
                        listBox2.Items.Add(j);
                        break;
                    }
                foreach (string k in posLib)
                    if (k == i)
                    {
                        listBox1.Items.Add(k);
                        break;
                    }
            }
            var positivelist = listBox1.Items.Cast<String>().ToList();

            var uniqueword = (from item in positivelist
                              group item by item into itemGroup
                              select String.Format("{0}", itemGroup.Key));

            var uniquecount = (from item in positivelist
                               group item by item into itemGroup
                               select String.Format("{0}", itemGroup.Count()));

            IEnumerable<string> keys = new List<string>(uniqueword);
            IEnumerable<string> count = new List<string>(uniquecount);

            var dict = keys.Zip(count, (k, v) => new { k, v })
                          .ToDictionary(x => x.k, x => x.v);

            foreach (KeyValuePair<string, string> word in dict)
            {
                FP.chart1.Series[0].Points.AddXY(word.Key, word.Value);
            }
            FP.Show();
            listBox1.Items.Clear();
            listBox2.Items.Clear();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            this.listBox1.Items.Clear();
            this.listBox2.Items.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            NegativeWordsChart FN = new NegativeWordsChart();

            string raw = this.textBox1.Text;
            char[] sep = { ' ', ',', '.', '?', '!', '\n', '\r' };
            string[] words = raw.Split(sep);

            foreach (string i in words)
            {
                foreach (string j in negLib)
                    if (j == i)
                    {
                        listBox2.Items.Add(j);
                        break;
                    }
                foreach (string k in posLib)
                    if (k == i)
                    {
                        listBox1.Items.Add(k);
                        break;
                    }

            }
            var negativelist = listBox2.Items.Cast<String>().ToList();

            var uniqueword = (from item in negativelist
                              group item by item into itemGroup
                              select String.Format("{0}", itemGroup.Key));

            var uniquecount = (from item in negativelist
                               group item by item into itemGroup
                               select String.Format("{0}", itemGroup.Count()));

            IEnumerable<string> keys = new List<string>(uniqueword);
            IEnumerable<string> count = new List<string>(uniquecount);

            var dict = keys.Zip(count, (k, v) => new { k, v })
                          .ToDictionary(x => x.k, x => x.v);

            foreach (KeyValuePair<string, string> word in dict)
            {
                FN.chart1.Series[0].Points.AddXY(word.Key, word.Value);
            }
            FN.Show();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            string raw = this.textBox1.Text;
            char[] sep = { ' ', ',', '.', '?', '!', '\n', '\r' };
            string[] words = raw.Split(sep);

            foreach (string i in words)
            {
                foreach (string j in negLib)
                    if (j == i)
                    {
                        listBox2.Items.Add(j);
                        break;
                    }
                foreach (string k in posLib)
                    if (k == i)
                    {
                        listBox1.Items.Add(k);
                        break;
                    }

            }
        }
    }
}
