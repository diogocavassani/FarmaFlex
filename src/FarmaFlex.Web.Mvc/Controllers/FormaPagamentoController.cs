using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIFarmaFlex.Domain.Models;
using FarmaFlex.Web.Mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FarmaFlex.Web.Mvc.Controllers
{
    public class FormaPagamentoController : Controller
    {
        private readonly FormaPagamentoRepository _formaPagamentoRepository;
        public FormaPagamentoController(FormaPagamentoRepository formaPagamentoRepository)
        {
            _formaPagamentoRepository = formaPagamentoRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _formaPagamentoRepository.ObterFormaPagamentosAtivas());
        }

        public async Task<IActionResult> Details (int id)
        {
            var formaPagamento = await _formaPagamentoRepository.ObterFormasPagamentosPorId(id);
            if (formaPagamento == null)
                return NotFound();
            return View(formaPagamento);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var formaPagamento = await _formaPagamentoRepository.ObterFormasPagamentosPorId(id);
            if (formaPagamento == null)
                return NotFound();
            return View(formaPagamento);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FormaPagamento formaPagamento)
        {
            formaPagamento.Ativo = true;
            formaPagamento.FormaPagamentoId = id;
            if (ModelState.IsValid)
            {
                await _formaPagamentoRepository.AtualizarFormaPagamento(formaPagamento);
                return RedirectToAction(nameof(Index));
            }
            return View(formaPagamento);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FormaPagamento formaPagamento)
        {
            var retorno = await _formaPagamentoRepository.InserirFormaPagamento(formaPagamento);
            if (retorno == null)
                return View(formaPagamento);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var formapagamento = await _formaPagamentoRepository.ObterFormasPagamentosPorId(id);
            if (formapagamento != null)
            {
                await _formaPagamentoRepository.InativarAtivarFormaPagamento(formapagamento);
                return View(formapagamento);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formapagamento = await _formaPagamentoRepository.ObterFormasPagamentosPorId(id);
            formapagamento.Ativo = false;
            await _formaPagamentoRepository.AtualizarFormaPagamento(formapagamento);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AlterarStatus(int id)
        {
            var formaPagamento = await _formaPagamentoRepository.ObterFormasPagamentosPorId(id);
            if (formaPagamento != null)
            {
                await _formaPagamentoRepository.AlterarStatus(formaPagamento);
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
