using Disney.Data;
using Disney.DTOs.User;
using Disney.Entities;
using Disney.Exceptions;
using Disney.Utils;
using Microsoft.EntityFrameworkCore;

namespace Disney.Services
{

    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IJwtUtils _jwtUtils;

        public AuthService(DataContext context, IJwtUtils jwtUtils)
        {
            _context = context;
            _jwtUtils = jwtUtils;
        }

        public async Task Register(UserAuthDto authDto)
        {
            var existingUser = await _context.Users
                                .FirstOrDefaultAsync(u => u.Email.ToLower().Equals(authDto.Email));

            if (existingUser != null)
                throw new AppException("Email already exists.");

            PasswordUtils.GetHash(authDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Email = authDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<string> Login(UserAuthDto authDto)
        {
            var existingUser = await _context.Users
                                .FirstOrDefaultAsync(u => u.Email.ToLower().Equals(authDto.Email.ToLower()));

            if (existingUser == null)
                throw new AppException("Wrong credentials.");

            var match = PasswordUtils.MatchHash(authDto.Password, existingUser.PasswordHash, existingUser.PasswordSalt);

            if (!match)
                throw new AppException("Wrong credentials.");

            return _jwtUtils.GetJwt(existingUser.Id);
        }
    }
}