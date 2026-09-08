using Microsoft.AspNetCore.Mvc;

[Route("tasks")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly IStorage storage;
    public TasksController(IStorage storage)
    {
        this.storage = storage;
    }
    [HttpGet]
    public List<ToDoItem> GetAll()
    {
        return storage.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<ToDoItem> GetById(int id)
    {
        var item = storage.GetById(id);
        if(item == null)
        {
            return NotFound();
        }
        return item;
    }
    [HttpPost]
    public ActionResult<ToDoItem> Create([FromBody] ToDoItem item)
    {
        storage.Add(item);
        return CreatedAtAction(nameof(GetById), new {id = item.Id}, item);
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = storage.GetById(id);
        if(item == null)
        {
            return NotFound();
        }
        storage.Delete(id);
        return NoContent();
    }
    [HttpPut("{id}")]
    public IActionResult Update(int id, ToDoItem updated)
    {
        var item = storage.GetById(id);
        if(item == null)
        {
            return NotFound();
        }
        item.AssignData(updated);
        storage.Update(item);
        return NoContent();
    }
}