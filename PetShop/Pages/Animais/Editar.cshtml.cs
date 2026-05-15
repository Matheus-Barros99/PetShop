using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetShop.Data.Context;
using PetShop.Data.Models;

namespace PetShop.Pages.Animais
{
    public class EditarModel : PageModel
    {
        private readonly PetShopDbContext _contexto;

        public EditarModel(PetShopDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Animal = await _contexto.Animais.FindAsync(id);

            if (Animal == null)
            {
                return RedirectToPage("/Clientes/Index");
            }

            IdCliente = Animal.IdCliente;

            return Page();
        }

        [BindProperty]
        public Animal Animal { get; set; }
        [BindProperty]
        public int IdCliente { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            _contexto.Attach(Animal).State = EntityState.Modified;

            await _contexto.SaveChangesAsync();

            return RedirectToPage("/Animais/Index", new { IdCliente });
        }
    }
}
