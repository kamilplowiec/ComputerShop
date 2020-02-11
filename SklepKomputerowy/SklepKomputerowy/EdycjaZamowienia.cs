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
    public partial class EdycjaZamowienia : Form
    {
        Baza baza = new Baza();

        Zamowienie zamowienie;

        DataTable dt;

        public Osoba Zalogowany;

        public EdycjaZamowienia(int id = 0)
        {
            InitializeComponent();

            zamowienie = new Zamowienie();

            if (id > 0)
            {
                zamowienie = baza.Zamowienie.FirstOrDefault(x => x.Id == id);

                textBox1.Text = zamowienie.Tytul;
            }

            

            dt = ConvertToDataTable(
                    baza.ProduktZamowienia.ToList().Where(x => x.Zamowienie_Id == zamowienie.Id).Select(x =>
                         new
                         {
                             x.Id,
                             ProduktId = x.Produkt_Id,
                             Nazwa = baza.Produkt.FirstOrDefault(p => p.Id == x.Produkt_Id).Nazwa,
                             x.Ilosc,
                             Cena = x.Ilosc * baza.Produkt.FirstOrDefault(p => p.Id == x.Produkt_Id).Cena
                         }).ToList());

            AktualizujSume();
        }

        private void EdycjaZamowienia_Shown(object sender, EventArgs e)
        {
            LadujProdukty();
            LadujZamowienie();
        }

        private void LadujProdukty()
        {
            this.dataGridView1.DataBindingComplete += (o, _) => //po zakonczeniu ladowania
            {
                var dataGridView = o as DataGridView;
                if (dataGridView != null)
                {
                    dataGridView.Columns["Id"].Visible = false;

                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView.Columns[dataGridView.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            };

            baza.Produkt.Load();
            this.dataGridView1.DataSource = baza.Produkt.Local.ToBindingList().Select(x =>
             new
             {
                 x.Id,
                 Kategoria = ((Kategoria)x.TypProduktu).ToString(),
                 x.Nazwa,
                 x.Cena,
                 x.Ilosc
             }).ToList();

            this.dataGridView1.Refresh();
        }

        private void DodajDoZamowienia(int produktId, int ilosc) 
        {
            var produkt = baza.Produkt.FirstOrDefault(x => x.Id == produktId);

            dt.Rows.Add(0, produktId, produkt.Nazwa, ilosc, produkt.Cena * ilosc);
            this.dataGridView2.Refresh();
        }

        private void LadujZamowienie()
        {
            this.dataGridView2.DataBindingComplete += (o, _) => //po zakonczeniu ladowania
            {
                var dataGridView = o as DataGridView;
                if (dataGridView != null)
                {
                    dataGridView.Columns["Id"].Visible = false;
                    dataGridView.Columns["ProduktId"].Visible = false;

                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView.Columns[dataGridView.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            };

            this.dataGridView2.DataSource = dt;

            AktualizujSume();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedRow = this.dataGridView1.SelectedRows[0];
            int produktId = int.Parse(selectedRow.Cells["Id"].Value.ToString());

            bool istnieje = this.dataGridView2.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["ProduktId"].Value.ToString().Equals(produktId.ToString())).Count() > 0;

            if(istnieje)
            {
                MessageBox.Show("Taki produkt już jest dodany do zamówienia");
                return;
            }

            int maxIlosc = int.Parse(selectedRow.Cells["Ilosc"].Value.ToString());

            if (maxIlosc == 0)
            {
                MessageBox.Show("W magazynie nie ma zapasów tego produktu");
                return;
            }

            IloscProduktow iloscProduktow = new IloscProduktow(maxIlosc);
            if (iloscProduktow.ShowDialog(this) == DialogResult.OK)
            {
                DodajDoZamowienia(produktId, iloscProduktow.Ilosc);
            }

            AktualizujSume();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(zamowienie.Id == 0) //przy edycji nie zmieniam zamawiajacego, edytowac moze tez pracownik
                zamowienie.Osoba_Id = Zalogowany.Id;

            zamowienie.Status = (int)StatusZamowienia.Nowe;
            zamowienie.Tytul = textBox1.Text;

            if (zamowienie.Id == 0)
                baza.Zamowienie.Add(zamowienie);

            baza.SaveChanges();

            //produkty zamowienia beda nadpisywane, wiec jezeli juz bylo utworzone zamowienie, usuwam z bazy wszystkie wpisy produktow zamowienia

            if(zamowienie.Id > 0)
            {
                baza.ProduktZamowienia.RemoveRange(baza.ProduktZamowienia.Where(x => x.Zamowienie_Id == zamowienie.Id));
            }

            //nastepnie dodaje je ponownie

            foreach (DataGridViewRow row in this.dataGridView2.Rows)
            {
                ProduktZamowienia pz = new ProduktZamowienia();
                pz.Zamowienie_Id = zamowienie.Id;
                pz.Produkt_Id = int.Parse(row.Cells["ProduktId"].Value.ToString());
                pz.Ilosc = int.Parse(row.Cells["Ilosc"].Value.ToString());

                baza.ProduktZamowienia.Add(pz);
            }

            baza.SaveChanges();


            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectedRow = this.dataGridView2.SelectedRows[0];

            if (MessageBox.Show("Czy na pewno chcesz usunąć produkt z zamówienia?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.dataGridView2.Rows.RemoveAt(selectedRow.Index);
                this.dataGridView2.Refresh();

                AktualizujSume();
            }
        }

        public void AktualizujSume()
        {
            label4.Text = dt.AsEnumerable().Select(x => x.Field<decimal>("Cena")).Sum().ToString(); ;
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
