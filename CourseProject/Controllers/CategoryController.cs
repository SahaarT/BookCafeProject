using CourseProject.Data;
using CourseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _db.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name == "")
            {
                ModelState.AddModelError("Name field", "The Name Field Cannot Be Empty!!!!");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["Success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        //Get
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            else
            {
                Category category = _db.Categories.Find(Id);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
            TempData["Success"] = "Category Updated Successfully";
            return RedirectToAction("Index");
        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return View();
            }
            Category category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                return View(category);
            }
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Category category)
        {            
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["Success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
