using Microsoft.EntityFrameworkCore;
using PetShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Context
{
    public class PetShopDbContext : DbContext
    {
        public PetShopDbContext(DbContextOptions<PetShopDbContext> options) : base(options)
        {
            
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Animal> Animais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(animal =>
            {
                animal.HasIndex(a => a.IdCliente)
                      .HasDatabaseName("AnimalClienteFKIndex");

                animal.HasOne(a => a.Cliente)
                      .WithMany(c => c.Animais)
                      .HasForeignKey(a => a.IdCliente)
                      .HasConstraintName("AnimalClienteFKCnstraint");

                animal.Property(a => a.TipoAnimal)
                      .HasConversion(
                            v => v.ToString(),
                            v => (Enums.TipoAnimal)
                            Enum.Parse(typeof(Enums.TipoAnimal), v));
            });
        }
    }
}
