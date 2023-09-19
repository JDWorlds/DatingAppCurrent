using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using datingApp.Data;
using datingApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace datingApp.Controllers
{
    [ApiController]
    [Route("datingApp/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected readonly DataContext _context;
        protected readonly ITokenService _tokenService;

        public BaseApiController(DataContext context, ITokenService tokenService)
        {
            _context        = context;
            _tokenService   = tokenService;
        }

    }
}