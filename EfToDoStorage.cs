public class EfToDoStorage : IStorage
{
    private readonly AppDbContext context;

    public EfToDoStorage(AppDbContext context)
    {
        this.context = context;
    }

    public List<ToDoItem> GetAll()
    {
        return context.Tasks.ToList();
    }

    public ToDoItem? GetById(int id)
    {
        return context.Tasks.Find(id);
    }

    public void Add(ToDoItem item)
    {
        context.Tasks.Add(item);
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        var item = GetById(id);
        if(item == null) return;
        context.Tasks.Remove(item);
        context.SaveChanges();
    }

    public void Update(ToDoItem item)
    {
        context.Tasks.Update(item);
        context.SaveChanges();
    }

}