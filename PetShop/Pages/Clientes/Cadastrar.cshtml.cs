using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetShop.Data.Context;
using PetShop.Data.Models;

namespace PetShop.Pages.Clientes
{
    [Authorize]
    public class CadastrarModel : PageModel
    {
        private readonly PetShopDbContext _context;

        public CadastrarModel(PetShopDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cliente Cliente { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _context.Clientes.AddAsync(Cliente);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Clientes/Index");
        }
    }
}
