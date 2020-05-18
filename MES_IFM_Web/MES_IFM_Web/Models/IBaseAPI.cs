using MES_IFM_Web.Models.SQL;
using System.Collections.Generic;

namespace MES_IFM_Web.Models
{
    interface IBaseAPI<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetList();
        T Get(T item);
        T Post(T item);
        bool Put(T item);
        bool Delete(string id);
    }
}
