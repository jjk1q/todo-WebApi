using System.Data.SqlTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

public class JsonToDoStorage : IStorage
{
    private const string FilePath = "tasks.json";
    private readonly JsonSerializerOptions options = new JsonSerializerOptions{ WriteIndented = true };
    private List<ToDoItem> items;
    public JsonToDoStorage()
    {
        options.Converters.Add(new JsonStringEnumConverter());
        items = Load();
    }

    public List<ToDoItem> GetAll()
    {
        return items.ToList();
    }
    public ToDoItem? GetById(int id)
    {
        return items.FirstOrDefault(i => i.Id == id);
    }
    public void Add(ToDoItem item)
    {
        int id;
        if(items.Count == 0)
        {
            id = 1;
        }
        else
        {
            id = items.Max(i => i.Id) + 1;
        }
        item.AssignId(id);
        items.Add(item);
        Save(items);  
    }
    public void Update(ToDoItem item)
    {
        int index = items.FindIndex(i => i.Id == item.Id);
        if(index == -1)
            throw new ArgumentException($"item with {item.Id} id wasn't found");
        Save(items);
    }
    public void Delete(int id)
    {
        int index = items.FindIndex(i => i.Id == id);
        if(index == -1) 
            throw new ArgumentException($"item with {id} id wasn't found");
        items.RemoveAt(index);
        Save(items);
    }
    private void Save(List<ToDoItem> itemsToSave)
    {

        string jsonString = JsonSerializer.Serialize(itemsToSave, options);
        File.WriteAllText(FilePath, jsonString);
    }
    
    private List<ToDoItem> Load()
    {
        if(!File.Exists(FilePath))
            return new List<ToDoItem>();

        string rawJson = File.ReadAllText(FilePath);
        if(string.IsNullOrEmpty(rawJson))
            return new List<ToDoItem>();
 
        var restoredTasks = JsonSerializer.Deserialize<List<ToDoItem>>(rawJson, options) ?? new List<ToDoItem>();

        return restoredTasks;
    }
}