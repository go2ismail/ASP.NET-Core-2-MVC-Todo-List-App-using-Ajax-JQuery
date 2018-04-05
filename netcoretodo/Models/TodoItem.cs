using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcoretodo.Models
{
    public class TodoItem
    {
        public int todoItemId { get; set; }
        public string title { get; set; }
        public string description { get; set; }

        public int todoId { get; set; }
        public Todo todo { get; set; }
    }
}
