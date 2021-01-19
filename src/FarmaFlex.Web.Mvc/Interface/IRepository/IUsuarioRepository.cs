using APIFarmaFlex.Domain.Models;
using FarmaFlex.Web.Mvc.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaFlex.Web.Mvc.Interface.IRepository
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> ObterUsuarios();
        Task<Usuario> InserirUsuario(Usuario usuario);
        Task<Usuario> AtualizarUsuario(Usuario usuario);
        Task<UsuarioDTO> LogarUsuario(UsuarioDTO usuario);
    }
}
