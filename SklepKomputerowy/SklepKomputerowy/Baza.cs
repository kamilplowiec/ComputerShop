namespace SklepKomputerowy
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Baza : DbContext
    {
        public Baza()
            : base("name=Baza")
        {
        }

        public virtual DbSet<Osoba> Osoba { get; set; }
        public virtual DbSet<Produkt> Produkt { get; set; }
        public virtual DbSet<ProduktZamowienia> ProduktZamowienia { get; set; }
        public virtual DbSet<Zamowienie> Zamowienie { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
