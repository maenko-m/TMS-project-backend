using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Auth;
using TmsSolution.Application.Interfaces;
using TmsSolution.Application.Utilities;
using TmsSolution.Infrastructure.Data.Repositories;
using TmsSolution.Infrastructure.Security;

namespace TmsSolution.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            IUserRepository userRepository,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponseDto> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            if (!PasswordHasher.Verify(password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            var (Token, ExpiresAt) = _jwtTokenGenerator.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = Token,
                ExpiresAt = ExpiresAt
            };
        }
    }
}
