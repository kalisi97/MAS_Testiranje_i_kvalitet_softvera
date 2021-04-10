using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MAS_TestiranjeSoftvera_Projekat.Extensions
{
    public class ValidVisina : ValidationAttribute
    {
        private readonly int allowedMinimum;

        public ValidVisina(int allowedMinimum)
        {
            this.allowedMinimum = allowedMinimum;
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;
            int v = Convert.ToInt32(value);
            if (v <= allowedMinimum || v > 250) return false;
            return true;
        }
    }
}
