using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 水准网平差实验1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double[] H = new double[100];
        double[] h = new double[100];
        double[] S = new double[100];
        int[] StartP = new int[100];
        int[] EndP = new int[100];
        double[] P = new double[100];
        string[] Pname = new string[100];
        double[,] BTPB = new double[100, 100];
        double[] BTPL = new double[100];
        double[] dx = new double[100];
        double[] v = new double[100];
        double pvv;
        string name;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            // 指定打开文本文件（后缀名为txt）
            openDlg.Filter = "文本文件|*.txt";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                // 读出文本文件的所以行
                string[] lines = File.ReadAllLines(openDlg.FileName);
                // 先清空richtextBox1
                richTextBox1.Clear();
                // 在richtextBox1中显示
                foreach (string line in lines)
                {
                    richTextBox1.AppendText(line + Environment.NewLine);
                }
            }
   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int row = richTextBox1.Lines.Length;
            string[] buffer = new string[100];
            string str = richTextBox1.Text;

            String[] str1 = str.Split(new char[2] { ',', '\n' });


            int n0 = int.Parse(textBox1.Text);
 
            Level Calcu = new Level(row-n0-1,row-n0,n0);


            //Dictionary<string, double> Dict = new Dictionary<string, double>();
            //for (int i = 1; i <= n0; i++)   //读取已知点高程
            //{
            //    Pname[i] = str1[2 * i - 2];
            //    H[i] = double.Parse(str1[2 * i - 1]);
            //    Dict.Add(Pname[i], H[i]);
            //}
            //foreach (KeyValuePair<string, double> kvp in Dict)
            //{
            //    if (kvp.Key == str1[2 * n0])
            //        Pname[i] = kvp.Key;
            //}
            for (int i = 1; i <= n0; i++)
            {
                Pname[i] = str1[2 * i - 2];
                H[i] = double.Parse(str1[2 * i - 1]);
                if (Pname[i] == str1[2 * n0])
                     StartP[i]=i;                 
            }

            //foreach (KeyValuePair<string, double> kvp in Dict)
            //{
            //    if(kvp.Key==str1[2*n0])
            //         kvp.Value=
            //}
                //this.richTextBox2.AppendText(kvp.Key.ToString() + ',' + kvp.Value.ToString() + '\n');

            for (int i = 1; i <= row - n0 - 1; i++)
            {            
                h[i] = double.Parse(str1[(n0 + 1) * i + i + 2]);
                S[i] = double.Parse(str1[(n0 + 1) * i + i + 3]);
                
                Pname[i+n0] = str1[(n0 + 1) * i + i + 1];

                //StartP[i] = i+n0-1;

                EndP[i] = i + n0;
             
            }
            ; 
            Calcu.ca_H0(H, h, StartP, EndP);
             Calcu.ca_P(P, S);

            Calcu.ca_BTPB(H, h, StartP, EndP, BTPB, BTPL, P);
            Calcu.ca_dx(H, dx, BTPB, BTPL);
            pvv = Calcu.ca_V(H, h, StartP, EndP, v, P, pvv);
                   
            for (int i = 1; i <= row - n0; i++)
                this.richTextBox2.AppendText(Pname[i]+':'+H[i].ToString() + '\n');

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string path = string.Empty;             //文件路径
                SaveFileDialog SaveFile = new SaveFileDialog();
                SaveFile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                SaveFile.FilterIndex = 1;
                if (SaveFile.ShowDialog() == DialogResult.OK)
                {
                    path = SaveFile.FileName;

                    if (path != string.Empty)
                    {
                        using (System.IO.FileStream file = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                        {
                            using (System.IO.TextWriter text = new System.IO.StreamWriter(file, System.Text.Encoding.Default))
                            {

                                string Ttext = "";
                              
                                text.Write(Ttext);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
   
    }
}
