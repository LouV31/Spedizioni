using Spedizioni.Validations;
using System.ComponentModel.DataAnnotations;

namespace Spedizioni.Models
{
    public class Cliente
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Il campo Tipo Cliente non può essere lasciato Vuoto")]
        public string TipoCliente { get; set; }

        [CodiceFiscaleValidation]
        public string? CodiceFiscale { get; set; }
        [PartitaIvaValidation]
        public string? PartitaIva { get; set; }

        [Required]
        public string Nome { get; set; }
    }
}
