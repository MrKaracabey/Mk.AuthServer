using System.Threading.Tasks;
using Mk.AuthServer.Core.Dtos;
using Mk.AuthServer.Core.models;
using SharedLibrary;
using SharedLibrary.Dtos;

namespace Mk.AuthServer.Core.Services
{
    public interface IUserService
    {
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<Response<UserAppDto>> GetUserByUserNameAsync(string userName);
    }
}