using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PetShop.Pages.Home
{
    [Authorize]
    public class CadastroUsuarioModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public CadastroUsuarioModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Email { get; set; }

            public string Password { get; set; }

            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Input.Password != Input.ConfirmPassword)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "As senhas não coincidem");

                return Page();
            }

            var user = new IdentityUser
            {
                UserName = Input.Email,
                Email = Input.Email
            };

            var result =
                await _userManager.CreateAsync(
                    user,
                    Input.Password);

            if (result.Succeeded)
            {
                return RedirectToPage("/Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(
                    string.Empty,
                    error.Description);
            }

            return Page();
        }
    }
}
