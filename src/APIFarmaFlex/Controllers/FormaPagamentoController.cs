using APIFarmaFlex.Domain.Enum;
using APIFarmaFlex.Domain.Models;
using APIFarmaFlex.Infra.Repository;
using APIFarmaFlex.Infra.UOW;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIFarmaFlex.Controllers
{
    [Route("api/formapagamento")]
    [ApiController]
    public class FormaPagamentoController : ControllerBase
    {
        private readonly FormaPagamentoRepositorio _formaPagamentoRepositorio;
        private readonly UnityOfWork _unityOfWork;

        public FormaPagamentoController(FormaPagamentoRepositorio formaPagamentoRepositorio, UnityOfWork unityOfWork)
        {
            _formaPagamentoRepositorio = formaPagamentoRepositorio;
            _unityOfWork = unityOfWork;

        }
        
        [HttpGet]
        [Route("BuscarStatusAtivos")]
        public async Task<IEnumerable<FormaPagamento>> BuscarStatusAtivos()
        {
            return await _formaPagamentoRepositorio.PegarFormaPagamentosAtivas();
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IEnumerable<FormaPagamento>> Buscar()
        {
            return await _formaPagamentoRepositorio.PegarTodos();
        }

        [HttpGet]
        [Route("BuscarPorId/{id}")]
        public async Task<FormaPagamento> BuscarPorId(int id)
        {
            return await _formaPagamentoRepositorio.PegarPeloId(id);
        }
        [HttpGet]
        [Route("BuscarPorNome/{nome}")]
        public async Task<IEnumerable<FormaPagamento>> BuscarPorNome(string nome)
        {
            return await _formaPagamentoRepositorio.PegarPeloNome(nome);
        }
        [HttpGet]
        [Route("BuscarPorSatus/{status}")]
        public async Task<IEnumerable<FormaPagamento>> BuscarPorSatus(StatusEnum status)
        {
            return await _formaPagamentoRepositorio.PegarPeloSatus(status);
        }
        [HttpPost]
        [Route("Registrar")]
        public async Task<ActionResult<FormaPagamento>> Registrar([FromBody] FormaPagamento formaPagamento)
        {
            if (ModelState.IsValid)
            {
                await _formaPagamentoRepositorio.Inserir(formaPagamento);
                _unityOfWork.Commit();
                return formaPagamento;
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("Atualizar/{id}")]
        public async Task<ActionResult<FormaPagamento>> Atualizar(int id, [FromBody] FormaPagamento formaPagamento)
        {
            if (id != formaPagamento.FormaPagamentoId)
                return NotFound(new { message = "Não foi possivel localizar" });
            if (ModelState.IsValid)
            {
                await _formaPagamentoRepositorio.Atualizar(formaPagamento);
                _unityOfWork.Commit();
                return formaPagamento;
            }
            else
                return BadRequest(ModelState);

        }
        [HttpPut]
        [Route("AlterarStatus/{id:int}")]
        public async Task<ActionResult<FormaPagamento>> AlterarStatus([FromBody] FormaPagamento formaPagamento, int id)
        {
            if (id != formaPagamento.FormaPagamentoId)
                return NotFound("não foi possivel atualizar cliente");
            try
            {
                if (formaPagamento.StatusForma == StatusEnum.Ativo)
                {
                    formaPagamento.StatusForma = StatusEnum.Inativo;
                    await _formaPagamentoRepositorio.Atualizar(formaPagamento);
                    _unityOfWork.Commit();
                    return formaPagamento;
                }
                else if (formaPagamento.StatusForma == StatusEnum.Inativo)
                {
                    formaPagamento.StatusForma = StatusEnum.Ativo;
                    await _formaPagamentoRepositorio.Atualizar(formaPagamento);
                    _unityOfWork.Commit();
                    return formaPagamento;
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
        public async Task<IEnumerable<FormaPagamento>> BuscarAtivos()
        {
            return await _formaPagamentoRepositorio.PegarAtivos();
        }
        [HttpGet]
        [Route("BuscarInativos")]
        public async Task<IEnumerable<FormaPagamento>> BuscarInativos()
        {
            return await _formaPagamentoRepositorio.PegarInativos();
        }
        

    }
}
