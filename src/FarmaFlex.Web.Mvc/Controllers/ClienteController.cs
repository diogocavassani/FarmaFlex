using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using APIFarmaFlex.Domain.Models;
using FarmaFlex.Web.Mvc.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FarmaFlex.Web.Mvc.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteRepository _clienteRepository;

        public ClienteController(ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<IActionResult> Index()
        {
           
            return View( await _clienteRepository.ObterClientes());
        }        
       
        public async  Task<IActionResult> Details(int id)
        {
            return View("Details",await _clienteRepository.ObterClientesPorId(id));
        }
        
        public async Task<IActionResult> Bloquear(Cliente cliente)
        {
            Cliente clienteLocal = await _clienteRepository.ObterClientesPorId(cliente.ClienteId);
             await _clienteRepository.AlterarStatus(clienteLocal,cliente.ClienteId);
            return RedirectToAction(nameof(Index));
        }
    }
}
