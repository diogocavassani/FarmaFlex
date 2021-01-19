using APIFarmaFlex.Domain.Models;
using FarmaFlex.Web.Mvc.Extentions.ViewComponents.Helpers;
using FarmaFlex.Web.Mvc.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaFlex.Web.Mvc.Extentions.ViewComponents.StatusPedido
{
    public class IniciadoViewComponents : ViewComponent
    {
        private readonly PedidoRepository _pedidoRepository;
        public IniciadoViewComponents(PedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var total = await _pedidoRepository.ObterPedidosPorStatus((int)StatusEnumPedido.Iniciado);

            var contador = new ContadorStatusPedido()
            {
                TotalStatus = total.Count(),
                ClassContent = "small-box bg-info",
                LinkDetalhes = "",
                Status = StatusEnumPedido.Iniciado

            };

            return View(contador);

        }
    }
}
