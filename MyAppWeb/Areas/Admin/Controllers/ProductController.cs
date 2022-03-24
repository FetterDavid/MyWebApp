using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyApp.DataAccess.Repository.IRepository;
using MyApp.Models;
using MyApp.Models.ViewModels;

namespace MyAppWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                product = new(),
                categoryList = _unitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                coverTypeList = _unitOfWork.CoverType.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                //Create
                return View(productVM);
            }
            else
            {
                //Update

            }

            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile file)
        {
           if(ModelState.IsValid)
           {
                //_unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                return Redirect("Index");
           }

            return View(obj);
        }
    }
}
