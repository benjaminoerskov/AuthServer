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

            var newValue = new AssociatedEvents();
            newValue.ApplicationUserId = user.Id;
            newValue.EventId = param.EventId;
            newValue.Type = param.TypeOfAssociation;
            _repo.Add(newValue);

            //if (param.TypeOfAssociation)
            //    var likedEvents = new AssociatedEvents();
            //likedEvents.EventId = param.EventId;
            //user = await _repo.GetAll().Include(i => i.AssociatedEvents).SingleOrDefaultAsync(i => i.Id == user.Id);
            //user.AssociatedEvents.Add(likedEvents);


            return Ok(newValue);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAssociatedEvent([FromBody] PostAssociatedEventParams param)
        {
            //TODO lav om så den sletter på id i stedet for
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var query = _repo.GetAll();
            var toDelete = query.SingleOrDefault(x => (x.EventId == param.EventId) && (x.Type == param.TypeOfAssociation) && (x.ApplicationUserId == user.Id));
            _repo.Delete(toDelete);
            return Ok();

        }
    }
}