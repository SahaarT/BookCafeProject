using AppRazor.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppRazor.Pages
{
    [BindProperties]
    public class CreateModel : PageModel
    {        
        public Category Category { get; set; }
        private BulkyBookContext _db;
        public CreateModel(BulkyBookContext db)
        {
            _db = db;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            await _db.Categories.AddAsync(Category);
            _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
