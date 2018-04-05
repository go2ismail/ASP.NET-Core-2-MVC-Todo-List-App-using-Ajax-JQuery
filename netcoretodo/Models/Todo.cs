using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcoretodo.Models
{
    public class Todo
    {
        public int todoId { get; set; }
        public string title { get; set; }
        public string description { get; set; }

        public ICollection<TodoItem> todoItem { get; set; }
    }
}
