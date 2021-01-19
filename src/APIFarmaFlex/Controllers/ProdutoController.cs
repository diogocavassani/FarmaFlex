using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIFarmaFlex.Domain.Enum;
using APIFarmaFlex.Domain.Models;
using APIFarmaFlex.Infra.Repository;
using APIFarmaFlex.Infra.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIFarmaFlex.Controllers
{
    [Route("api/produtos")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoRepositorio _produtoRepositorio;
        private readonly UnityOfWork _unityOfWork;

        public ProdutoController(ProdutoRepositorio produtoRepositorio, UnityOfWork unityOfWork)
        {
            _produtoRepositorio = produtoRepositorio;
            _unityOfWork = unityOfWork;
        }
        [HttpGet]
        [Route("BuscarStatusAtivos")]
        public async Task<IEnumerable<Produto>> BuscarStatusAtivos()
        {
            return await _produtoRepositorio.PegarProdutosAtivos();
        }

        [HttpGet]
        [Route("BuscarTodos")]
        public async Task<IEnumerable<Produto>> BuscarTodos()
        {
            return await _produtoRepositorio.PegarProdutoComCategoria();
        }
        [HttpGet]
        [Route("BuscarPromocionais")]
        public async Task<IEnumerable<Produto>> BuscarPromocionais()
        {
            return await _produtoRepositorio.PegarPromocionais();
        }


        [HttpGet]
        [Route("BuscarPorId/{id}")]
        public async Task<IEnumerable<Produto>> BuscarPorId(int id)
        {
            return await _produtoRepositorio.PegarPorId(id);
        }


        [HttpGet]
        [Route("BuscarPorNome/{nome}")]
        public async Task<IEnumerable<Produto>> BuscarPorNome(string nome)
        {
            return await _produtoRepositorio.PegarProdutoPorNome(nome);
        }


        [HttpGet]
        [Route("BuscarPorCategoria/{id}")]
        public async Task<IEnumerable<Produto>> BuscarPorCategoria(int id)
        {

            return await _produtoRepositorio.PegarProdutoPorCategoria(id);

        }


        [HttpGet]
        [Route("BuscarPorStatus/{status}")]
        public async Task<IEnumerable<Produto>> BuscarPorStatus(StatusEnum status)
        {
            return await _produtoRepositorio.PegarProdutoPorStatus(status);
        }


        [HttpPost]
        [Route("Registrar")]
        public async Task<ActionResult<Produto>> Registrar([FromBody] Produto produto)
        {
            if (ModelState.IsValid)
            {

                await _produtoRepositorio.Inserir(produto);
                _unityOfWork.Commit();
                return produto;
            }
            else
                return BadRequest(ModelState);
        }


        [HttpPut]
        [Route("Atualizar/{id}")]
        public async Task<ActionResult<Produto>> Atualizar(int id, [FromBody] Produto produto)
        {
            if (id != produto.ProdutoId)
                return NotFound(new { message = "Produto não encontrada" });
            if (ModelState.IsValid)
            {
                await _produtoRepositorio.Atualizar(produto);
                _unityOfWork.Commit();
                return produto;
            }
            else
                return BadRequest(ModelState);

        }


        [HttpPut]
        [Route("AlterarStatus/{id}")]
        public async Task<ActionResult<Produto>> AlterarStatus([FromBody] Produto produto, int id)
        {
            if (id != produto.ProdutoId)
                return NotFound("não foi possivel atualizar produto");
            try
            {
                if (produto.StatusProduto == StatusEnum.Ativo)
                {
                    produto.StatusProduto = StatusEnum.Inativo;
                    await _produtoRepositorio.Atualizar(produto);
                    _unityOfWork.Commit();
                    return produto;
                }
                else if (produto.StatusProduto == StatusEnum.Inativo)
                {
                    produto.StatusProduto = StatusEnum.Ativo;
                    await _produtoRepositorio.Atualizar(produto);
                    _unityOfWork.Commit();
                    return produto;
                }
                else
                    return BadRequest("Não foi possivel alterar");

            }
            catch
            {
                return BadRequest("Não foi possivel alterar");
            }
        }
        [HttpGet]
        [Route("BuscarAtivos")]
        public async Task<IEnumerable<Produto>> BuscarAtivos()
        {
            return await _produtoRepositorio.PegarAtivos();
        }
        [HttpGet]
        [Route("BuscarInativos")]
        public async Task<IEnumerable<Produto>> BuscarInativos()
        {
            return await _produtoRepositorio.PegarInativos();
        }
    }
}
