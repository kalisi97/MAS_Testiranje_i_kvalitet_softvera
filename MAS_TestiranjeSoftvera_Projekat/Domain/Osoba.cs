using MAS_TestiranjeSoftvera_Projekat.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static MAS_TestiranjeSoftvera_Projekat.Extensions.Enums;

namespace MAS_TestiranjeSoftvera_Projekat.Domain
{
    public class Osoba
    {
        public int? OsobaId { get; set; }
        [Required(ErrorMessage = "Unesite maticni broj!")]
        [Remote(action: "VerifikujMaticniBroj", controller: "Osoba")]
        public string MaticniBroj { get; set; }
  
        [Required(ErrorMessage = "Unesite ime!")]
        [RegularExpression("[A-Z][a-z]*",ErrorMessage = "Ime mora poceti velikim slovom!")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Unesite prezime!")]
        [RegularExpression("[A-Z][a-z]*",ErrorMessage = "Prezime mora poceti velikim slovom!")]
        public string Prezime { get; set; }
        [ValidVisina(allowedMinimum: 35, ErrorMessage = "Minimum za visinu je 35 cm!")]
        public int? Visina { get; set; }
        [ValidTezina(allowedMaximum: 250, ErrorMessage = "Maximum za tezinu je 250 kg!")]
        public int? Tezina { get; set; }
        public BojaOciju? BojaOciju { get; set; }
        [Remote(action: "VerifikujTelefon", controller: "Osoba")]
        public string Telefon { get; set; }
        [Required(ErrorMessage = "Unesite email!")]
        [Remote(action: "VerifikujEmail", controller: "Osoba")]
        public string Email { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Unesite datum rodjenja!")]
        public DateTime? DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public string Napomena { get; set; }
        [Required(ErrorMessage = "Odaberite mesto!")]
        public int? MestoId { get; set; }
        public Mesto Mesto { get; set; }
 


    }
}
