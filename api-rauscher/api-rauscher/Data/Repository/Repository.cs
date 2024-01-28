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
        //: IRepository<TEntity> where TEntity : class
    {
        //protected SiStocklerCmvContext Db;
        //protected DbSet<TEntity> DbSet;

        //public Repository(SiStocklerCmvContext context)
        //{
        //    Db = context;
        //    DbSet = Db.Set<TEntity>();
        //}

        //public async Task<List<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        //{
        //    return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        //}

        //public async Task<TEntity> GetExport(Guid id)
        //{
        //    return await DbSet.FindAsync(id);
        //}

        //public IQueryable<TEntity> GetExports()
        //{
        //    return DbSet.AsQueryable();
        //}

        //public void Adicionar(TEntity obj)
        //{
        //    DbSet.Add(obj);
        //}

        //public void Atualizar(TEntity obj)
        //{
        //    DbSet.Update(obj);
        //}

        //public void Remover(TEntity obj)
        //{
        //    DbSet.Remove(obj);
        //}

        //public int SaveChanges()
        //{
        //    return Db.SaveChanges();
        //}

        //public async Task<TEntity> BuscarUnico(Expression<Func<TEntity, bool>> predicate)
        //{
        //    return await DbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        //}

        //public void Dispose()
        //{
        //    Db.Dispose();
        //    GC.SuppressFinalize(this);
        //}
    }
}
