using APIFarmaFlex.Domain.Enum;
using APIFarmaFlex.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIFarmaFlex.Infra.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioGenerico<Usuario>
    {
        Task<IEnumerable<Usuario>> PegarPeloNome(string nome);
        Task<IEnumerable<Usuario>> PegarPeloStatus(StatusEnum status);

         Usuario LogarUsuario(string nome,string senha);
    }
}
