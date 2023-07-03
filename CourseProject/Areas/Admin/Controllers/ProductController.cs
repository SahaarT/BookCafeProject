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
        public IWebHostEnvironment _webHostEnvironmet;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironmet)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironmet = webHostEnvironmet;
        }
        public IActionResult Index()
        {
            //IEnumerable<Product> products = _unitOfWork.Product.GetAll();
            return View();
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


        [HttpPost, ActionName("Upsert")]
        [ValidateAntiForgeryToken]
        public IActionResult UpsertPost(ProductVM productVM, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironmet.WebRootPath;
                if (imageFile != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(imageFile.FileName);
                    string uploadPath = Path.Combine(wwwRootPath, @"Images\Products");
                    using (FileStream fileStream = new FileStream(Path.Combine(uploadPath, fileName + extension), FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\Images\Products\" + fileName + extension;
                }
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["Success"] = "Product Saved Successfully";
                return RedirectToAction("Index");
            }
            return View(productVM);
        }

        #region API Calls
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll();
            return Json(new { data = productList});
        }
        #endregion
    }
}
