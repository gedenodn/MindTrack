using Microsoft.AspNetCore.Identity;
using MindTrack.Application.Interfaces;
using MindTrack.Domain.Entities;
using MindTrack.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace MindTrack.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            IUserRepository userRepository,
            SignInManager<ApplicationUser> signInManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> RegisterAsync(string email, string name, string password)
        {
            try
            {
                var existingUser = await _userRepository.GetByEmailAsync(email);
                if (existingUser != null)
                    throw new InvalidOperationException("User already exists");

                var user = new ApplicationUser { Email = email, UserName = email, Name = name };
                bool isAdded = await _userRepository.AddAsync(user, password);

                if (!isAdded)
                    throw new Exception("User registration failed");

                return _jwtTokenGenerator.GenerateToken(user);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Registration error: {ex.Message}", ex);
            }
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _userRepository.GetByEmailAsync(email);
                if (user == null)
                    throw new UnauthorizedAccessException("Invalid email or password");

                var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
                if (!result.Succeeded)
                    throw new UnauthorizedAccessException("Invalid email or password");

                return _jwtTokenGenerator.GenerateToken(user);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Login error: {ex.Message}", ex);
            }
        }
    }
}
