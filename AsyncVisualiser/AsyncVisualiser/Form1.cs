using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace AsyncVisualiser
{
    public partial class Form1 : Form
    {
        ManualResetEvent pauza = new ManualResetEvent(true);
        ManualResetEvent end = new ManualResetEvent(false);
        
        public void AppendTextBox(string value)
        {
         if (InvokeRequired)
         {
            this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
            // Gets executed on a seperate thread and 
             // doesn't block the UI while sleeping
             return;
         }
          textBox1.Text += value;
        }
     public void Work(string value)
        {
            
            while (true)
            {
                pauza.WaitOne();
                if (end.WaitOne(0))
                { 

                break;
                
                }
                AppendTextBox(value);
                Thread.Sleep(1000);
            }
        }    
        
        public Form1()
        {
            InitializeComponent();           
        }
          
        private void Form1_Load(object sender, EventArgs e){}
        private void button1_Click(object sender, EventArgs e)
        {
            new Thread(() => Work("1")).Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pauza.Reset();  
        }

        private void button7_Click(object sender, EventArgs e)
        {
            end.Set();
        }

        private void button4_Click(object sender, EventArgs e)//add0
        {
            new Thread(() => Work("0")).Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pauza.Set();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void saveTotxt_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Save file";
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string name = saveFileDialog1.FileName;
                File.WriteAllText(name, textBox1.Text);
                textBox1.Text = saveFileDialog1.FileName;
            }
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button1, "App created as a test for ManualResetEvent, by JKarol.");
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            
        }
    }
}

