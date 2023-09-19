using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using datingApp.Data;
using datingApp.Entities;
using datingApp.Interfaces;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;

namespace datingApp.Controllers
{
    public class UsersController : BaseApiController
    {
        public UsersController(DataContext context, ITokenService tokenService) : base(context, tokenService){}

        // Http Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        // Http Post

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }
        
    }
}