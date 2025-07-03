using Data.Context;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace Data.UoW
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly RauscherDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;

    public UnitOfWork(RauscherDbContext context, ILogger<UnitOfWork> logger)
    {
      _context = context;
      _logger = logger;
    }

    public bool Commit()
    {
      try
      {
        return _context.SaveChanges() > 0;
      }
      catch (Exception e)
      {
        _logger.LogError(e, "Error committing changes to the database");
        throw;
      }
    }

    #region Dispose
    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
      if (!disposed && disposing)
      {
        _context.Dispose();
      }
      disposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
    #endregion
  }
}
