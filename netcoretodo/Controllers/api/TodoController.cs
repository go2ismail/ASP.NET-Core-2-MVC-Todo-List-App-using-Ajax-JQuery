using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netcoretodo.Data;
using netcoretodo.Models;

namespace netcoretodo.Controllers.api
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Todo
        [HttpGet]
        public IEnumerable<Todo> GetTodo()
        {
            return _context.Todo;
        }

        // GET: api/Todo/5
        [HttpGet]
        public async Task<IActionResult> GetTodoById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = await _context.Todo.SingleOrDefaultAsync(m => m.todoId == id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo([FromRoute] int id, [FromBody] Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todo.todoId)
            {
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Todo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostTodo(Todo todo)
        {
            try
            {
                if (todo != null && todo.todoId == 0)
                {
                    _context.Todo.Add(todo);

                    await _context.SaveChangesAsync();


                }
                else if (todo.todoId > 0)
                {
                    _context.Entry(todo).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return StatusCode(500, new { Message = "Object null." });
                }

                return StatusCode(200, todo);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = ex.Message });
            }
            

            
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = await _context.Todo.SingleOrDefaultAsync(m => m.todoId == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todo.Remove(todo);
            await _context.SaveChangesAsync();

            return Ok(todo);
        }

        private bool TodoExists(int id)
        {
            return _context.Todo.Any(e => e.todoId == id);
        }
    }
}