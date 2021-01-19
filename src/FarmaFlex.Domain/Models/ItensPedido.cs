using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace APIFarmaFlex.Domain.Models
{
    public class ItensPedido
    {

        public int ItensPedidoId { get; set; }
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }     
        public int PedidoId { get; set; }       
        public virtual Pedido Pedido { get; set; }
        public int QuantidadeProduto { get; set; }
    }
}
