using APIFarmaFlex.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIFarmaFlex.Aplication.ViewModels.PedidoViewModels
{
     public class DetalhesPedidoViewModel
    {
        public int ClienteId { get; set; }

        public string NomeCliente { get; set; }
        public string SobreNomeCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public List<int> ProdutoId { get; set; }
        public List<string> NomeProduto { get; set; }
        public List<string> DescricaoProduto { get; set; }
        public List<string> EAN { get; set; }
        public List<int> Quantidade { get; set; }
        public List<float> ValorUnitario { get; set; }
        public List<float> ValorPromocao { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Observacoes { get; set; }
        public int FormaPagamentoId { get; set; }
        public string FormaPagamento { get; set; }
        public float TotalPedido { get; set; }
        public int PedidoId { get; set; }
        public StatusEnumPedido StatusPedido { get; set; }

        public DateTime DataPedido { get; set; }
        public DateTime UltimaAlteracao { get; set; }
        public int UsuarioId { get; set;}
        public string Usuario { get; set; }
    }
}
