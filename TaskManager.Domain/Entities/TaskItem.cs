using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.Common;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities;

public class TaskItem : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TodoStatus Status { get; set; } = TodoStatus.Todo;
    public Priority Priority { get; set; } = Priority.Medium;
    public DateTime? DueDate { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
