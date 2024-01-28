using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Adicionar(TEntity obj);
        void Atualizar(TEntity obj);
        void Remover(TEntity obj);
        Task<List<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> BuscarUnico(Expression<Func<TEntity, bool>> predicate);
        int SaveChanges();
    }
}
