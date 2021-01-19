using APIFarmaFlex.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIFarmaFlex.Aplication.ViewModels.PedidoViewModels
{
    public class ListarPedidoViewModel
    {
        public int PedidoId { get; set; }
        public string NomeCliente { get; set; }
        public string SobreNomeCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public StatusEnumPedido Status { get; set; }
        public DateTime DataPedido { get; set; }
        public float TotalPedido { get; set; }
    }
}
