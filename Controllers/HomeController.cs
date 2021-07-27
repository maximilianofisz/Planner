using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Planner.Models;
using Planner.Helpers;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Planner.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ToDoItemViewModel viewModel = new ToDoItemViewModel();
            return View("Index", viewModel);
        }

        public IActionResult Edit(int id)
        {
            ToDoItemViewModel viewModel = new ToDoItemViewModel();
            viewModel.EditableItem = viewModel.ToDoItems.FirstOrDefault(x => x.Id == id);
            return View("Index", viewModel);
        }

        public IActionResult Delete(int id)
        {
            using (var db = DbHelper.GetConnection())
            {
                ToDoItem item = db.Get<ToDoItem>(id);
                if (item != null)
                    db.Delete(item);
                return RedirectToAction("Index");
            }
        }

        public IActionResult CreateUpdate(ToDoItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = DbHelper.GetConnection())
                {
                    if (viewModel.EditableItem.Id <= 0)
                    {

                        viewModel.EditableItem.Date = DateTime.Now;
                        db.Insert<ToDoItem>(viewModel.EditableItem);
                    }
                    else
                    {
                        ToDoItem dbItem = db.Get<ToDoItem>(viewModel.EditableItem.Id);
                        TryUpdateModelAsync<ToDoItem>(dbItem, "EditableItem");
                        db.Update<ToDoItem>(dbItem);
                    }
                }
                return RedirectToAction("Index");
            }
            else
                return View("Index", new ToDoItemViewModel());
        }

        public IActionResult ToggleIsDone(int id)
        {
            using (var db = DbHelper.GetConnection())
            {
                ToDoItem item = db.Get<ToDoItem>(id);
                if (item != null)
                {
                    item.IsDone = !item.IsDone;
                    db.Update<ToDoItem>(item);
                }
                return RedirectToAction("Index");
            }
        }
    }
}
