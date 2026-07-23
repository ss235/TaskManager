using TaskManager.Application.DTOs.Tasks;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.API.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<TaskResponse>> GetAllTasksAsync(Guid userId)
    {
        var tasks = await _taskRepository.GetAllByUserIdAsync(userId);
        return tasks.Select(MapToResponse);
    }

    public async Task<TaskResponse> GetTaskByIdAsync(Guid id)
    {
        var task = await _taskRepository.GetByIdAsync(id)
            ?? throw new Exception("Task not found");
        return MapToResponse(task);
    }

    public async Task<TaskResponse> CreateTaskAsync(Guid userId, CreateTaskRequest request)
    {
        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            DueDate = DateTime.SpecifyKind((DateTime)request.DueDate,DateTimeKind.Utc),
            UserId = userId
        };

        var created = await _taskRepository.CreateAsync(task);
        return MapToResponse(created);
    }

    public async Task<TaskResponse> UpdateTaskAsync(Guid id, UpdateTaskRequest request)
    {
        var task = await _taskRepository.GetByIdAsync(id)
            ?? throw new Exception("Task not found");

        task.Title = request.Title;
        task.Description = request.Description;
        task.Status = request.Status;
        task.Priority = request.Priority;
        task.DueDate = DateTime.SpecifyKind((DateTime)request.DueDate, DateTimeKind.Utc);

        var updated = await _taskRepository.UpdateAsync(task);
        return MapToResponse(updated);
    }

    public async Task DeleteTaskAsync(Guid id)
    {
        var task = await _taskRepository.GetByIdAsync(id)
            ?? throw new Exception("Task not found");
        await _taskRepository.DeleteAsync(task.Id);
    }

    private static TaskResponse MapToResponse(TaskItem task) => new()
    {
        Id = task.Id,
        Title = task.Title,
        Description = task.Description,
        Status = task.Status,
        Priority = task.Priority,
        DueDate = task.DueDate,
        CreatedAt = task.CreatedAt
    };
}