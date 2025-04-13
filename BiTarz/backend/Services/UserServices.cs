using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using backend.Context;
using backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services
{

    public class UserServices
    {

        private readonly butikContext _butikContext;
        private readonly IConfiguration _configuration;
        public UserServices(butikContext butikContext, IConfiguration configuration)
        {
            _butikContext = butikContext;
            _configuration = configuration;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var users = await _butikContext.Users.ToListAsync();
            if (users == null)
            {
                return null;
            }
            return users;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _butikContext.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<User> RegisterUserAsync(User user, int RoleId)
        {
            if (await _butikContext.Users.AnyAsync(u => u.Email == user.Email))
            {
                return null;
            }
            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = HashPassword(user.Password),
                RoleId = 2

            };

            _butikContext.Users.Add(newUser);
            await _butikContext.SaveChangesAsync();
            return newUser;

        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            string hashedInput = HashPassword(inputPassword);
            return hashedInput == hashedPassword;
        }

        public async Task<string> LoginUserAsync(User loginUser)
        {
            var user = await _butikContext.Users.FirstOrDefaultAsync(u => u.Email == loginUser.Email);
            if (user == null)
            {

                Console.WriteLine("Kullanıcı Bulunamadı");
                return "Geçersiz Kullanıcı Adı";
            }

            if (!VerifyPassword(loginUser.Password, user.Password))
            {
                Console.WriteLine("Geçersiz Şifre");
                return "Geçersiz Şifre";
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())

            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(
                 new SymmetricSecurityKey(key),
                 SecurityAlgorithms.HmacSha256Signature)
            };

            var token= tokenHandler.CreateToken(tokenDescriptor);
            var tokenString=tokenHandler.WriteToken(token);
            return tokenString;

        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _butikContext.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }
            _butikContext.Users.Remove(user);
            await _butikContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = await _butikContext.Users.FindAsync(user.Id);
            if (existingUser == null)
            {
                return false;
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = HashPassword(user.Password);
            existingUser.RoleId = user.RoleId;

            _butikContext.Users.Update(existingUser);
            await _butikContext.SaveChangesAsync();
            return true;
        }





    }
}