using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetShop.Data.Context;
using PetShop.Data.Models;

namespace PetShop.Pages.Animais
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
        public Animal Animal { get; set; }
        public string? NomeCliente { get; set; }
        [BindProperty]
        public int IdCliente { get; set; }

        public async Task<IActionResult> OnGetAsync(int idCliente)
        {
            var cliente = await _context.Clientes.FindAsync(idCliente);

            NomeCliente = cliente.Nome;
            IdCliente = cliente.Id;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Animal.IdCliente = IdCliente;

            await _context.Animais.AddAsync(Animal);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Animais/Index", new {IdCliente});
        }
    }
}
