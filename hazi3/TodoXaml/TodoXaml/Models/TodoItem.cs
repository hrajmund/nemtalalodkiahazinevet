using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoXaml.Models
{
    public class TodoItem
    {
        public DateTimeOffset Deadline { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public Priority Priority { get; set; }
        public string Title { get; set; }
    }
}
