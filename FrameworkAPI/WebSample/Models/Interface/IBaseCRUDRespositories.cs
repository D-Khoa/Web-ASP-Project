using System.Collections.Generic;
using WebSample.Models.Library;

namespace WebSample.Models.Interface
{
    interface IBaseCRUDRespositories<T>
    {
        MESSQL mesSQL();
        IEnumerable<T> GetAll();
        IEnumerable<T> GetCollection(T inputItemInfo);
        T GetItem(T inputItemInfo);
        T Create(T inputItem);
        bool Update(T inputItem);
        void Remove(int inputItemID);
        void Remove(string inputItemUID);
    }
}
