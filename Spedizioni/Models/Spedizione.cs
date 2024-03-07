using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spedizioni.Models
{
    public class Spedizione
    {
        [Key]
        public int IdSpedizione { get; set; }
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        [Required]
        [Display(Name = "Spedito")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DataSpedizione { get; set; }
        [Required]
        [Display(Name = "Peso/Kg")]
        public decimal peso { get; set; }
        [Required]
        public string Destinazione { get; set; }

        [Required]
        [Display(Name = "Nominativo Destinatario")]
        public string NominativoDestinatario { get; set; }
        [Required]
        public string Indirizzo { get; set; }
        [Required]
        [Display(Name = "Costo di Spedizione")]
        public string PrezzoSpedizione { get; set; }
        [Required]
        [Display(Name = "Consegna Prevista")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DataConsegna { get; set; }
        // Proprietà di navigazione
        // Serve a creare una relazione uno a molti tra cliente e spedizione

        [NotMapped]
        public virtual Cliente Cliente { get; set; }
        [NotMapped]
        public virtual ICollection<DettagliSpedizione> DettagliSpedizione { get; set; }


    }
}
