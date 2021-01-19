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
    public class CategoriaController : Controller
    {
        private readonly CategoriaRepository _categoriaRepository;
        public CategoriaController(CategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoriaRepository.ObterCategoriasAtivas());
        }

        public async Task<IActionResult> Details(int id)
        {
            var categoria = await _categoriaRepository.ObterCategoriaPorId(id);
            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            try
            {
                await _categoriaRepository.InserirCategoria(categoria);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                return NotFound();
            }
            
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _categoriaRepository.ObterCategoriaPorId(id);
            if (categoria == null)
                return NotFound();
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(int id, Categoria categoria)
        {
            categoria.Ativo = true;
            categoria.CategoriaId = id;
            if(ModelState.IsValid)
            {
               var teste = await _categoriaRepository.AtualizarCategoria(categoria);
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }
        public async Task<IActionResult> AlterarStatus(int id)
        {
            var categoria = await _categoriaRepository.ObterCategoriaPorId(id);
            if (categoria != null)
            {
                await _categoriaRepository.AlterarStatus(categoria);
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _categoriaRepository.ObterCategoriaPorId(id);
            if (categoria != null)
            {
                await _categoriaRepository.InativarAtivarCategoria(categoria);
                return View(categoria);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _categoriaRepository.ObterCategoriaPorId(id);
            categoria.Ativo = false;
            await _categoriaRepository.AtualizarCategoria(categoria);
            return RedirectToAction(nameof(Index));
        }



    }
}
