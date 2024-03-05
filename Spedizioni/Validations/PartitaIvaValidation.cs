using Spedizioni.Models;
using System.ComponentModel.DataAnnotations;

namespace Spedizioni.Validations
{
    public class PartitaIvaValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Cliente cliente = (Cliente)validationContext.ObjectInstance;
            if (string.IsNullOrEmpty(cliente.TipoCliente))
            {
                return ValidationResult.Success;
            }
            if (cliente.TipoCliente.ToLower() == "azienda" && string.IsNullOrEmpty(cliente.PartitaIva))
            {
                return new ValidationResult("Il campo Partita Iva non può essere lasciato vuoto");
            }
            return ValidationResult.Success;
        }
    }
}
