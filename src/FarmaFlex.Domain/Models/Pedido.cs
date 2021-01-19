using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIFarmaFlex.Domain.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        public int FormapagamentoId { get; set; }
        public int UsuarioId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual FormaPagamento FormaPagamento { get; set; }
        public virtual Usuario Usuario { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 5 e 60 caracteres")]
        [MinLength(5, ErrorMessage = "Este campo deve conter entre 5 e 60 caracteres")]
        public string Observacao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime DataPedido { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM HH:mm}")]
        public DateTime DataUltimaAlteracao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(1, 99999, ErrorMessage = "O total deve estar entre 1 e 99999")]
        public float Total { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public StatusEnumPedido StatusPedido { get; set; }

        public virtual ICollection<ItensPedido> ItensPedidos { get; set; }

    }
}
