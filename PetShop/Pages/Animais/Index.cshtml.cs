using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetShop.Data.Context;
using PetShop.Data.Models;

namespace PetShop.Pages.Animais
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly PetShopDbContext _contexto;

        public IndexModel(PetShopDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IActionResult> OnGetAsync(int idCliente)
        {
            Animais = new List<Animal>();

            IdCliente = idCliente;

            Animais = await _contexto.Animais.Where(a => a.IdCliente == idCliente).ToListAsync();

            NomeCliente = await _contexto.Clientes
                                         .Where(c => c.Id == idCliente)
                                         .Select(c => c.Nome)
                                         .FirstAsync();

            return Page();
        }

        public IList<Animal> Animais { get; set; }

        public string? NomeCliente { get; set; }
        public int IdCliente { get; set; }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var animal = await _contexto.Animais.FindAsync(id);

            var idCliente = animal.IdCliente;

            if (animal != null)
            {
                _contexto.Animais.Remove(animal);

                await _contexto.SaveChangesAsync();
            }

            return RedirectToPage("/Animais/Index", new { idCliente });
        }
    }
}
