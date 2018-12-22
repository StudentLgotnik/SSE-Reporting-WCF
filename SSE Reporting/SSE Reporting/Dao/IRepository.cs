using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Dao
{
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        T save(T entity);
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T get(int id);
        /// <summary>
        /// Gets the specified line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        T get(string line);
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        ObservableCollection<T> getAll();
        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        T update(T entity);
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        T delete(T entity);
    }
}
