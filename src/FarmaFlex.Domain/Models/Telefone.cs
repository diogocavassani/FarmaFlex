using System.ComponentModel.DataAnnotations;

namespace APIFarmaFlex.Domain.Models
{
    public class Telefone
    {
        public int TelefoneId { get; set; }
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string NumeroTelefone { get; set; }
    }
}
