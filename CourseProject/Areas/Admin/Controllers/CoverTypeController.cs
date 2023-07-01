using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;
using Microsoft.AspNetCore.Mvc;


namespace CourseProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> categories = _unitOfWork.CoverType.GetAll();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {
            if (coverType.Name == "")
            {
                ModelState.AddModelError("Name field", "The Name Field Cannot Be Empty!!!!");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(coverType);
                _unitOfWork.Save();
                TempData["Success"] = "CoverType Created Successfully";
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
                CoverType coverType = _unitOfWork.CoverType.GetFirstOrDefault(a => a.Id == Id);
                if (coverType == null)
                {
                    return NotFound();
                }
                return View(coverType);
            }
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(CoverType coverType)
        {
            _unitOfWork.CoverType.Update(coverType);
            _unitOfWork.Save();
            TempData["Success"] = "CoverType Updated Successfully";
            return RedirectToAction("Index");
        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return View();
            }
            CoverType coverType = _unitOfWork.CoverType.GetFirstOrDefault(a => a.Id == id);
            if (coverType == null)
            {
                return NotFound();
            }
            else
            {
                return View(coverType);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(CoverType coverType)
        {
            _unitOfWork.CoverType.Remove(coverType);
            _unitOfWork.Save();
            TempData["Success"] = "CoverType Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
