using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Web_MES.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRUD_Web_MES.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public RegisterModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public u10000 user { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost(u10000 user)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            _db.u10000.Add(user);
            await _db.SaveChangesAsync();
            return RedirectToPage("Login");
        }
    }
}