using APIFarmaFlex.Domain.Enum;
using APIFarmaFlex.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIFarmaFlex.Infra.Interfaces
{
    public interface ICategoriaRepositorio : IRepositorioGenerico<Categoria>
    {
        Task<IEnumerable<Categoria>> PegarPeloNome(string nome);
        Task<IEnumerable<Categoria>> PegarPeloStatus(StatusEnum status);
        Task<IEnumerable<Categoria>> PegarAtivos();
    }
}
