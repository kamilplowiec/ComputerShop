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
    public partial class Login : Form
    {
        Baza baza = new Baza();

        public Osoba Zalogowany { get; set; }

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!baza.Database.Exists())
            {
                MessageBox.Show("Brak połączenia z bazą danych!");
                return;
            }

            var osoba = baza.Osoba.FirstOrDefault(x => x.Login == textBox1.Text && x.Haslo == textBox2.Text);
            if (osoba != null)
            {
                Zalogowany = osoba;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Niepoprawne dane logowania!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EdycjaOsoby edycja = new EdycjaOsoby();
            edycja.ShowDialog(this);
        }
    }
}
