using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.DataAccess;
using MyApp.DataAccess.Repository.IRepository;
using MyApp.Models;

namespace MyAppWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;

        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.GetAll());
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name==obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Name cannot exactly match the Display Order");
            }

            if (ModelState.IsValid)
            {
                _db.Add(obj);
                _db.Save();

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category category = _db.GetFirstOrDefault(x => x.Id == id);
            if (category == null) return NotFound();

            return View(category);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Name cannot exactly match the Display Order");
            }

            if (ModelState.IsValid)
            {
                _db.Update(obj);
                _db.Save();

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category category = _db.GetFirstOrDefault(x => x.Id == id);
            if (category == null) return NotFound();

            return View(category);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {
            _db.Remove(obj);
            _db.Save();

            return RedirectToAction("Index");
        }
    }
}
