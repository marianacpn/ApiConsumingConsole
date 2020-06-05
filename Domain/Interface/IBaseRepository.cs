using System;
using System.Collections.Generic;

namespace Domain.Interface
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void SaveChanges(int userId = 1, string userName = "System", bool saveLog = true);
    }
}
