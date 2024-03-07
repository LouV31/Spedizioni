using Spedizioni.Models;
using System.ComponentModel.DataAnnotations;

namespace Spedizioni.Validations
{
    public class StatoSpedizioneValidation : ValidationAttribute
    {
        // la proprietà stato di DettagliSpedizione può assumere solo i seguenti valori: "In Spedizione", "Spedito", "Consegnato"

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DettagliSpedizione dettagliSpedizione = (DettagliSpedizione)validationContext.ObjectInstance;
            if (string.IsNullOrEmpty(dettagliSpedizione.Stato))
            {
                return new ValidationResult("Il campo Stato Spedizione non può essere lasciato vuoto");
            }
            if (dettagliSpedizione.Stato.ToLower() != "in spedizione" && dettagliSpedizione.Stato.ToLower() != "spedito" && dettagliSpedizione.Stato.ToLower() != "consegnato")
            {
                return new ValidationResult("Il campo Stato Spedizione può assumere solo i seguenti valori: In Spedizione, Spedito, Consegnato");
            }
            return ValidationResult.Success;
        }
    }



}
