using SSE_Reporting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Dao.Impl
{
    class AdminImpl : IRepository<Admin>
    {
        /// <summary>
        /// The database context
        /// </summary>
        private DBContext _dbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminImpl"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AdminImpl(DBContext context)
        {
            _dbContext = context;
        }
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Admin delete(Admin entity)
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
        public Admin get(int id) => (Admin)_dbContext.Employees.Find(id);

        /// <summary>
        /// Gets the specified line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        public Admin get(string line) => (Admin)_dbContext.Employees.Where(user => user.Login == line).FirstOrDefault();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Admin> getAll()
        {
            HashSet<Admin> Admins = new HashSet<Admin>();
            foreach (Employee item in _dbContext.Employees)
            {
                Admins.Add(new Admin() { Id = item.Id, Login = item.Login, Password = item.Password, TimeOff =item.TimeOff, Sickness = item.Sickness, ProjectId = item.ProjectId, Role = item.Role });
            }
            return new ObservableCollection<Admin>(Admins);
        }
        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Admin save(Admin entity)
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
        public Admin update(Admin entity)
        {
            _dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
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
