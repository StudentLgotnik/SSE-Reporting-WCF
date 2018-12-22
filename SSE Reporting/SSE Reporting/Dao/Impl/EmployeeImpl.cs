using SSE_Reporting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Dao.Impl
{
    class EmployeeImpl : IRepository<Employee>
    {
        /// <summary>
        /// The database context
        /// </summary>
        private DBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeImpl"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EmployeeImpl(DBContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Employee delete(Employee entity)
        {
            _dbContext.Employees.Remove(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Employee get(int id) => _dbContext.Employees.Find(id);

        /// <summary>
        /// Gets the specified line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        public Employee get(string line) => _dbContext.Employees.Where(user => user.Login == line).FirstOrDefault();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Employee> getAll()
        {
            return new ObservableCollection<Employee>(_dbContext.Employees);
        }

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Employee save(Employee entity)
        {
            _dbContext.Employees.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Employee update(Employee entity)
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
