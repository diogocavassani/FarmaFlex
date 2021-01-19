using APIFarmaFlex.Domain.Models;
using FarmaFlex.Web.Mvc.Extentions.ViewComponents.Helpers;
using FarmaFlex.Web.Mvc.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaFlex.Web.Mvc.Extentions.ViewComponents.StatusPedido
{

    public class CanceladoViewComponents : ViewComponent
    {
        private readonly PedidoRepository _pedidoRepository;
        public CanceladoViewComponents(PedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var total = await _pedidoRepository.ObterPedidosPorStatus((int)StatusEnumPedido.Cancelado);

            var contador = new ContadorStatusPedido()
            {
                TotalStatus = total.Count(),
                ClassContent = "small-box bg-secondary",
                LinkDetalhes = "",
                Status = StatusEnumPedido.Cancelado

            };

            return View(contador);
       
        }
    }
}
