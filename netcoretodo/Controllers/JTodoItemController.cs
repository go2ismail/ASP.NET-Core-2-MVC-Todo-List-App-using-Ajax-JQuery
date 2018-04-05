using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using netcoretodo.Data;
using netcoretodo.Models;

namespace netcoretodo.Controllers
{
    public class JTodoItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JTodoItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JTodoItem
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TodoItem.Include(t => t.todo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: JTodoItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem
                .Include(t => t.todo)
                .SingleOrDefaultAsync(m => m.todoItemId == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // GET: JTodoItem/Create
        public IActionResult Create()
        {
            ViewData["todoId"] = new SelectList(_context.Todo, "todoId", "todoId");
            return View();
        }

        // POST: JTodoItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("todoItemId,title,description,todoId")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["todoId"] = new SelectList(_context.Todo, "todoId", "todoId", todoItem.todoId);
            return View(todoItem);
        }

        // GET: JTodoItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem.SingleOrDefaultAsync(m => m.todoItemId == id);
            if (todoItem == null)
            {
                return NotFound();
            }
            ViewData["todoId"] = new SelectList(_context.Todo, "todoId", "todoId", todoItem.todoId);
            return View(todoItem);
        }

        // POST: JTodoItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("todoItemId,title,description,todoId")] TodoItem todoItem)
        {
            if (id != todoItem.todoItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoItemExists(todoItem.todoItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["todoId"] = new SelectList(_context.Todo, "todoId", "todoId", todoItem.todoId);
            return View(todoItem);
        }

        // GET: JTodoItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem
                .Include(t => t.todo)
                .SingleOrDefaultAsync(m => m.todoItemId == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // POST: JTodoItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todoItem = await _context.TodoItem.SingleOrDefaultAsync(m => m.todoItemId == id);
            _context.TodoItem.Remove(todoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoItemExists(int id)
        {
            return _context.TodoItem.Any(e => e.todoItemId == id);
        }
    }
}
