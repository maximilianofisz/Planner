using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Planner.Helpers;
using Planner.Models;
using Dapper;
using System.Data.SqlClient;

namespace Planner.Models
{
    public class ToDoItemViewModel
    {
        public ToDoItemViewModel()
        {
            using (var db = DbHelper.GetConnection())
            {
                this.EditableItem = new ToDoItem();
                this.ToDoItems = db.Query<ToDoItem>("SELECT * FROM toDo ORDER BY Date ASC").ToList();
            }
        }

        public ToDoItem EditableItem { get; set; }

        public List<ToDoItem> ToDoItems { get; set; }


    }
}
