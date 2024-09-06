using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
  public interface IRepository<TEntity> : IDisposable where TEntity : class
  {
    void Add(TEntity obj);
    Task<List<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
    TEntity GetById(Guid id);
    Task<List<TEntity>> GetAllAsync();
    void Update(TEntity obj);
    void Remove(Guid id);
    int SaveChanges();
    void RemoveAll(List<TEntity> obj);
    void AddRange(List<TEntity> obj);
  }
}
