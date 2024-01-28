using Data.Context;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repository
{
  public class Repository<TEntity>
      : IRepository<TEntity> where TEntity : class
  {
    protected RauscherDbContext Db;
    protected DbSet<TEntity> DbSet;

    public Repository(RauscherDbContext context)
    {
      Db = context;
      DbSet = Db.Set<TEntity>();
    }
    public virtual void Add(TEntity obj)
    {
      DbSet.Add(obj);
    }

    public async Task<List<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
    {
      return await DbSet.Where(predicate).ToListAsync();
    }

    public virtual TEntity GetById(Guid id)
    {
      return DbSet.Find(id);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
      return await DbSet.ToListAsync();
    }

    public virtual void Update(TEntity obj)
    {
      DbSet.Update(obj);
    }

    public virtual void Remove(Guid id)
    {
      DbSet.Remove(DbSet.Find(id));
    }

    public int SaveChanges()
    {
      return Db.SaveChanges();
    }

    public void Dispose()
    {
      Db.Dispose();
      GC.SuppressFinalize(this);
    }
  }
}
