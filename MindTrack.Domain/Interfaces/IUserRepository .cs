using MindTrack.Domain.Entities;

namespace MindTrack.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<bool> AddAsync(ApplicationUser user, string password);
    }
}
