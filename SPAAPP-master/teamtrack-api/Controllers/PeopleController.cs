using Microsoft.AspNetCore.Mvc;
using teamtrack_api.Model;

namespace teamtrack_api.Controllers;

[ApiController] [Route("api/[controller]")] public class PeopleController : ControllerBase {
    private readonly DataContext context;
    public PeopleController(DataContext c)  {
        context = c;
    }
    [HttpGet] public IActionResult GetPeople() {
        return Ok(context.PeopleList);
    }   
    [HttpPost] public IActionResult Create([FromBody] People e) {
        var dbEvent = context.PeopleList?.Find(e.Id); 
        if (dbEvent == null) {
            context.PeopleList?.Add(e); 
            context.SaveChanges();
            return CreatedAtAction(nameof(GetPeople), new { e.Id }, e);
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
        var peopleToDelete = context.PeopleList?.Find(id);
        if (peopleToDelete == null) return NotFound();
        context.PeopleList?.Remove(peopleToDelete);
        context.SaveChanges();
        return NoContent();
    }
}

