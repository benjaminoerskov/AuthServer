using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VentureAarhusBackend.API.DTOs;
using VentureAarhusBackend.API.Models;
using VentureAarhusBackend.API.Repositories;

namespace VentureAarhusBackend.API.Controllers
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
            user = await _repo.GetAll().Include(i => i.AssociatedOccurrences).SingleOrDefaultAsync(i => i.Id == user.Id);
            var returnval = _mapper.Map<GetUserDTO>(user);

            return Ok(returnval);
        }




    }
}