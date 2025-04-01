using Microsoft.AspNetCore.Identity;
using MindTrack.Domain.Entities;
using MindTrack.Domain.Interfaces;

namespace MindTrack.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> AddAsync(ApplicationUser user, string password)
        {
            
            var result = await _userManager.CreateAsync(user, password);
            
            return result.Succeeded;
        }
    }
}
