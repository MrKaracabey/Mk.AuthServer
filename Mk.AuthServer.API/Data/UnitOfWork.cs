using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mk.AuthServer.Core.Services;

namespace Mk.AuthServer.API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(EfDataContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}