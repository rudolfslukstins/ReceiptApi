using System.Collections.Generic;
using System.Linq;
using Receipt.Core.Models;

namespace Receipt.Core.Services
{
    public interface IEntityService<T> where T : Entity
    {
        void Create<T>(T entity) where T : Entity;
        void Update<T>(T entity) where T : Entity;
        List<T> GetAll<T>() where T : Entity;
        T GetById<T>(int id) where T : Entity;
        IQueryable<T> Query<T>() where T : Entity;
    }
}