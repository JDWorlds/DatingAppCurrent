using System.Reflection.Metadata;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using datingApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using datingApp.Data;
using System.Text;
using datingApp.DTOs;
using datingApp.Interfaces;


namespace datingApp.Controllers
{
    public class AccoutController : BaseApiController
    {
        public AccoutController(DataContext context, ITokenService tokenService) : base(context, tokenService){}

        // Register
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTOs registerDTO)
        {
            // User Name is it valid?
            if(await UserExsist(registerDTO.Username)) return BadRequest("Username is taken");

            // Password hash , salt
            using var hmac = new HMACSHA512();

            var user = new AppUser()
            {
                UserName        = registerDTO.Username,
                PasswordHash    = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt    = hmac.Key
            };
            
            _context.Add(user);
            await _context.SaveChangesAsync();

            return new UserDTO()
            {
                Username = user.UserName,
                Token    = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExsist(string username)
        {
            return await _context.Users.AnyAsync(x=>x.UserName == username.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUser>> Login(LoginDTOs loginDTO)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x=>x.UserName == loginDTO.UserName);

            if(user == null) return Unauthorized();

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for(int i=0; i<computedHash.Length; i++)
            {
                if(computedHash[i]!=user.PasswordHash[i]) return Unauthorized("invalid username");
            }

            return user;
        }
    }
}