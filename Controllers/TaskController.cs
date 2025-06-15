using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using TaskMasterApi.Models;

namespace TaskMasterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private static List<TaskItem> tasks = new List<TaskItem>();
        private static int nextId = 1;

        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> GetAll() => Ok(tasks);

        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetById(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPost]
        public ActionResult<TaskItem> Create(TaskItem task)
        {
            task.Id = nextId++;
            tasks.Add(task);
            return CreatedAtAction(nameof(GetById), new { id = task.Id });
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, TaskItem updatedtask)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
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