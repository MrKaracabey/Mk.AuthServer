using System.Threading.Tasks;

namespace Mk.AuthServer.Core.Services
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}