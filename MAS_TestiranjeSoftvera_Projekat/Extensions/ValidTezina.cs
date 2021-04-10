using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MAS_TestiranjeSoftvera_Projekat.Extensions
{
    public class ValidTezina : ValidationAttribute
    {
        private readonly int allowedMaximum;

        public ValidTezina(int allowedMaximum)
        {
            this.allowedMaximum = allowedMaximum;
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;
            int v = Convert.ToInt32(value);
            if (v < 0 || v > allowedMaximum) return false;
            return true;
        }
    }
}
