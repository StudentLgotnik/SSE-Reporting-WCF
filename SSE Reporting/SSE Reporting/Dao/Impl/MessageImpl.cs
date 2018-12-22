using SSE_Reporting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Dao.Impl
{
    class MessageImpl : IRepository<Message>
    {
        /// <summary>
        /// The database context
        /// </summary>
        private DBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageImpl"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public MessageImpl(DBContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Message delete(Message entity)
        {
            _dbContext.Messages.Remove(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Message get(int id) => _dbContext.Messages.Find(id);

        /// <summary>
        /// Gets the specified line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        public Message get(string line) => _dbContext.Messages.Where(message => message.Messagee == line).FirstOrDefault();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Message> getAll()
        {
            return new ObservableCollection<Message>(_dbContext.Messages);
        }

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Message save(Message entity)
        {
            _dbContext.Messages.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Message update(Message entity)
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
