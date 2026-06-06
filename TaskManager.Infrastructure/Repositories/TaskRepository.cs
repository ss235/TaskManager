using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetAllByUserIdAsync(Guid userId)
        => await _context.TaskItems.Where(t => t.UserId == userId).ToListAsync();

    public async Task<TaskItem?> GetByIdAsync(Guid id)
        => await _context.TaskItems.FindAsync(id);

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<TaskItem> UpdateAsync(TaskItem task)
    {
        task.UpdatedAt = DateTime.UtcNow;
        _context.TaskItems.Update(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task DeleteAsync(Guid id)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task != null)
        {
            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}