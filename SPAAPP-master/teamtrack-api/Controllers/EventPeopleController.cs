using Microsoft.AspNetCore.Mvc;
using teamtrack_api.Model;

namespace teamtrack_api.Controllers;

[ApiController] [Route("api/[controller]")] public class EventPeopleController : ControllerBase {
    private readonly DataContext context;
    public EventPeopleController(DataContext c)  {
        context = c;
    }
    [HttpGet] public IActionResult GetEventPeople() {
        return Ok(context.EventPeople);
    }   
    [HttpPost] public IActionResult Create([FromBody] EventPeople e) {
        var dbEvent = context.EventPeople?.Find(e.Id); 
        if (dbEvent == null) {
            context.EventPeople?.Add(e); 
            context.SaveChanges();
            return CreatedAtAction(nameof(GetEventPeople), new { e.Id }, e);
        }
        return Conflict();
    }
    [HttpPut("{id}")] public IActionResult Update(int? id, [FromBody] EventPeople e) {
        if (id != e.Id || !context.EventPeople!.Any(e => e.Id == id)) return NotFound();
        context.Update(e);
        context.SaveChanges();
        return NoContent();
    }
    [HttpDelete("{id}")] public IActionResult Delete(int id) {
        var eventPeopleToDelete = context.EventPeople?.Find(id);
        if (eventPeopleToDelete == null) return NotFound();
        context.EventPeople?.Remove(eventPeopleToDelete);
        context.SaveChanges();
        return NoContent();
    }
}

