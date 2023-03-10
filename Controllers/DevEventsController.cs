using AwasomeDevEvents.Entities;
using AwasomeDevEvents.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//O que é rota

namespace AwasomeDevEvents.Controllers
{
    [Route("api/[dev-events]")]
    [ApiController]
    public class DevEventsController : ControllerBase
    {
        private readonly DevEventsDbContext _context;

        public DevEventsController(DevEventsDbContext context)
        {
            _context = context;
        }

        // api/dev-events GET
        [HttpGet]
        public IActionResult GetAll() 
        {
            var devEvents = _context.DevEvents.Where(d => !d.IsDeleted).ToList();

            return Ok(devEvents);
        }

        // api/dev-events/ 'Guid' GET
        [HttpGet("{Id}")]
        public IActionResult GetById(Guid id)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null) 
            {
                return NotFound();
            }

            return Ok(devEvent);
        }

        // api/dev-events/ POST
        [HttpPost]
        public IActionResult Post(DevEvent devEvent)
        {
            _context.DevEvents.Add(devEvent);

            return CreatedAtAction(nameof(GetById), new { id = devEvent.Id }, devEvent);
        }

        //api/dev-events/ 'Guid' PUT
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, DevEvent input)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Update(input.Title, input.Description, input.StartDate, input.EndDate);

            return NoContent();
        }

        // api/dev-events/ 'Guid' DELETE
        [HttpDelete("{id}")]

        public IActionResult Delete(Guid id) 
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Delete();

            return NoContent();
        }
    }
}
