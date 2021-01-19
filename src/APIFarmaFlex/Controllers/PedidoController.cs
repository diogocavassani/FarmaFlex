using APIFarmaFlex.Aplication.ViewModels;
using APIFarmaFlex.Aplication.ViewModels.PedidoViewModels;
using APIFarmaFlex.Domain.Models;
using APIFarmaFlex.Infra.Repository;
using APIFarmaFlex.Infra.UOW;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIFarmaFlex.Controllers
{
    [Route("api/pedidos")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoRepositorio _pedidoRepositorio;
        private readonly UnityOfWork _unityOfWork;

        public PedidoController(PedidoRepositorio pedidoRepositorio, UnityOfWork unityOfWork)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _unityOfWork = unityOfWork;

        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IEnumerable<ListarPedidoViewModel>> Listar()
        {
            return await _pedidoRepositorio.PegarPedidoCompleto();
        }
        [HttpGet]
        [Route("ListarWeb")]
        public async Task<IEnumerable<Pedido>> ListarWeb()
        {
            return await _pedidoRepositorio.PegarListarPedidos();
        }

        [HttpGet]
        [Route("DetalhesPedidos/{id}")]
        public async Task<Pedido> DetalhesPedidos(int id)
        {
            return await _pedidoRepositorio.DetalhesPedidos(id);
        }


        [HttpGet]
        [Route("Detalhes/{id}")]
        public async Task<IEnumerable<DetalhesPedidoViewModel>> Detalhes(int id)
        {
            return await _pedidoRepositorio.PedidosDetalhes(id);
        }
        [HttpGet]
        [Route("BuscarporCliente/{id}")]
        public async Task<IEnumerable<ListarPedidoViewModel>> BuscarPorCliente(int id)
        {
            return await _pedidoRepositorio.PedidosPorCliente(id);
        }


        [HttpGet]
        [Route("ListarPorStatus/{status}")]
        public async Task<IEnumerable<Pedido>> ListarPorStatus(StatusEnumPedido status)
        {
            return await _pedidoRepositorio.ListarPorStatus(status);
        }


        [HttpPost]
        [Route("Registrar")]
        public async Task<ActionResult<ResultViewModel>> Registrar([FromBody] PedidoViewModel pedidoViewModel)
        {
            if (!ModelState.IsValid)
                return new ResultViewModel
                {
                    Sucesso = false,
                    Mensagem = "Erro ao cadastrar pedido",
                    Objeto = ModelState
                };
            else
            {
                await _pedidoRepositorio.InserirPedido(pedidoViewModel);
                _unityOfWork.Commit();
                return new ResultViewModel
                {
                    Sucesso = true,
                    Mensagem = "Sucesso ao cadastrar pedido",
                    Objeto = null
                };
            }
        }


        [HttpPut]
        [Route("Atualizar/{id}")]
        public async Task<ActionResult<ResultViewModel>> Atualizar([FromBody] PedidoViewModel pedidoViewModel, int id)
        {
            if (id != pedidoViewModel.PedidoId)
                return new ResultViewModel
                {
                    Sucesso = false,
                    Mensagem = "Erro ao alterar pedido",
                    Objeto = ModelState
                };
            if (!ModelState.IsValid)
                return new ResultViewModel
                {
                    Sucesso = false,
                    Mensagem = "Erro ao alterar pedido",
                    Objeto = ModelState
                };
            else
            {
                await _pedidoRepositorio.AlterarPedido(pedidoViewModel);
                return new ResultViewModel
                {
                    Sucesso = true,
                    Mensagem = "Sucesso ao atualizar pedido",
                    Objeto = null
                };
            }
        }

        [HttpPut]
        [Route("AlterarStatus/{id}/{status}")]
        public async Task<ActionResult<ResultViewModel>> AlterarStatus(int id,int status)
        {
            await _pedidoRepositorio.AlterarStatusPedido(id, status);
            _unityOfWork.Commit();
            return new ResultViewModel
            {
                Sucesso = true,
                Mensagem = "Sucesso ao alterar status",
                Objeto = null,
            };
        }
    }
}
