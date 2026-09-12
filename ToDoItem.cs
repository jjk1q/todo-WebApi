using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ItemStatus
{
    Completed,
    Uncompleted
}
public class ToDoItem
{
    private int _id;
    private string _titel;
    public int Id
    {
        get => _id;
        private set
        {
            if(value < 0)
                throw new ArgumentException("Id cannot be negative");
            _id = value;
        }
    }
    public string Title
    {
        get => _titel;
        private set
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Tittle cannot be empty");
            _titel = value;
        }
    }
    public string Description{get;private set;}
    public ItemStatus Status{get;private set;} = ItemStatus.Uncompleted;
    public DateTime Deadline{get;private set;}
    public DateTime CreationDate{get;private set;}
    [JsonIgnore]
    public string FormatedDateTime
    {
        get
        {
            if(Deadline.TimeOfDay == TimeSpan.Zero)
            {
                return Deadline.ToString("dd.MM.yyyy");
            }
            return Deadline.ToString("dd.MM.yyyy HH:mm");
        }
    }
    [JsonIgnore]
    public bool IsOverdue => DateTime.Now > Deadline;

    public ToDoItem(string title, string description, DateTime deadline)
    {
        if(deadline < DateTime.Now)
            throw new ArgumentException("Invalid deadline date");

        _titel = title;
        Description = description;
        Deadline = deadline;
        CreationDate = DateTime.Now;
    }
    
    [JsonConstructor]
    public ToDoItem(int id, string title, string description, ItemStatus status, DateTime deadline, DateTime creationDate)
    {
        _id = id;
        _titel = title;
        Description = description;
        Status = status;
        Deadline = deadline;
        CreationDate = creationDate;
    }


    public void ChangeStatus(ItemStatus status)
    {
        ItemStatus newStatus = (ItemStatus)status;
        this.Status = newStatus;   
    }

    public void AssignId(int id)
    {
        if(id == 0)
            throw new ArgumentException("id cannot be 0");
        Id = id;
    }

    public void AssignData(ToDoItem item)
    {
        _titel = item.Title;
        Description = item.Description;
        Deadline = item.Deadline;
        Status = item.Status;
    }

}