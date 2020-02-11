using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SklepKomputerowy
{
    public partial class Sklep : Form
    {
        Baza baza = new Baza();

        public Osoba Zalogowany { get; set; }
        public Sklep()
        {
            InitializeComponent();
            Zalogowany = null;
        }

        private void Sklep_Shown(object sender, EventArgs e)
        {
            Logowanie();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Wylogowanie();
        }

        private void Logowanie()
        {
            Login l = new Login();
            if (l.ShowDialog(this) == DialogResult.OK)
            {
                Zalogowany = l.Zalogowany;

                Zalogowano();
            }
            else
            {
                Close();
            }
        }

        private void Zalogowano()
        {
            label2.Text = Zalogowany.Nazwa;

            if (Zalogowany.Pracownik)
            {
                label2.Text += " (Pracownik)";

                button2.Visible = true;
                comboBox1.Visible = true;
            }
            else
            {
                button2.Visible = false;
                comboBox1.Visible = false;
            }

            InicjujZamowienia();
        }

        private void InicjujZamowienia()
        {
            if (Zalogowany.Pracownik)
            {
                comboBox1.Visible = true;
                comboBox1.Items.Add("Wszyscy");
                comboBox1.Items.AddRange(baza.Osoba.Where(x => !x.Pracownik).Select(x => x.Nazwa).ToArray());

                comboBox1.SelectedIndex = 0;

                button1.Visible = false;
            }
            else
            {
                comboBox1.Visible = false;
                button1.Visible = true;
            }

            LadujZamowienia();
        }

        private void LadujZamowienia()
        {
            string nazwa = Zalogowany.Pracownik ? comboBox1.SelectedItem.ToString() : "";

            this.dataGridView1.DataBindingComplete += (o, _) => //po zakonczeniu ladowania
            {
                var dataGridView = o as DataGridView;
                if (dataGridView != null)
                {
                    DataGridViewButtonColumn przycisk = new DataGridViewButtonColumn();
                    
                    przycisk.Name = "Edytuj";
                    przycisk.HeaderText = "Edycja";
                    przycisk.Text = "Edycja";
                    przycisk.UseColumnTextForButtonValue = true;

                    int columnIndex = dataGridView.ColumnCount;
                    if (dataGridView.Columns["Edytuj"] == null)
                    {
                        dataGridView.Columns.Insert(columnIndex, przycisk);
                    }

                    if(Zalogowany.Pracownik)
                    {
                        DataGridViewButtonColumn przycisk2 = new DataGridViewButtonColumn();

                        przycisk2.Name = "Zrealizuj";
                        przycisk2.HeaderText = "Zrealizowane";
                        przycisk2.Text = "Zrealizowane";
                        przycisk2.UseColumnTextForButtonValue = true;

                        int columnIndex2 = dataGridView.ColumnCount;
                        if (dataGridView.Columns["Zrealizuj"] == null)
                        {
                            dataGridView.Columns.Insert(columnIndex2, przycisk2);
                        }
                    }
                    else
                    {
                        if (dataGridView.Columns["Zrealizuj"] != null)
                        {
                            dataGridView.Columns.Remove(dataGridView.Columns["Zrealizuj"]);
                        }
                    }

                    dataGridView.Columns["Id"].Visible = false;

                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView.Columns[dataGridView.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            };

            baza.Osoba.Load();
            var osoby = baza.Osoba.Local.ToBindingList().Where(x => string.IsNullOrEmpty(nazwa) || nazwa == "Wszyscy" ? x.Id > 0 : x.Nazwa.Contains(nazwa)).ToList();

            baza.Zamowienie.Load();
            var zamowienia = baza.Zamowienie.Local.ToBindingList().Where(x => osoby.Any(o => o.Id == x.Osoba_Id)).ToList();


            this.dataGridView1.DataSource = zamowienia.Select(x =>
            new {
                x.Id,
                x.Tytul,
                Osoba = osoby.FirstOrDefault(o => o.Id == x.Osoba_Id).Nazwa,
                Status = ((StatusZamowienia)x.Status).ToString()
            }).ToList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Edytuj"].Index)
            {
                int zamowienieId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());

                if(baza.Zamowienie.First(x => x.Id == zamowienieId).Status == (int)StatusZamowienia.Zrealizowane)
                {
                    MessageBox.Show("To zamówienie jest już zrealizowane. Nie możesz go edytować.");
                    return;
                }

                EdycjaZamowienia edycja = new EdycjaZamowienia(zamowienieId);
                edycja.Zalogowany = Zalogowany;
                if(edycja.ShowDialog(this) == DialogResult.OK)
                {
                    LadujZamowienia();
                }
            }
            else if (dataGridView1.Columns["Zrealizuj"] != null && e.ColumnIndex == dataGridView1.Columns["Zrealizuj"].Index)
            {
                int zamowienieId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());

                if (baza.Zamowienie.First(x => x.Id == zamowienieId).Status == (int)StatusZamowienia.Zrealizowane)
                {
                    MessageBox.Show("To zamówienie jest już zrealizowane. Nie możesz go edytować.");
                    return;
                }

                if (MessageBox.Show("Czy chcesz zrealizować to zamówienie?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var zamowienie = baza.Zamowienie.First(x => x.Id == zamowienieId);

                    if (zamowienie != null)
                    {
                        baza.Produkt.Load();
                        //zmniejszam ilosc produktow w magazynie o ilosc produktow z zamowienia
                        var produktList = baza.ProduktZamowienia.Where(x => x.Zamowienie_Id == zamowienie.Id);
                        foreach(var produkt in produktList)
                        {
                            var p = baza.Produkt.FirstOrDefault(x => x.Id == produkt.Produkt_Id);
                            if(p != null)
                            {
                                p.Ilosc -= produkt.Ilosc;
                            }
                        }

                        //zmieniam status zamowienia na zrealizowane
                        zamowienie.Status = (int)StatusZamowienia.Zrealizowane;

                        baza.SaveChanges();

                        LadujZamowienia();
                    }
                }
            }
        }

        private void Wylogowanie()
        {
            Zalogowany = null;

            label2.Text = "?";
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();

            Logowanie();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EdycjaOsoby edycja = new EdycjaOsoby(Zalogowany.Id);
            edycja.ShowDialog(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LadujZamowienia();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Magazyn m = new Magazyn();
            m.ShowDialog(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EdycjaZamowienia edycja = new EdycjaZamowienia();
            edycja.Zalogowany = Zalogowany;
            if(edycja.ShowDialog(this) == DialogResult.OK)
            {
                LadujZamowienia();
            }
        }
    }

    public enum StatusZamowienia
    {
        Nowe = 1,
        Zrealizowane = 2
    }

    public enum Kategoria
    {
        Komputer = 1,
        Laptop = 2,
        Akcesoria = 3,
        Podzespoly = 4
    }
}
