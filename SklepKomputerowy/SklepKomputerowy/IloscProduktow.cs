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
    public partial class IloscProduktow : Form
    {
        public int Ilosc { get; set; }

        private int MaxIlosc { get; set; }

        public IloscProduktow(int max)
        {
            InitializeComponent();

            MaxIlosc = max;

            label1.Text += " (max " + max.ToString() + ")";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ilosc;
            if(int.TryParse(textBox1.Text, out ilosc))
            {
                if(ilosc > MaxIlosc)
                {
                    MessageBox.Show("W magazynie ma nie takiej ilości produktów");
                    return;
                }

                Ilosc = ilosc;

                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Podaj poprawną ilość produktów.");
            }
        }
    }
}
