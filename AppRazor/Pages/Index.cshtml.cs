using AppRazor.Data;
using AppRazor.Data.Model;
//using AppRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppRazor.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Category> categories { get; set; }
        private BulkyBookContext _db;
        public IndexModel(BulkyBookContext db)
        {
            _db= db;
        }
        public void OnGet()
        {
            categories = _db.Categories;
        }
    }
}