namespace SklepKomputerowy
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Zamowienie")]
    public partial class Zamowienie
    {
        public int Id { get; set; }

        public int Osoba_Id { get; set; }

        public string Tytul { get; set; }

        public int Status { get; set; }
    }
}
