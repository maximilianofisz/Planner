using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Planner.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public bool IsDone { get; set; }

        public bool IsRecurring { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Be more descriptive, please")]
        [MaxLength(200, ErrorMessage = "Geez, be more concise, please")]
        public string Title { get; set; }


    }
}
