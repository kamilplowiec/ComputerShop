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
    public partial class EdycjaOsoby : Form
    {
        Baza baza = new Baza();
        private Osoba osoba { get; set; }

        public EdycjaOsoby(int id = 0)
        {
            InitializeComponent();

            osoba = new Osoba();

            if (id > 0)
                osoba = baza.Osoba.FirstOrDefault(x => x.Id == id);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Pola: Nazwa, Adres muszą być uzupełnione!");
                return;
            }

            if (textBox4.Text.Length > 30)
            {
                MessageBox.Show("Pole Telefon jest za długie!");
                return;
            }

            if (textBox5.Text.Length > 50)
            {
                MessageBox.Show("Pole Email jest za długie!");
                return;
            }
            if (string.IsNullOrEmpty(textBox7.Text) || string.IsNullOrEmpty(textBox8.Text))
            {
                MessageBox.Show("Pola: Login i Hasło musza byc uzupełnione!");
                return;
            }

            if(baza.Osoba.FirstOrDefault(x => x.Login == textBox7.Text && x.Id != osoba.Id) != null)
            {
                MessageBox.Show("Użytkownik z takim loginem już istnieje w systemie!");
                return;
            }


            osoba.Nazwa = textBox1.Text;
            osoba.Adres = textBox2.Text;
            osoba.Telefon = textBox4.Text;
            osoba.Email = textBox5.Text;

            osoba.Login = textBox7.Text;
            osoba.Haslo = textBox8.Text;

            if (osoba.Id == 0)
                baza.Osoba.Add(osoba);

            baza.SaveChanges();

            this.DialogResult = DialogResult.OK;
        }

        private void EdycjaOsoby_Shown(object sender, EventArgs e)
        {
            if (osoba.Id > 0)
            {
                textBox1.Text = osoba.Nazwa;
                textBox2.Text = osoba.Adres;
                textBox4.Text = osoba.Telefon;
                textBox5.Text = osoba.Email;
                textBox7.Text = osoba.Login;
                textBox8.Text = osoba.Haslo;

                this.Text = "Edycja klienta";
            }
            else
            {
                this.Text = "Rejestracja klienta";
            }
        }
    }
}
