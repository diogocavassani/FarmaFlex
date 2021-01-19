using System.Threading.Tasks;
using APIFarmaFlex.Domain.Models;
using FarmaFlex.Web.Mvc.Interface.IRepository;
using FarmaFlex.Web.Mvc.Repository;
using FarmaFlex.Web.Mvc.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FarmaFlex.Web.Mvc.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioRepository _usuarioRepository;

        public UsuarioController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _usuarioRepository.ObterUsuariosAtivos());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    await _usuarioRepository.InserirUsuario(usuario);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(usuario);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string Email, string Senha)
        {
            UsuarioDTO usuario = new UsuarioDTO();
            usuario.Email = Email;
            usuario.Senha = Senha;
            if (usuario.Email !=null)
            {
              var resposta =   await _usuarioRepository.LogarUsuario(usuario);
                if (resposta.Email != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                    return View();
                
            }
            return View();
        }
       
        
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _usuarioRepository.ObterUsuariosPorId(id);
            if (usuario != null)
            {
                await _usuarioRepository.InativarAtivarUsuario(usuario);
                return View(usuario);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _usuarioRepository.ObterUsuariosPorId(id);
            usuario.Ativo = false;
            await _usuarioRepository.AtualizarUsuario(usuario);
            return RedirectToAction(nameof(Index));
        }

    }
}
