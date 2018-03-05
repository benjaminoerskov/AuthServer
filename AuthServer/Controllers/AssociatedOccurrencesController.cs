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
    [Route("api/[controller]")]
    public class AssociatedOccurrencesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IRepository<AssociatedOccurrences> _repo;
        public AssociatedOccurrencesController(
            UserManager<ApplicationUser> userManager,
            IRepository<AssociatedOccurrences> repo,
            IMapper mapper)
        {
            _userManager = userManager;
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostAssociatedEvent([FromBody]PostAssociatedOccurrencesParams param)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var newValue = new AssociatedOccurrences
            {
                ApplicationUserId = user.Id,
                EventId = param.EventId,
                Type = param.TypeOfAssociation
            };
            _repo.Add(newValue);

            return Ok(newValue);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAssociatedOccurrence([FromBody] IdParam param)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var query = _repo.GetAll();
            var toDelete = query.SingleOrDefault(x => x.Id == param.Id);
            _repo.Delete(toDelete);
            return Ok();
        }

        [HttpGet]
        [Route("Recommended")]
        public async Task<IActionResult> GetAssociated()
        {
            return Ok("Her kommer dejlige occurrences");
        }

        [HttpGet]
        [Route("UserListTypes")]
        public async Task<IActionResult> GetUserTypes()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var query = _repo.GetAll();
            var types = query.ToList().Where(x => x.ApplicationUserId == user.Id).Select(u => u.Type);

            return Ok(types);
        }
    }
}