using APIFarmaFlex.Domain.Enum;
using APIFarmaFlex.Domain.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIFarmaFlex.Infra.Interfaces
{

    public interface IFormaPagamentoRepositorio : IRepositorioGenerico<FormaPagamento>
    {
        Task<IEnumerable<FormaPagamento>> PegarPeloNome(string nome);
        Task<IEnumerable<FormaPagamento>> PegarPeloSatus(StatusEnum status);

    }
}
