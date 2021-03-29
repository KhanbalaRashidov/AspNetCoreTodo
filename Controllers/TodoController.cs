using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTodo.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;

        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }
        public async Task<IActionResult> Index()
        {
            var todoItems = await _todoItemService.GetIncompleteItemsAsync();

            var model = new TodoViewModel()
            {
                Items = todoItems
            };

            return View(model);
        }

        [HttpPost]
        public async Task <IActionResult> AddItem(NewTodoItem newItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var successful = await _todoItemService.AddItemAsync(newItem);
            if (!successful)
            {
                return BadRequest(new { err = "Could not add Item" });
            }
            return Ok();
        }

        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            var successful = await _todoItemService.MarkDoneAsync(id);
            if (!successful)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
