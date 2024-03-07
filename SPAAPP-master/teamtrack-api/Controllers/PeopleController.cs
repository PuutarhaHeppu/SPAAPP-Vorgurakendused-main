using Microsoft.AspNetCore.Mvc;
using teamtrack_api.Model;

namespace teamtrack_api.Controllers;

[ApiController] [Route("api/[controller]")] public class PeoplesController : ControllerBase {
    private readonly DataContext context;
    public PeoplesController(DataContext c)  {
        context = c;
    }
    [HttpGet] public IActionResult GetPeoples() {
        return Ok(context.PeopleList);
    }   
    [HttpPost] public IActionResult Create([FromBody] People e) {
        var dbPeople = context.PeopleList?.Find(e.Id); 
        if (dbPeople == null) {
            context.PeopleList?.Add(e); 
            context.SaveChanges();
            return CreatedAtAction(nameof(GetPeoples), new { e.Id }, e);
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
        var PeopleToDelete = context.PeopleList?.Find(id);
        if (PeopleToDelete == null) return NotFound();
        context.PeopleList?.Remove(PeopleToDelete);
        context.SaveChanges();
        return NoContent();
    }
}

