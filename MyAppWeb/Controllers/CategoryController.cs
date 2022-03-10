using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.DataAccess;
using MyApp.DataAccess.Repository.IRepository;
using MyApp.Models;

namespace MyAppWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View(_unitOfWork.category.GetAll());
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
                _unitOfWork.category.Add(obj);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category category = _unitOfWork.category.GetFirstOrDefault(x => x.Id == id);
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
                _unitOfWork.category.Update(obj);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category category = _unitOfWork.category.GetFirstOrDefault(x => x.Id == id);
            if (category == null) return NotFound();

            return View(category);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {
            _unitOfWork.category.Remove(obj);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }
    }
}
