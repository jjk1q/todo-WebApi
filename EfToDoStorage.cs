using Microsoft.EntityFrameworkCore;

public class EfToDoStorage : IStorage
{
    private readonly AppDbContext context;

    public EfToDoStorage(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<List<ToDoItem>> GetAllAsync()
    {
        return await context.Tasks.ToListAsync();
    }

    public async Task<ToDoItem?> GetByIdAsync(int id)
    {
        return await context.Tasks.FindAsync(id);
    }

    public async Task AddAsync(ToDoItem item)
    {
        await context.Tasks.AddAsync(item);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await GetByIdAsync(id);
        if(item == null) return;
        context.Tasks.Remove(item);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ToDoItem item)
    {
        context.Tasks.Update(item);
        await context.SaveChangesAsync();
    }

}