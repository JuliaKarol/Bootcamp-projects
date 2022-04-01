using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncFinal
{
    public partial class Form1 : Form
    {
       CancellationTokenSource cts = new CancellationTokenSource();
       Task<double> ObliczAsync(int N, CancellationToken t)
        {
            t.ThrowIfCancellationRequested();//dej with cancellation
            return Task.Run(() =>
            {
               // List<int> liczby = Enumerable.Range(1, N).ToList();
                //return liczby.Select(a => Math.Sqrt(a)).Sum();
                return Enumerable.Range(1, N).Select(a => Math.Sqrt(a)).Sum();
                
            });
        }
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string a = textBox2.Text;
            await Task.Delay(int.Parse(textBox1.Text) * 1000);
            MessageBox.Show(a);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int setTime = int.Parse(textBox1.Text) * 1000;
            }
            catch (System.FormatException)
            {
                MessageBox.Show("wpisz liczbę");
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            double z = await ObliczAsync(int.Parse(textBox1.Text), cts.Token);

            MessageBox.Show("wynik to " + z);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }
    }
}
