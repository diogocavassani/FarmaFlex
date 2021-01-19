using APIFarmaFlex.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaFlex.Web.Mvc.Interface.IRepository
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ObterClientes();
        Task<IEnumerable<Cliente>> ObterClientesPorNome(string filtro);
    }
}
