using Microsoft.AspNetCore.Mvc;
using MyApp.DataAccess.Repository.IRepository;
using MyApp.Models;

namespace MyAppWeb.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {
        IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View(_unitOfWork.CoverType.GetAll());
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            { 
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();

            CoverType obj = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);

            if(obj == null) return NotFound();

            return View(obj);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();

            CoverType obj = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);

            if (obj == null) return NotFound();

            return View(obj);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CoverType obj)
        {
            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
