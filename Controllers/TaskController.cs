using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskMasterApi.Data;
using TaskMasterApi.Models;

namespace TaskMasterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskDbContext _context;
        public TaskController(TaskDbContext context) {
            _context = context;
        }
        private static List<TaskItem> tasks = new List<TaskItem>();
        private static int nextId = 1;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll()
        {
            return Ok(await _context.Tasks.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> Create(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            // return Ok(task); 
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, TaskItem updatedtask)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();
            task.Description = updatedtask.Description;
            task.IsCompleted = updatedtask.IsCompleted;
            task.Title = updatedtask.Title;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();
            tasks.Remove(task);
            return NoContent();
        }

    }
}