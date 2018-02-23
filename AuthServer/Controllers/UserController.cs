using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.DTOs;
using AuthServer.Models;
using AuthServer.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IRepository<ApplicationUser> _repo;

        public UserController(UserManager<ApplicationUser> userManager,
           IRepository<ApplicationUser> repo,
            IMapper mapper)
        {
            _userManager = userManager;
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            user = await _repo.GetAll().Include(i => i.AssociatedEvents).SingleOrDefaultAsync(i => i.Id == user.Id);
            var returnval = _mapper.Map<GetUserDTO>(user);

            return Ok(returnval);
        }

        

    }
}