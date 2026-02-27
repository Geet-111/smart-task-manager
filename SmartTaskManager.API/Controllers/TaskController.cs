using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartTaskManager.API.Data;
using SmartTaskManager.API.DTOs;
using SmartTaskManager.API.Models;
using System.Security.Claims;

namespace SmartTaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private int CurrentUserId => int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDto dto)
        {
            var userId = CurrentUserId;

            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = userId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return Ok(new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetMyTasks()
        {
            var userId = CurrentUserId;

            var tasks = await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return Ok(tasks.Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt
            }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto dto)
        {
            var userId = CurrentUserId;

            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null)
                return NotFound("Task not found");
            if(dto.Title != null)
                 task.Title = dto.Title;
            if(dto.Description != null)
                 task.Description = dto.Description;
            if(dto.IsCompleted.HasValue)
                 task.IsCompleted = dto.IsCompleted.Value;

            await _context.SaveChangesAsync();

            return Ok(new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userId = CurrentUserId;
               
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null)
                return NotFound("Task not found");

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return Ok( new { message = "Task deleted successfully" });
        }
    }
}