using APIFarmaFlex.Aplication.Services;
using APIFarmaFlex.Aplication.ViewModels.UsuarioViewModel;
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
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;
        private readonly UnityOfWork _unitOfWork;

        public UsuarioController(UsuarioRepositorio usuarioRepositorio, UnityOfWork unityOfWork)
        {
            _usuarioRepositorio = usuarioRepositorio;

            _unitOfWork = unityOfWork;
        }

        [HttpGet]
        [Route("BuscarTodos")]
        public async Task<IEnumerable<Usuario>> BuscarTodos()
        {
            return await _usuarioRepositorio.PegarTodos();
        }
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] UsuarioLoginViewModel usuario)
        {
            //TODO: Verificar a necessidade de metodo assincrono
            var user = _usuarioRepositorio.LogarUsuario(usuario.Email, usuario.Senha);
            if (user == null)
                return NotFound(new { message = "usuario não encontrado" });
            var token = TokenService.GenerateTokenUsuario(user);
            user.Senha = "";
            return new
            {
                user = user,
                Email = user.Email,
                token = token
            };
        }


        [HttpGet]
        [Route("BucarPorId/{id}")]
        public async Task<Usuario> BucarPorId(int id)
        {
            return await _usuarioRepositorio.PegarPeloId(id);
        }

        [HttpGet]
        [Route("BuscarPorNome/{nome}")]
        public async Task<IEnumerable<Usuario>> BuscarPorNome(string nome)
        {
            return await _usuarioRepositorio.PegarPeloNome(nome);
        }
        [HttpGet]
        [Route("BuscarPorStatus/{status}")]
        public async Task<IEnumerable<Usuario>> BuscarPorStatus(StatusEnum status)
        {
            return await _usuarioRepositorio.PegarPeloStatus(status);
        }

        [HttpPost]
        [Route("Registrar")]
        public async Task<ActionResult<Usuario>> Registrar([FromBody] Usuario usuario)
        {
            if (ModelState.IsValid)
            {

                await _usuarioRepositorio.Inserir(usuario);
                _unitOfWork.Commit();
                return usuario;
            }
            else
                return BadRequest(ModelState);
        }
        [HttpPut]
        [Route("Atualizar/{id}")]
        public async Task<ActionResult<Usuario>> Atualizar(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.UsuarioId)
                return NotFound(new { message = "Usuario não encontrado" });
            if (ModelState.IsValid)
            {
                await _usuarioRepositorio.Atualizar(usuario);
                _unitOfWork.Commit();
                return usuario;
            }
            else
                return BadRequest(ModelState);

        }
        [HttpPut]
        [Route("status/{id}")]
        public async Task<ActionResult<Usuario>> AlterarStatus([FromBody] Usuario usuario, int id)
        {
            if (id != usuario.UsuarioId)
                return NotFound("não foi possivel atualizar usuario");
            try
            {
                if (usuario.StatusUsuario == StatusEnum.Ativo)
                {
                    usuario.StatusUsuario = StatusEnum.Inativo;
                    await _usuarioRepositorio.Atualizar(usuario);
                    _unitOfWork.Commit();
                    return usuario;
                }
                else if (usuario.StatusUsuario == StatusEnum.Inativo)
                {
                    usuario.StatusUsuario = StatusEnum.Ativo;
                    await _usuarioRepositorio.Atualizar(usuario);
                    _unitOfWork.Commit();
                    return usuario;
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
        public async Task<IEnumerable<Usuario>> BuscarAtivos()
        {
            return await _usuarioRepositorio.PegarAtivos();
        }
        [HttpGet]
        [Route("BuscarInativos")]
        public async Task<IEnumerable<Usuario>> BuscarInativos()
        {
            return await _usuarioRepositorio.PegarInativos();
        }
    }
}

