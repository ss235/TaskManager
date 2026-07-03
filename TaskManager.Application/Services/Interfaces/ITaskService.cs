using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.DTOs.Tasks;

namespace TaskManager.Application.Services.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskResponse>> GetAllTasksAsync(Guid userId);
    Task<TaskResponse> GetTaskByIdAsync(Guid id);
    Task<TaskResponse> CreateTaskAsync(Guid userId, CreateTaskRequest request);
    Task<TaskResponse> UpdateTaskAsync(Guid id, UpdateTaskRequest request);
    Task DeleteTaskAsync(Guid id);
}
