using APIFarmaFlex.Domain.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIFarmaFlex.Domain.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(30, ErrorMessage = ("Este campo deve conter entre 5 e 30 caracteres"))]
        [MinLength(5, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(30, ErrorMessage = ("Este campo deve conter entre 5 e 30 caracteres"))]
        [MinLength(5, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(30, ErrorMessage = ("Este campo deve conter entre 5 e 30 caracteres"))]
        [MinLength(5, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public FuncaoEnum Funcao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public StatusEnum StatusUsuario { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
        public bool Ativo { get; set; }
    }
}
