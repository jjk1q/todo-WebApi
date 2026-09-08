public interface IStorage
{
    List<ToDoItem> GetAll();
    ToDoItem? GetById(int id);
    void Add(ToDoItem item);
    void Update(ToDoItem item);
    void Delete(int id);
}