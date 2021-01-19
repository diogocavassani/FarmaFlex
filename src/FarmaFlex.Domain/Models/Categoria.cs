using APIFarmaFlex.Domain.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIFarmaFlex.Domain.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public StatusEnum StatusCategoria { get; set; }

        public bool Ativo { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
