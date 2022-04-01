using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace sqlForms
{
    public partial class Form1 : Form { BazaSklepu db;
    
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void Load_Click(object sender, EventArgs e)
        {
            db = new BazaSklepu();
            listBox1.DataSource = db.Kategoria.Select(x => x.Nazwa).ToList();
        }
        private void LoadProducts()
        {
            string wybranaKategoria = (string)listBox1.SelectedItem;
            dataGridView1.DataSource = db.Produkt.Where(x => x.Kategoria.Nazwa == wybranaKategoria).ToList();
        }
        private void listBox1_SelectedIndexChanged (object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void AddNew_Click(object sender, EventArgs e)
        {
            try
            {
            string wybranaKategoria = (string)listBox1.SelectedItem;
            Produkt Nowy = new Produkt()
            {
                Nazwa = "Wpisz nazwę",
                Cena = 0,
                Kategoria = db.Kategoria.First(x => x.Nazwa == wybranaKategoria)

            };
            db.Produkt.Add(Nowy);
            db.SaveChanges();
            LoadProducts();
            }
            catch (IOException)
            {
                //złe typy danych
                throw;
            }
            
        }

        private void delete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow wiersz in dataGridView1.SelectedRows)
            {
                Produkt p = (Produkt)wiersz.DataBoundItem;
                db.Produkt.Remove(p);
            }
            try
            {
             db.SaveChanges();
            }
            catch (Exception)
            {
                //logika przy tym kiedy brak rekordu w bazie, zmiany bazie w  międzyczasie (DbUpdateConcurrencyException)
                throw;
            }
            LoadProducts();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            db.SaveChanges();
        }
    }
}
