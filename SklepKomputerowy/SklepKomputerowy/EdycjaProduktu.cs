using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SklepKomputerowy
{
    public partial class EdycjaProduktu : Form
    {
        Baza baza = new Baza();

        Produkt produkt;

        public EdycjaProduktu(int id = 0)
        {
            InitializeComponent();

            comboBox1.Items.AddRange(Enum.GetNames(typeof(Kategoria)).ToArray());

            produkt = new Produkt();

            if (id > 0)
                produkt = baza.Produkt.FirstOrDefault(x => x.Id == id);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            produkt.Nazwa = textBox1.Text;
            produkt.TypProduktu = (int)Enum.Parse(typeof(Kategoria), comboBox1.SelectedItem.ToString());

            int ilosc;
            int.TryParse(textBox2.Text, out ilosc);
            produkt.Ilosc = ilosc;

            decimal cena;
            decimal.TryParse(textBox3.Text, out cena);
            produkt.Cena = cena;

            if (produkt.Id == 0)
                baza.Produkt.Add(produkt);

            baza.SaveChanges();

            DialogResult = DialogResult.OK;
        }

        private void EdycjaProduktu_Shown(object sender, EventArgs e)
        {
            if(produkt.Id > 0)
            {
                textBox1.Text = produkt.Nazwa;
                comboBox1.SelectedItem = ((Kategoria)produkt.TypProduktu).ToString();
                textBox2.Text = produkt.Ilosc.ToString();
                textBox3.Text = produkt.Cena.ToString();
            }
        }
    }
}
