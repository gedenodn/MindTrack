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
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
                throw new Exception("User already exists");

            var user = new ApplicationUser { Email = email, UserName = email, Name = name };

            if (!await _userRepository.AddAsync(user, password))
                throw new Exception("User registration failed");

            return _jwtTokenGenerator.GenerateToken(user);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                throw new Exception("Invalid email or password");

            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            if (!result.Succeeded)
                throw new Exception("Invalid email or password");

            return _jwtTokenGenerator.GenerateToken(user);
        }
    }
}
