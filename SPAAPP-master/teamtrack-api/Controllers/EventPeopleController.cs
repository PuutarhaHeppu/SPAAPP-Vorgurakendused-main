using Microsoft.AspNetCore.Mvc;
using teamtrack_api.Model;

namespace teamtrack_api.Controllers;

[ApiController] [Route("api/[controller]")] public class EventPeoplesController : ControllerBase {
    private readonly DataContext context;
    public EventPeoplesController(DataContext c)  {
        context = c;
    }
    [HttpGet] public IActionResult GetEventPeoples() {
        return Ok(context.EventPeopleList);
    }   
    [HttpPost] public IActionResult Create([FromBody] EventPeople e) {
        var dbEventPeople = context.EventPeopleList?.Find(e.Id); 
        if (dbEventPeople == null) {
            context.EventPeopleList?.Add(e); 
            context.SaveChanges();
            return CreatedAtAction(nameof(GetEventPeoples), new { e.Id }, e);
        }
        return Conflict();
    }
    [HttpPut("{id}")] public IActionResult Update(int? id, [FromBody] EventPeople e) {
        if (id != e.Id || !context.EventPeopleList!.Any(e => e.Id == id)) return NotFound();
        context.Update(e);
        context.SaveChanges();
        return NoContent();
    }
    [HttpDelete("{id}")] public IActionResult Delete(int id) {
        var EventPeopleToDelete = context.EventPeopleList?.Find(id);
        if (EventPeopleToDelete == null) return NotFound();
        context.EventPeopleList?.Remove(EventPeopleToDelete);
        context.SaveChanges();
        return NoContent();
    }
}

