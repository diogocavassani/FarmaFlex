using APIFarmaFlex.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIFarmaFlex.Domain.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(15, ErrorMessage = "Este campo deve conter entre 5 e 15 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 15 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(30, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        [MinLength(5, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        public string SobreNome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        //[Range(13, 13, ErrorMessage = "Este campo deve conter 13 caracteres")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public StatusEnum Status { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(30, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        [MinLength(5, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(30, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        [MinLength(5, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public FuncaoEnum Funcao { get; set; } 
        public virtual Endereco Endereco { get; set; }
        public virtual Telefone Telefone { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
        public bool Ativo { get; set; }
    }
}
