namespace SklepKomputerowy
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Osoba")]
    public partial class Osoba
    {
        public int Id { get; set; }

        [Required]
        public string Nazwa { get; set; }

        [Required]
        public string Adres { get; set; }

        [StringLength(30)]
        public string Telefon { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Haslo { get; set; }

        public bool Pracownik { get; set; }
    }
}
