using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyApp.DataAccess.Repository.IRepository;
using MyApp.Models;
using MyApp.Models.ViewModels;

namespace MyAppWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Get
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
                productVM.product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);
            }
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
           if(ModelState.IsValid)
           {
                //ImageUrl
                string wwwRoot = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRoot, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRoot, obj.product.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.product.ImageUrl = @"\images\products\" + fileName + extension;
                }

                if (obj.product.Id == 0 )
                {
                    _unitOfWork.Product.Add(obj.product);
                    TempData["success"] = "Category created successfully";
                }
                else
                {
                    _unitOfWork.Product.Update(obj.product);
                    TempData["success"] = "Category edited successfully";
                }

                _unitOfWork.Save();
                return Redirect("Index");
           }

            return View(obj);
        }


    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    { 
        var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
        return Json(new { data = productList });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);

        if(obj==null)
        {
             return Json(new { success = false, message = "Error while deleting" });
        }

        var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));

        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }

        _unitOfWork.Product.Remove(obj);
        _unitOfWork.Save();

        return Json(new { success = true, message = "Delet Successful" });
    }
    #endregion

    }
}
