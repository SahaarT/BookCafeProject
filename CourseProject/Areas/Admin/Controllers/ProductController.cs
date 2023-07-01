using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.Product.GetAll();
            return View(products);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(Product product)
        {

            if (product.Name == "")
            {
                ModelState.AddModelError("Name Field", "Name field cannot be empty for the product");
                return View();
            }
            else
            {
                _unitOfWork.Product.Add(product);
                _unitOfWork.Save();
                TempData["Success"] = "Product created successfully";
            }

            return RedirectToAction("Index");
        }

        //Get
        public IActionResult Delete(Product product)
        {
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Product obj)
        {
            if (obj.Id == null || obj.Id == 0)
            {
                return NotFound();
            }

            Product product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == obj.Id);
            if (product == null) { return NotFound(); }
            else
            {
                _unitOfWork.Product.Remove(obj);
            }
            return RedirectToAction("Index");
        }

        //Get
        public IActionResult Edit(Product obj)
        {
            if (obj.Id == null || obj.Id == 0)
            {
                return NotFound();
            }

            Product product = _unitOfWork.Product.GetFirstOrDefault(a => a.Id == obj.Id);
            if (product == null) { return NotFound(); }
            else
            {
                return View(obj);
            }
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(Product obj)
        {
            if (obj.Id == null || obj.Id == 0)
            {
                return NotFound();
            }

            Product product = _unitOfWork.Product.GetFirstOrDefault(a => a.Id == obj.Id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Modified Product Successfully";
            }
            return RedirectToAction("Index");
        }
    }
}
