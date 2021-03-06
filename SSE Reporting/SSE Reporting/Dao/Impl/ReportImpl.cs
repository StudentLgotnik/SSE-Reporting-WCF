﻿using SSE_Reporting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Dao.Impl
{
    class ReportImpl : IRepository<Report>
    {
        /// <summary>
        /// The database context
        /// </summary>
        private DBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportImpl"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ReportImpl(DBContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Report delete(Report entity)
        {
            _dbContext.Reports.Remove(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Report get(int id) => _dbContext.Reports.Find(id);

        /// <summary>
        /// Gets the specified line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        public Report get(string line) => _dbContext.Reports.Where(user => user.Commect == line).FirstOrDefault();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Report> getAll()
        {
            return new ObservableCollection<Report>(_dbContext.Reports.ToList());
        }

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Report save(Report entity)
        {
            _dbContext.Reports.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Report update(Report entity)
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
