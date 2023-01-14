using Microsoft.AspNetCore.Mvc;
using PersonService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonContext context;
        public PersonController()
        {
            context = new PersonContext();
        }

        // GET: api/<PersonController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(context.People);
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                return Ok(context.GetPersonById(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST api/<PersonController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            try
            {
                context.CreatePerson(person);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Person person)
        {
            try
            {
                context.UpdatePerson(id, person);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                context.DeletePerson(context.GetPersonById(id));
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
