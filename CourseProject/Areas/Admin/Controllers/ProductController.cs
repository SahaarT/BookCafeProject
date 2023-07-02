using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                })
            };            

            if (id == null || id == 0)
            {
                //create Product
                //ViewData["CoverTypes"] = CoverTypes;
                //ViewBag.Categories = Categories;

                return View(productVM);
            }
            else
            {
                //update product                               
                return View(productVM);
            }
        }


        //[HttpPost, ActionName("Edit")]
        //[ValidateAntiForgeryToken]
        //public IActionResult UpsertPost(Product obj)
        //{
        //    if (obj.Id == null || obj.Id == 0)
        //    {
        //        return NotFound();
        //    }

        //    Product product = _unitOfWork.Product.GetFirstOrDefault(a => a.Id == obj.Id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        _unitOfWork.Product.Update(obj);
        //        _unitOfWork.Save();
        //        TempData["Success"] = "Modified Product Successfully";
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}
