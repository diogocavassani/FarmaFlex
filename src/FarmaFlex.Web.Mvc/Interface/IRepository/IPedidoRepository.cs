using APIFarmaFlex.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaFlex.Web.Mvc.Interface.IRepository
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> ObterPedidos();
        Task<IEnumerable<Pedido>> ObterPedidosPorStatus(int status);
        Task<Pedido> AlterarPedido(Pedido pedido);
    }
}
