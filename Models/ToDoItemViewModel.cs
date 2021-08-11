using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Planner.Helpers;
using Planner.Models;
using Dapper;
using Dapper.Contrib;
using System.Data.SqlClient;
using System.Data;

namespace Planner.Models
{
    public class ToDoItemViewModel
    {
        public ToDoItemViewModel()
        {
            using (var db = DbHelper.GetConnection())
            {
                this.EditableItem = new ToDoItem();
                //this.ToDoItems = db.Query<ToDoItem>("SELECT * FROM toDo ORDER BY Date ASC").ToList();
                this.ToDoItems = db.Query<ToDoItem>("GetAllData", null, null, true, null, CommandType.StoredProcedure).ToList();
            }
        }

        public ToDoItem EditableItem { get; set; }

        public List<ToDoItem> ToDoItems { get; set; }

        public void ClearHistory()
        {
            using (var db = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@YesterdaysDate", DateTime.Now.AddDays(-1));
                db.Execute("ClearHistory", parameters, null, null, CommandType.StoredProcedure);
            }
        }
    }
}
