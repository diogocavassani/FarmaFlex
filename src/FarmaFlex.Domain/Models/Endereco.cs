using System.ComponentModel.DataAnnotations;

namespace APIFarmaFlex.Domain.Models
{
    public class Endereco
    {
        public int EnderecoId { get; set; }

        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(30, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        [MinLength(5, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(10, ErrorMessage = "Este campo deve conter entre 1 e 10 caracteres")]
        [MinLength(1, ErrorMessage = "Este campo deve conter entre 1 e 10 caracteres")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(30, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        [MinLength(5, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(30, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        [MinLength(5, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(30, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        [MinLength(5, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        public string Cidade { get; set; }
    }
}
