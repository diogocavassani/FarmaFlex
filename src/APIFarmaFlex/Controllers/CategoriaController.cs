using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using APIFarmaFlex.Domain.Enum;
using APIFarmaFlex.Domain.Models;
using APIFarmaFlex.Infra.Repository;
using APIFarmaFlex.Infra.UOW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace APIFarmaFlex.Controllers
{
    [Route("api/categorias")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaRepositorio _categoriaRepositorio;
        private readonly UnityOfWork _unityOfWork;

        public CategoriaController(CategoriaRepositorio categoriaRepositorio, UnityOfWork unityOfWork)
        {
            _categoriaRepositorio = categoriaRepositorio;
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IEnumerable<Categoria>> Buscar()
        {
            return await _categoriaRepositorio.PegarTodos();
        }
        [HttpGet]
        [Route("BuscarStatusAtivas")]
        public async Task<IEnumerable<Categoria>> BuscarStatusAtivas()
        {
            return await _categoriaRepositorio.PegarCategoriasAtivas();
        }

        [HttpGet]
        [Route("BuscarAtivos")]
        public async Task<IEnumerable<Categoria>> BuscarAtivos()
        {
            return await _categoriaRepositorio.PegarAtivos();
        }
        [HttpGet]
        [Route("BuscarInativos")]
        public async Task<IEnumerable<Categoria>> BuscarInativos()
        {
            return await _categoriaRepositorio.PegarInativos();
        }


        [HttpGet]
        [Route("BuscarPorId/{id}")]
        public async Task<Categoria> BuscarPorId(int id)
        {
            return await _categoriaRepositorio.PegarPeloId(id);
        }


        [HttpGet]
        [Route("BuscarPorNome/{nome}")]
        public async Task<IEnumerable<Categoria>> BuscarPorNome(string nome)
        {
            return await _categoriaRepositorio.PegarPeloNome(nome);
        }


        [HttpGet]
        [Route("BuscarPorStatus/{status}")]
        public async Task<IEnumerable<Categoria>> BuscarPorStatus(StatusEnum status)
        {
            return await _categoriaRepositorio.PegarPeloStatus(status);
        }


        [HttpPost]
        [Route("Registrar")]
        public async Task<ActionResult<Categoria>> Registrar([FromBody] Categoria categoria)
        {
            if (ModelState.IsValid)
            {

                await _categoriaRepositorio.Inserir(categoria);
                _unityOfWork.Commit();
                return categoria;
            }
            else
                return BadRequest(ModelState);
        }


        [HttpPut]
        [Route("Atualizar/{id}")]
        public async Task<ActionResult<Categoria>> Atualizar(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.CategoriaId)
                return NotFound(new { message = "Categoria não encontrada" });
            if (ModelState.IsValid)
            {
                await _categoriaRepositorio.Atualizar(categoria);
                _unityOfWork.Commit();
                return categoria;
            }
            else
                return BadRequest(ModelState);

        }

        //[HttpPut]
        //[Route("Inativar/{id}")]
        //public async Task<ActionResult<Categoria>> Inativar(int id, [FromBody] Categoria categoria)
        //{
        //    if (id != categoria.CategoriaId)
        //        return NotFound(new { message = "Categoria não encontrada" });
        //    if (ModelState.IsValid)
        //    {
        //        categoria.Ativo = false;
        //        await _categoriaRepositorio.Atualizar(categoria);
        //        _unityOfWork.Commit();
        //        return categoria;
        //    }
        //    else
        //        return BadRequest(ModelState);
        //}

        [HttpPut]
        [Route("AlterarStatus/{id}")]
        public async Task<ActionResult<Categoria>> AlterarStatus([FromBody] Categoria categoria, int id)
        {
            if (id != categoria.CategoriaId)
                return NotFound("não foi possivel atualizar categoria");
            try
            {
                if (categoria.StatusCategoria == StatusEnum.Ativo)
                {
                    categoria.StatusCategoria = StatusEnum.Inativo;
                    await _categoriaRepositorio.Atualizar(categoria);
                    _unityOfWork.Commit();
                    return categoria;
                }
                else if (categoria.StatusCategoria == StatusEnum.Inativo)
                {
                    categoria.StatusCategoria = StatusEnum.Ativo;
                    await _categoriaRepositorio.Atualizar(categoria);
                    _unityOfWork.Commit();
                    return categoria;
                }
                else
                    return BadRequest("Não foi possivel alterar");

            }
            catch
            {
                return BadRequest("Não foi possivel alterar");
            }
        }
    }

}

