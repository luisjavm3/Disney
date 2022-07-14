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

        public AuthService(DataContext context)
        {
            _context = context;
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
    }
}