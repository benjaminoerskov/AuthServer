using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DetSkerIAarhusBackend.API.DTOs;
using DetSkerIAarhusBackend.API.Models;
using DetSkerIAarhusBackend.API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DetSkerIAarhusBackend.API.Controllers
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
        public async Task<IActionResult> PostAssociatedOccurrence([FromBody]PostAssociatedOccurrencesParams param)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var occurrences = new AssociatedOccurrences
            {
                ApplicationUserId = user.Id,
                OccurrenceId = param.OccurrenceId,
                Type = param.TypeOfAssociation
            };

            var query = _repo.GetAll();
            var result = query.FirstOrDefault(x =>
                 (x.ApplicationUserId == user.Id) && (x.OccurrenceId == param.OccurrenceId) &&
                 (x.Type == param.TypeOfAssociation));
            if (result != null)
            {
                return BadRequest("Occurence already exists for user with this type");
            }
            _repo.Add(occurrences);

            var occurencesDto = _mapper.Map<AssociatedOccurrencesDTO>(occurrences);

            return Ok(occurencesDto);
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
            if (toDelete != null)
            {
                _repo.Delete(toDelete);
                return NoContent();
            }
            else
            {
                return NotFound(string.Format($"No occurrence with id: {0} exists", param.Id));
            }
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
            var types = query.ToList().Where(x => x.ApplicationUserId == user.Id).Select(u => u.Type).Distinct();
            if (types.ToList().Count > 1)
            {
                return Ok(types);
            }
            else
            {
                return NotFound("User doesnt have any lists");
            }
        }
    }
}