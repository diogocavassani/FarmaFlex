using APIFarmaFlex.Domain.Models;
using FarmaFlex.Web.Mvc.Extentions.ViewComponents.Helpers;
using FarmaFlex.Web.Mvc.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaFlex.Web.Mvc.Extentions.ViewComponents.StatusPedido
{
    public class PendenteViewComponents : ViewComponent
    {
        private readonly PedidoRepository _pedidoRepository;
        public PendenteViewComponents(PedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var total = await _pedidoRepository.ObterPedidosPorStatus((int)StatusEnumPedido.Pendente);

            var contador = new ContadorStatusPedido()
            {
                TotalStatus = total.Count(),
                ClassContent = "small-box bg-danger",
                LinkDetalhes = "",
                Status = StatusEnumPedido.Pendente

            };

            return View(contador);

        }
    }
}
