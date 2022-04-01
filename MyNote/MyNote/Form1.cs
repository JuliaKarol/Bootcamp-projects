using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNote
{
    
    public partial class Form1 : Form
    {
        const string ProgramName = "JulNote";
        private bool isModified;
        private DateTime StartTime = DateTime.Now;
        public Form1()
        {
            InitializeComponent();
            this.Text = ProgramName;
            toolStripComboBox1.SelectedIndex = 0;
            //splitContainer1.Panel1Collapsed = true;
            StartTime = DateTime.Now;
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            int lines = richTextBox1.Lines.Length;
            int letters = richTextBox1.Text.Length;
            toolStripStatusLabel2.Text = "Akapitów " + lines + " liter " + letters;
            isModified = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            richTextBox1.LoadFile(openFileDialog1.FileName);
            this.Text = ProgramName + " - " + openFileDialog1.SafeFileName;
            isModified = false;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
                richTextBox1.SelectionFont = fontDialog1.Font;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                richTextBox1.SelectionColor = colorDialog1.Color;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                richTextBox1.SelectionBackColor = colorDialog1.Color;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            richTextBox1.SaveFile(saveFileDialog1.FileName);
            this.Text = ProgramName + " - " + saveFileDialog1.FileName;
            isModified = false;
        }

        private void New_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length == 0) return;
            if (MessageBox.Show("Czy chcesz wyczyścić notatkę?", "Jesteś pewien?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            richTextBox1.Clear();
            this.Text = ProgramName;
            isModified = false;
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            About okno = new About();
            //okno.ShowDialog(); //blokuje do zamknięcia; show nie
            okno.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void kopiujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void wytnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void wklejToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void ponówToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void cofnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() != DialogResult.OK) return;
            IDataObject org = Clipboard.GetDataObject();
            Image img = Image.FromFile(openFileDialog2.FileName);
            Clipboard.SetImage(img);
            richTextBox1.Paste();
            Clipboard.SetDataObject(org);
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int sec = (int)(DateTime.Now - StartTime).TotalSeconds;
            toolStripStatusLabel1.Text = "[Pracujesz " + sec + " sekund]";
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(textBox1.Text);
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length == 0)
            {
                MessageBox.Show("Zaznacz tekst do wyszukania");
                return;
            }
            string text = richTextBox1.SelectedText.Replace("", "%20");
            splitContainer1.Panel2Collapsed = false;
            int ind = toolStripComboBox1.SelectedIndex;
            if (ind == 0) webBrowser1.Navigate("https://wikipedia.org/wiki/" + text);
            if (ind == 1) webBrowser1.Navigate("https://translate.google.pl/?hl=pl#auto/pl/" + text);
            if (ind == 2) webBrowser1.Navigate("https://google.com" + text);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isModified) return;
            DialogResult r = MessageBox.Show("Czy zapisać przed wyjściem?", "Zapisać?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (r == DialogResult.Cancel) e.Cancel = true;
            if (r == DialogResult.Yes) toolStripButton3_Click(sender, e);
        }
    }
}
