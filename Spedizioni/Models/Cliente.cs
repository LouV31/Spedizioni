using System.ComponentModel.DataAnnotations;

namespace Spedizioni.Models
{
    public class Cliente
    {
        public enum Tipo { Privato, Azienda }
        [Required]
        public int Id { get; set; }
        public string Identificativo { get; set; }
        [Required]
        public string Nome { get; set; }
    }
}
