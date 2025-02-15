using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ToDoListManager _listManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ToDoListManager listManager, ILogger<HomeController> logger)
        {
            _listManager = listManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var todoItems = _listManager.GetToDoItems();

            return View(new ToDoListViewModel()
            {
                Items = todoItems.Select(ti => new Item()
                {
                    Id = ti.Id,
                    Text = ti.Text,
                    IsCompleted = ti.IsCompleted
                })
            });
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public IActionResult Add(Item item)
        {
            _listManager.AddToDoItem(new ToDoItem()
            {
                Text = item.Text,
                IsCompleted = false
            });

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _listManager.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult MarkComplete(int id)
        {
            _listManager.MarkComplete(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}