using System.ComponentModel;
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
    public ActionResult<TaskResponseDTO> Create([FromBody] CreateTaskDTO dto)
    {
        var item = new ToDoItem(dto.Title, dto.Description, dto.Deadline);
        storage.Add(item);
        var taskResponse = new TaskResponseDTO{Id = item.Id, Title = item.Title, Deadline = item.Deadline, Description = item.Description, Status = item.Status, CreationDate = item.CreationDate, IsOverdue = item.IsOverdue};
        return CreatedAtAction(nameof(GetById), new {id = item.Id}, taskResponse);
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