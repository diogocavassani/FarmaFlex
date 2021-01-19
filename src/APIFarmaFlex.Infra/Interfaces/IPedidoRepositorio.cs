using APIFarmaFlex.Aplication.ViewModels.PedidoViewModels;
using APIFarmaFlex.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIFarmaFlex.Infra.Interfaces
{
    public interface IPedidoRepositorio : IRepositorioGenerico<Pedido>
    {
        Task<IEnumerable<ListarPedidoViewModel>> PegarPedidoCompleto();
        Task<IEnumerable<DetalhesPedidoViewModel>> PedidosDetalhes(int id);

        Task<IEnumerable<Pedido>> ListarPorStatus(StatusEnumPedido status);

        Task InserirPedido(PedidoViewModel pedidoViewModel);
        Task AlterarPedido(PedidoViewModel pedidoViewModel);
       
    }
}
