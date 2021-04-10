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
    public class Mesto
    {
        public int? MestoId { get; set; }
        [Required(ErrorMessage = "Unesite naziv!")]
        [RegularExpression("[A-Z][a-z]*",ErrorMessage = "Naziv mora poceti velikim slovom!")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Nevalidan PTT broj!")]
        [Range(11000,99999,ErrorMessage = "Nevalidan PTT broj!")]
        public int? PttBroj { get; set; }
        [Remote(action: "VerifikujBrojStanovnika", controller: "Mesto")]
        public int? BrojStanovnika { get; set; }
        public virtual IEnumerable<Osoba> Osobe { get; set; }
   

    }
}
