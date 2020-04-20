using System.Collections.Generic;

namespace WepAPI.Models
{
    /// <summary>
    /// Base CRUD respository Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IBaseCRUDRespository<T>
    {
        /// <summary>
        /// Get all <typeparamref name="T"/>
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Get list <typeparamref name="T"/>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        IEnumerable<T> GetList(T item);
        /// <summary>
        /// Get <typeparamref name="T"/> with id
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        T Get(T item);
        /// <summary>
        /// Add new <typeparamref name="T"/>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        T Create(T item);
        /// <summary>
        /// Update <typeparamref name="T"/>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Update(T item);
        /// <summary>
        /// Remove <typeparamref name="T"/>
        /// </summary>
        /// <param name="id"></param>
        void Remove(int id);
    }
}
