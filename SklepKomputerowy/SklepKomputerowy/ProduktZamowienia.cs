namespace SklepKomputerowy
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProduktZamowienia")]
    public partial class ProduktZamowienia
    {
        public int Id { get; set; }

        public int Zamowienie_Id { get; set; }

        public int Produkt_Id { get; set; }

        public int Ilosc { get; set; }
    }
}
