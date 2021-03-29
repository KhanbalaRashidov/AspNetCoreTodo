using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTodo.Services
{
    public class TodoItemsService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;
        public TodoItemsService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<bool> AddItemAsync(NewTodoItem newItem)
        {
            var entity = new TodoItem
            {
                Id = Guid.NewGuid(),
                IsDone = false,
                Title = newItem.Title,
                DueAt = DateTimeOffset.Now.AddDays(3)
            };
            _context.Items.Add(entity);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync()
        {
            return await _context.Items
                  .Where(x => x.IsDone == false)
                  .ToArrayAsync();
        }

        public async Task<bool> MarkDoneAsync(Guid id)
        {
            var items = await _context.Items
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
            if (items==null)
            {
                return false;
            }
            items.IsDone = true;
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
