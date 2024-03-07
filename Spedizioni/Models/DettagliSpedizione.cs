using Spedizioni.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spedizioni.Models
{
    public class DettagliSpedizione
    {
        [Key]
        [Display(Name = "ID Dettagli Spedizione")]
        public int IdDettagliSpedizione { get; set; }
        [Display(Name = "ID Spedizione")]
        [ForeignKey("Spedizione")]
        public int IdSpedizione { get; set; }

        [Required]
        [Display(Name = "Stato Spedizione")]
        [StatoSpedizioneValidation]
        public string Stato { get; set; }

        [NotMapped]
        public virtual Spedizione Spedizione { get; set; }
        [NotMapped]
        public virtual Cliente Cliente { get; set; }

    }
}
