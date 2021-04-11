using MAS_TestiranjeSoftvera_Projekat.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAS_TestiranjeSoftvera_Projekat.BusinessLogicLayer.Interfaces
{
    public interface IOsobaService : IService<Osoba>
    {
        IEnumerable<Osoba> SelectAllAdults();
    }
}
