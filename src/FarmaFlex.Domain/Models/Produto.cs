using APIFarmaFlex.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIFarmaFlex.Domain.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(30, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        [MinLength(5, ErrorMessage = "Este campo deve conter entre 5 e 30 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(60, ErrorMessage = ("Este campo deve conter entre 5 e 60 caracteres"))]
        [MinLength(5, ErrorMessage = "Este campo deve conter entre 5 e 60 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(13, ErrorMessage = ("Este campo deve conter 13 caracteres"))]       
        public string EAN { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public float Preco { get; set; }
        public float PrecoPromocional { get; set; }
        public string Foto { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public StatusEnum StatusProduto { get; set; }

        public virtual ICollection<ItensPedido> ItensPedidos { get; set; }
        public bool Ativo { get; set; }
    }
}
