using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAS_TestiranjeSoftvera_Projekat.BusinessLogicLayer
{
    public interface IService<T> where T : class
    {
        string Insert(T entity);
        string Update(T entity);
        string Delete(T entity);
        IEnumerable<T> SelectAll();
        T SelectById(int? id);
        bool Validate(T entity, out string poruka);
    }
}
