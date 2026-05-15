using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public Enums.TipoAnimal TipoAnimal { get; set; }
        public bool TemAlergia { get; set; }
        public bool PodePerfume { get; set; }
        public bool PodeEnfeite { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
    }
}
