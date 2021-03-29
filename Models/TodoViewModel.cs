using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTodo.Models
{
    public class TodoViewModel
    {
        public IEnumerable<TodoItem> Items { get; set; }

    }
}
