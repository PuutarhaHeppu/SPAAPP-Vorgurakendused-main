using Microsoft.AspNetCore.Mvc;
using teamtrack_api.Model;

namespace teamtrack_api.Controllers;

[ApiController] [Route("api/[controller]")] public class PeopleController : ControllerBase {
    private readonly DataContext context;
    public EventsController(DataContext c)  {
        context = c;
    }
    [HttpGet] public IActionResult GetPeople() {
        return Ok(context.PeopleList);
    }   
    [HttpPost] public IActionResult Create([FromBody] People e) {
        var dbPeopleList = context.PeopleList?.Find(e.Id); 
        if (dbPeopleList == null) {
            context.PeopleList?.Add(e); 
            context.SaveChanges();
            return CreatedAtAction(nameof(GetPeopleList), new { e.Id }, e);
        }
        return Conflict();
    }
    [HttpPut("{id}")] public IActionResult Update(int? id, [FromBody] People e) {
        if (id != e.Id || !context.PeopleList!.Any(e => e.Id == id)) return NotFound();
        context.Update(e);
        context.SaveChanges();
        return NoContent();
    }
    [HttpDelete("{id}")] public IActionResult Delete(int id) {
        var PeopleListToDelete = context.PeopleList?.Find(id);
        if (PeopleListToDelete == null) return NotFound();
        context.PeopleList?.Remove(PeopleListToDelete);
        context.SaveChanges();
        return NoContent();
    }
}

