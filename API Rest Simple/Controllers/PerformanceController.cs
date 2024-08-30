using API_Rest_Simple.Models;
using API_Rest_Simple.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API_Rest_Simple.Controllers
{

    [Route("api/performance")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {
        
        private readonly IPersonRepository _personRepository;

        public PerformanceController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Index()
        {

            int i = 0;
            while(i < 10)
            {
                var peoplelita = await _personRepository.GetAllPersonAsync();
                i++;
            }
            var people = await _personRepository.GetAllPersonAsync();

            return Ok(people);
        }
    }
}
