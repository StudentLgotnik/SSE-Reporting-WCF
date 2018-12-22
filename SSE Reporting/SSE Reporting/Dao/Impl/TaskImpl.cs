using SSE_Reporting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SSE_Reporting.Dao.Impl
{
    class TaskImpl : IRepository<Task>
    {
        /// <summary>
        /// The database context
        /// </summary>
        private DBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskImpl"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public TaskImpl(DBContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Task delete(Task entity)
        {
            _dbContext.Tasks.Remove(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task get(int id) => _dbContext.Tasks.Find(id);

        /// <summary>
        /// Gets the specified line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        public Task get(string line) => _dbContext.Tasks.Where(user => user.Name == line).FirstOrDefault();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Task> getAll()
        {
            return new ObservableCollection<Task>(_dbContext.Tasks.ToList());
        }

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Task save(Task entity)
        {
            _dbContext.Tasks.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Task update(Task entity)
        {
            _dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
    }
}
