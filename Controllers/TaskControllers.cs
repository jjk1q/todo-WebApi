
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
    public async Task<ActionResult<List<TaskResponseDTO>>> GetAll()
    {
        var tasks = await storage.GetAllAsync();
        var response = tasks.Select(item => MapToResponse(item)).ToList();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskResponseDTO>> GetById(int id)
    {
        var item = await storage.GetByIdAsync(id);
        if(item == null)
        {
            return NotFound();
        }
        return Ok(MapToResponse(item));
    }
    [HttpPost]
    public async Task<ActionResult<TaskResponseDTO>> Create([FromBody] CreateTaskDTO dto)
    {
        var item = new ToDoItem(dto.Title!, dto.Description, dto.Deadline);
        await storage.AddAsync(item);
        var taskResponse = MapToResponse(item);
        return CreatedAtAction(nameof(GetById), new {id = item.Id}, taskResponse);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await storage.GetByIdAsync(id);
        if(item == null)
        {
            return NotFound();
        }
        await storage.DeleteAsync(id);
        return NoContent();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTaskDTO dto)
    {
        var item = await storage.GetByIdAsync(id);
        if(item == null)
        {
            return NotFound();
        }

        item.AssignData(dto);
        await storage.UpdateAsync(item);
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