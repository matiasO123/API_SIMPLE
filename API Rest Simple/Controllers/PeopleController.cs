using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Rest_Simple.Context;
using API_Rest_Simple.Models;
using API_Rest_Simple.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace API_Rest_Simple.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonRepository _person_Rep;

        public PeopleController(IPersonRepository person_Rep)
        {
            _person_Rep = person_Rep;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Getpersons()
        {
            var people =  await _person_Rep.GetAllPersonAsync();
            return Ok(people);
        }

        // GET: api/People
        [HttpGet("people_we")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople_and_we()
        {
            var people = await _person_Rep.GetAllPeople_with_Working_exp();
            return Ok(people);
        }

        // GET: api/People/hardcodad
        [HttpGet("hardcoded")]
        public async Task<ActionResult<IEnumerable<string>>> Getpersons_Hardcoded()
        {
            var people = await _person_Rep.GetAllPeople_Hardcoded();
            return Ok(people);
        }

        [HttpGet("hardcoded_headers")]
        public async Task<ActionResult<IEnumerable<string>>> Getpersons_Hardcoded_headers([FromHeader] int quantity)
        {
            var people = await _person_Rep.GetAllPeople_Hardcoded(quantity);
            return Ok(people);
        }

        [HttpGet("hardcoded_parameter")]
        public async Task<ActionResult<IEnumerable<string>>> Getpersons_Hardcoded_parameters([FromQuery] int quantity)
        {
            var people = await _person_Rep.GetAllPeople_Hardcoded(quantity);
            return Ok(people);
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _person_Rep.GetPersonByIdAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("addPerson")]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            await _person_Rep.AddPersonAsync(person);
            return CreatedAtAction("GetPerson", new { id = person.id }, person);
        }

        [HttpPost("addPerson_requestBody")]
        public async Task<ActionResult<IEnumerable<string>>> addPerson_requestbody([FromBody] Person person)
        {
            await _person_Rep.AddPersonAsync(person);
            return CreatedAtAction("GetPerson", new { id = person.id }, person);
        }

        [HttpPost("addPerson_fromForm")]
        public async Task<ActionResult<IEnumerable<string>>> addPerson_from_form([FromForm] Person person)
        {
            await _person_Rep.AddPersonAsync(person);
            return CreatedAtAction("GetPerson", new { id = person.id }, person);
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.id)
            {
                return BadRequest();
            }
            await _person_Rep.UpdatePersonAsync(person);
            return NoContent();
        }


        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _person_Rep.GetPersonByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            await _person_Rep.DeletePersonAsync(id);
            return NoContent();
        }
    }
}
