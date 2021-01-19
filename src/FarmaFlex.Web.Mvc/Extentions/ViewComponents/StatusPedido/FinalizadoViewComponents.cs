using APIFarmaFlex.Domain.Models;
using FarmaFlex.Web.Mvc.Extentions.ViewComponents.Helpers;
using FarmaFlex.Web.Mvc.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaFlex.Web.Mvc.Extentions.ViewComponents.StatusPedido
{
    public class FinalizadoViewComponents : ViewComponent
    {
        private readonly PedidoRepository _pedidoRepository;
        public FinalizadoViewComponents(PedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var total = await _pedidoRepository.ObterPedidosPorStatus((int)StatusEnumPedido.Finalizado);

            var contador = new ContadorStatusPedido()
            {
                TotalStatus = total.Count(),
                ClassContent = "small-box bg-success",
                LinkDetalhes = "",
                Status = StatusEnumPedido.Finalizado

            };

            return View(contador);

        }
    }
}
