using APIFarmaFlex.Domain.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaFlex.Web.Mvc.Interface
{
    public interface IUsuarioController
    {
        [Post("/usuario/login")]
        Task<Usuario> Logar();
    }
}
