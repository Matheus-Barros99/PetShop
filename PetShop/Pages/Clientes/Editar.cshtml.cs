using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetShop.Data.Context;
using PetShop.Data.Models;

namespace PetShop.Pages.Clientes
{
    [Authorize]
    public class EditarModel : PageModel
    {
        private readonly PetShopDbContext _contexto;

        public EditarModel(PetShopDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Cliente = await _contexto.Clientes.FindAsync(id);

            if (Cliente == null)
            {
                return RedirectToPage("/Clientes/Index");
            }

            return Page();
        }

        [BindProperty]
        public Cliente Cliente { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            _contexto.Attach(Cliente).State = EntityState.Modified;

            await _contexto.SaveChangesAsync();

            return RedirectToPage("/Clientes/Index");
        }
    }
}
