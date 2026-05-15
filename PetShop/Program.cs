using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetShop.Data.Context;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
var connectionStringIdentity = builder.Configuration.GetConnectionString("IdentityConnection");

builder.Services.AddDbContext<PetShopDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), 
                                                           m => m.MigrationsAssembly("PetShop.Data")));

builder.Services.AddDbContext<IdentidadeDbContext>(options => options.UseMySql(connectionStringIdentity, ServerVersion.AutoDetect(connectionStringIdentity)));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentidadeDbContext>()
                .AddDefaultUI(); ;

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/");

    options.Conventions.AllowAnonymousToPage("/Home/Login");
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Home/Login";
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(12);

    options.Cookie.HttpOnly = true;

    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
