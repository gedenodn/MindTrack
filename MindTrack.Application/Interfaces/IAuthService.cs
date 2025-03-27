using System.Threading.Tasks;

namespace MindTrack.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(string email, string name, string password);
        Task<string> LoginAsync(string email, string password);
    }
}
