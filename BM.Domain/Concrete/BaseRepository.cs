using System;
using BM.DataAccess.DbContexts;
using Microsoft.Extensions.Logging;

namespace BM.Domain.Concrete
{
    public abstract class BaseRepository : IDisposable
    {
        protected readonly BMContext _context;

        protected readonly ILogger _logger;

        protected BaseRepository(BMContext context, ILogger<BaseRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }
    }
}
