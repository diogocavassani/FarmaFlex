using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using APIFarmaFlex.Domain.Models;
using FarmaFlex.Web.Mvc.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FarmaFlex.Web.Mvc.Controllers
{
    public class ProdutoController : Controller        
    {
        private readonly ProdutoRepository _produtoRepository;
        private readonly CategoriaRepository _categoriaRepository;
        public ProdutoController(ProdutoRepository produtoRepository, CategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }
        // GET: ProdutoController
        public async Task<ActionResult> Index()
        {

            return View(await _produtoRepository.ObterProdutosAtivas());
        }
        public async Task<ActionResult> IndexPromocao()
        {

            return View(await _produtoRepository.ObterProdutosPromocionais());
        }

        // GET: ProdutoController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View( await _produtoRepository.ObterProdutosPorId(id));
        }

        // GET: ProdutoController/Create
        public async Task<IActionResult> Create()
        {
            var categorias = await _categoriaRepository.ObterCategoriasAtivas();
            ViewData["CategoriaId"] = new SelectList(categorias, "CategoriaId", "Descricao");
            return View();
        }

        // POST: ProdutoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto,ICollection<IFormFile> foto)
        {
            produto.Ativo = true;
            produto.Preco = produto.Preco / 100;
            string url = "";
            foreach (var file in foto)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string s = Convert.ToBase64String(fileBytes);
                        url = await _produtoRepository.GerarURL(s);
                    }
                }
            };
            produto.Foto = url;

            await _produtoRepository.InserirProduto(produto);
            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutoController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _produtoRepository.ObterProdutosPorId(id));
        }

        // POST: ProdutoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Produto produto, ICollection<IFormFile> foto)
        {
            produto.Ativo = true;
            try
            {

                Produto produtolocal = await _produtoRepository.ObterProdutosPorId(produto.ProdutoId);
                string url = "";
                if (foto != null)
                {
                    foreach (var file in foto)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                var fileBytes = ms.ToArray();
                                string s = Convert.ToBase64String(fileBytes);
                                url = await _produtoRepository.GerarURL(s);
                            }
                        }
                        produto.Foto = url;
                    };
                }
                produto.Foto = produtolocal.Foto;

                await _produtoRepository.AlterarProduto(produto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> AlterarStatus(int id)
        {
            var produto = await _produtoRepository.ObterProdutosPorId(id);
            if (produto != null)
            {
                await _produtoRepository.AlterarStatus(produto);
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _produtoRepository.ObterProdutosPorId(id);
            if (produto != null)
            {
                await _produtoRepository.InativarAtivarProduto(produto);
                return View(produto);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _produtoRepository.ObterProdutosPorId(id);
            produto.Ativo = false;
            await _produtoRepository.AlterarProduto(produto);
            return RedirectToAction(nameof(Index));
        }

    }
}
