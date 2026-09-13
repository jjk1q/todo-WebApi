
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
    public ActionResult<List<TaskResponseDTO>> GetAll()
    {
        var tasks = storage.GetAll();
        var response = tasks.Select(item => MapToResponse(item)).ToList();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public ActionResult<TaskResponseDTO> GetById(int id)
    {
        var item = storage.GetById(id);
        if(item == null)
        {
            return NotFound();
        }
        return Ok(MapToResponse(item));
    }
    [HttpPost]
    public ActionResult<TaskResponseDTO> Create([FromBody] CreateTaskDTO dto)
    {
        var item = new ToDoItem(dto.Title!, dto.Description, dto.Deadline);
        storage.Add(item);
        var taskResponse = MapToResponse(item);
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
    public IActionResult Update(int id, UpdateTaskDTO dto)
    {
        var item = storage.GetById(id);
        if(item == null)
        {
            return NotFound();
        }

        item.AssignData(dto);
        storage.Update(item);
        return NoContent();
    }

    private TaskResponseDTO MapToResponse(ToDoItem item)
    {
        return new TaskResponseDTO
        {
            Id = item.Id, Title = item.Title, Deadline = item.Deadline,
            Description = item.Description, Status = item.Status,
            CreationDate = item.CreationDate, IsOverdue = item.IsOverdue
        };
    }
}