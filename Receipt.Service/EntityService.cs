using Receipt.Core.Models;
using System.Collections.Generic;
using System.Linq;
using Receipt.Core.Services;
using Receipt.Db;

namespace Receipt.Service
{
    public class EntityService<T> : DbService, IEntityService<T> where T : Entity
    {
        public EntityService(ReceiptDbContext context) : base(context)
        {
        }

        public void Create(T entity)
        {
            Create<T>(entity);
        }

        public void Delete(T entity)
        {
            Delete<T>(entity);
        }

        public void Update(T entity)
        {
            Update<T>(entity);
        }

        public List<T> GetAll()
        {
            return GetAll<T>();
        }

        public T GetById(int id)
        {
            return GetById<T>(id);
        }

        public IQueryable<T> Query()
        {
            return Query<T>();
        }

    }
}