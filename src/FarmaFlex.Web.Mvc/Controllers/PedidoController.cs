using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIFarmaFlex.Domain.Models;
using FarmaFlex.Web.Mvc.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmaFlex.Web.Mvc.Controllers
{
    public class PedidoController : Controller
    {
        private readonly PedidoRepository _pedidoRepository;
        private readonly ProdutoRepository _produtoRepository;
        public PedidoController(PedidoRepository pedidoRepository, ProdutoRepository produtoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<IActionResult> Index(StatusEnumPedido? status)
        {
            IEnumerable<Pedido> pedidos;
            IEnumerable<Pedido> pedidosOrdenados;
            switch (status)
            {
                case StatusEnumPedido.Pendente:
                    pedidos = await _pedidoRepository.ObterPedidosPorStatus((int)StatusEnumPedido.Pendente);
                    pedidosOrdenados = pedidos.OrderByDescending(pedidos => pedidos.DataPedido);
                    return View(pedidosOrdenados);

                case StatusEnumPedido.Iniciado:
                    pedidos = await _pedidoRepository.ObterPedidosPorStatus((int)StatusEnumPedido.Iniciado);
                    pedidosOrdenados = pedidos.OrderByDescending(pedidos => pedidos.DataPedido);
                    return View(pedidosOrdenados);

                case StatusEnumPedido.EmEntrega:
                    pedidos = await _pedidoRepository.ObterPedidosPorStatus((int)StatusEnumPedido.EmEntrega);
                    pedidosOrdenados = pedidos.OrderByDescending(pedidos => pedidos.DataPedido);
                    return View(pedidosOrdenados);
                case StatusEnumPedido.Finalizado:
                    pedidos = await _pedidoRepository.ObterPedidosPorStatus((int)StatusEnumPedido.Finalizado);
                    pedidosOrdenados = pedidos.OrderByDescending(pedidos => pedidos.DataPedido);
                    return View(pedidosOrdenados);
                case StatusEnumPedido.Cancelado:
                    pedidos = await _pedidoRepository.ObterPedidosPorStatus((int)StatusEnumPedido.Cancelado);
                    pedidosOrdenados = pedidos.OrderByDescending(pedidos => pedidos.DataPedido);
                    return View(pedidosOrdenados);
                default:
                    pedidos = await _pedidoRepository.ObterPedidos();
                    pedidosOrdenados = pedidos.OrderByDescending(pedidos => pedidos.DataPedido);
                    return View(pedidosOrdenados);

            }
        }

        // GET: PedidoController1/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var pedido = await _pedidoRepository.ObterPorId(id);
            foreach (var item in pedido.ItensPedidos)
            {
                item.Produto = await _produtoRepository.ObterProdutosPorId(item.ProdutoId);
            }

            return View(pedido);
        }

        public async Task<IActionResult> AlterarStatus(int id)
        {
            var pedido = await _pedidoRepository.ObterPorId(id);
            switch (pedido.StatusPedido)
            {
                case StatusEnumPedido.Pendente:
                    await _pedidoRepository.AlterarStatus(pedido, (int)StatusEnumPedido.Iniciado);
                    return RedirectToAction(nameof(Index));

                case StatusEnumPedido.Iniciado:
                    await _pedidoRepository.AlterarStatus(pedido, (int)StatusEnumPedido.EmEntrega);
                    return RedirectToAction(nameof(Index));

                case StatusEnumPedido.EmEntrega:
                    await _pedidoRepository.AlterarStatus(pedido, (int)StatusEnumPedido.Finalizado);
                    return RedirectToAction(nameof(Index));
                default:
                    return RedirectToAction(nameof(Index));
            }
        }
        public async Task<IActionResult> Cancelar(int id)
        {
            var pedido = await _pedidoRepository.ObterPorId(id);
            await _pedidoRepository.AlterarStatus(pedido, (int)StatusEnumPedido.Cancelado);
            return RedirectToAction(nameof(Index));

        }

    }
}
