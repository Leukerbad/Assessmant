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
        public IEnumerable<Person> Get()
        {
            return context.People;
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public Person Get(string id)
        {
            return context.GetPersonById(id);
        }

        // POST api/<PersonController>
        [HttpPost]
        public void Post([FromBody] Person person)
        {
            context.CreatePerson(person);
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Person person)
        {
            context.UpdatePerson(id, person);
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            context.DeletePerson(context.GetPersonById(id));
        }
    }
}
