using AspNetCoreTodo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync();
        Task<bool> AddItemAsync(NewTodoItem newItem);
        Task<bool> MarkDoneAsync(Guid id);
    }
}
