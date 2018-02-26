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
    [Route("api/AssociatedEvents")]
    public class AssociatedEventsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IRepository<AssociatedEvents> _repo;
        public AssociatedEventsController(
            UserManager<ApplicationUser> userManager,
            IRepository<AssociatedEvents> repo,
            IMapper mapper)
        {
            _userManager = userManager;
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostAssociatedEvent([FromBody]PostAssociatedEventParams param)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            bool success = Enum.IsDefined(typeof(Models.Type), param.TypeOfAssociation);
            if (!success)
            {
                var returnval = string.Format("Type {0} does not yet exist", param.TypeOfAssociation);
                return NotFound(returnval);
            }

            var newValue = new AssociatedEvents
            {
                ApplicationUserId = user.Id,
                EventId = param.EventId,
                Type = param.TypeOfAssociation
            };
            _repo.Add(newValue);

            return Ok(newValue);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAssociatedEvent([FromBody] IdParam param)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var query = _repo.GetAll();
            //var toDelete = query.SingleOrDefault(x => (x.EventId == param.EventId) && (x.Type == param.TypeOfAssociation) && (x.ApplicationUserId == user.Id));
            var toDelete = query.SingleOrDefault(x => x.Id == param.Id);
            _repo.Delete(toDelete);
            return Ok();
        }
    }
}