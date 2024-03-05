using Spedizioni.Models;
using System.ComponentModel.DataAnnotations;

namespace Spedizioni.Validations
{
    public class CodiceFiscaleValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Cliente cliente = (Cliente)validationContext.ObjectInstance;
            if (string.IsNullOrEmpty(cliente.TipoCliente))
            {
                return ValidationResult.Success;
            }
            if (cliente.TipoCliente.ToLower() == "privato" && string.IsNullOrEmpty(cliente.CodiceFiscale))
            {
                return new ValidationResult("Il campo Codice Fiscale non può essere lasciato vuoto");
            }
            return ValidationResult.Success;
        }
    }
}
