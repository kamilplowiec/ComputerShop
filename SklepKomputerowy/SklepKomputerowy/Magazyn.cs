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
    public partial class Magazyn : Form
    {
        Baza baza = new Baza();

        public Magazyn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EdycjaProduktu edycja = new EdycjaProduktu();
            if (edycja.ShowDialog(this) == DialogResult.OK)
            {
                OdswiezProdukty();
            }
        }

        private void Magazyn_Shown(object sender, EventArgs e)
        {
            OdswiezProdukty();
        }

        private void OdswiezProdukty()
        {
            baza = new Baza();

            this.dataGridView1.DataBindingComplete += (o, _) => //po zakonczeniu ladowania
            {
                var dataGridView = o as DataGridView;
                if (dataGridView != null)
                {
                    DataGridViewButtonColumn przycisk = new DataGridViewButtonColumn();

                    przycisk.Name = "Usuń";
                    przycisk.HeaderText = "Usuń";
                    przycisk.Text = "Usuń";
                    przycisk.UseColumnTextForButtonValue = true;

                    int columnIndex = dataGridView.ColumnCount;
                    if (dataGridView.Columns["Usuń"] == null)
                    {
                        dataGridView.Columns.Insert(columnIndex, przycisk);
                    }

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Usuń"].Index)
            {
                if (MessageBox.Show("Czy na pewno chcesz usunąć produkt: " + dataGridView1.Rows[e.RowIndex].Cells["Nazwa"].Value.ToString() + "? \r\nSpowoduje to usunięcie produktu z wszystkich zamówień.", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int productId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                    baza.Produkt.Remove(baza.Produkt.FirstOrDefault(x => x.Id == productId));
                    baza.SaveChanges();

                    OdswiezProdukty();
                }
            }
            else
            {
                EdycjaProduktu edycja = new EdycjaProduktu(int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                if(edycja.ShowDialog(this) == DialogResult.OK)
                {
                    OdswiezProdukty();
                }
            }
        }
    }
}
