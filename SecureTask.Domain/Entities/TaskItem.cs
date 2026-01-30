namespace SecureTask.Domain.Entities;

public class TaskItem
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string OwnerUserId { get; set; } = null!;
    public bool IsCompleted { get; set; }
}
