using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetShop.Data.Context;
using PetShop.Data.Models;

namespace PetShop.Pages.Clientes
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly PetShopDbContext _contexto;

        public IndexModel(PetShopDbContext contexto)
        {
            _contexto = contexto;
        }

        public IList<Cliente> Clientes { get; set; }

        public IActionResult OnGet()
        {
            Clientes = new List<Cliente>();

            Clientes = _contexto.Clientes.ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var cliente = await _contexto.Clientes.FindAsync(id);

            if (cliente != null)
            {
                _contexto.Clientes.Remove(cliente);

                await _contexto.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
