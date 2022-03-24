using Microsoft.AspNetCore.Mvc;
using MyApp.DataAccess.Repository.IRepository;

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
    }
}
