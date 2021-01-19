using APIFarmaFlex.Aplication.Services;
using APIFarmaFlex.Aplication.ViewModels.Cliente;
using APIFarmaFlex.Domain.Enum;
using APIFarmaFlex.Domain.Models;
using APIFarmaFlex.Infra.Repository;
using APIFarmaFlex.Infra.UOW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIFarmaFlex.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteRepositorio _clienteRepositorio;
        private readonly UnityOfWork _unityOfWork;

        public ClienteController(ClienteRepositorio clienteRepositorio, UnityOfWork unityOfWork)
        {
            _clienteRepositorio = clienteRepositorio;
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        [Route("BuscarTodos")]

        public async Task<IEnumerable<Cliente>> BuscarTodos()
        {
            return await _clienteRepositorio.PegarClienteCompleto();
        }


        [HttpGet]
        [Route("cliente/{id}")]

        public async Task<Cliente> BuscarporId(int id)
        {
            return await _clienteRepositorio.PegarPeloId(id);
        }


        [HttpGet]
        [Route("BuscarPorNome/{nome}")]

        public async Task<IEnumerable<Cliente>> BuscarPorNome(string nome)
        {
            return await _clienteRepositorio.PegarPeloNome(nome);
        }


        [HttpGet]
        [Route("BuscarPorstatus/{status}")]
        [Authorize(Roles = "Usuario")]
        public async Task<IEnumerable<Cliente>> BuscarPorstatus(StatusEnum status)
        {
            return await _clienteRepositorio.PegarPeloStatus(status);
        }


        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] ClienteLoginViewModel cliente)
        {
            //var user = _clienteRepositorio.LogarCliente(cliente.Email, cliente.Senha);
            //if (user == null)
            //    return NotFound(new { message = "usuario não encontrado" });
            //var token = TokenService.GenerateTokenCliente(user);
            //user.Senha = "";
            //return new
            //{
            //    user = user,
            //    token = token
            //};
            return _clienteRepositorio.LogarCliente(cliente.Email, cliente.Senha);
        }


        [HttpPost]
        [Route("Registrar")]
        public async Task<ActionResult<Cliente>> Registrar([FromBody] Cliente cliente)
        {

            if (ModelState.IsValid)
            {
                await _clienteRepositorio.Inserir(cliente);
                _unityOfWork.Commit();
                return cliente;
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("Atualizar/{id}")]
        public async Task<ActionResult<Cliente>> Atualizar(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.ClienteId)
                return NotFound(new { message = "Cliente não encontrado" });
            if (ModelState.IsValid)
            {
                await _clienteRepositorio.Atualizar(cliente);
                _unityOfWork.Commit();
                return cliente;
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("status/{id}")]
        public async Task<ActionResult<Cliente>> AlterarStatus([FromBody] Cliente cliente, int id)
        {
            if (id != cliente.ClienteId)
                return NotFound("Não foi possivel alterar");
            try
            {
                if (cliente.Status == StatusEnum.Ativo)
                {
                    cliente.Status = StatusEnum.Inativo;
                    await _clienteRepositorio.Atualizar(cliente);
                    _unityOfWork.Commit();
                    return cliente;
                }
                else
                {
                    cliente.Status = StatusEnum.Ativo;
                    await _clienteRepositorio.Atualizar(cliente);
                    _unityOfWork.Commit();
                    return cliente;
                }
            }
            catch
            {
                return BadRequest("Não foi possivel alterar");
            }
        }
    }
}
