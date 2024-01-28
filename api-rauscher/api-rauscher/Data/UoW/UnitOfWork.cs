using Data.Context;
using Domain.Interfaces;
using System;

namespace Data.UoW
{
    public class UnitOfWork 
        //: IUnitOfWork
    {
        //private readonly SiStocklerCmvContext _context;

        //public UnitOfWork(SiStocklerCmvContext context)
        //{
        //    _context = context;
        //}

        //public bool Commit()
        //{
        //    try
        //    {
        //        return _context.SaveChanges() > 0;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.InnerException);
        //        throw;
        //    }
        //}

        #region Dispose
        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposed && disposing)
        //    {
        //        _context.Dispose();
        //    }
        //    disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
        #endregion
    }
}
