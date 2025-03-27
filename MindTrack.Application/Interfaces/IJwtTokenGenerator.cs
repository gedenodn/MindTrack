using MindTrack.Domain.Entities;

namespace MindTrack.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser user);
    }
}
