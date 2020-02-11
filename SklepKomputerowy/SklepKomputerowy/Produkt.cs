namespace SklepKomputerowy
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Produkt")]
    public partial class Produkt
    {
        public int Id { get; set; }

        [Required]
        public string Nazwa { get; set; }

        public decimal Cena { get; set; }

        public int Ilosc { get; set; }

        public int TypProduktu { get; set; }
    }
}
