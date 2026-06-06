using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces;

public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetAllByUserIdAsync(Guid userId);
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task<TaskItem> CreateAsync(TaskItem task);
    Task<TaskItem> UpdateAsync(TaskItem task);
    Task DeleteAsync(Guid id);
}