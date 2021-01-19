using APIFarmaFlex.Domain.Enum;
using APIFarmaFlex.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIFarmaFlex.Infra.Interfaces
{
    public interface IClienteRepositorio : IRepositorioGenerico<Cliente>
    {
        Task<IEnumerable<Cliente>> PegarClienteCompleto();
        Task<IEnumerable<Cliente>> PegarPeloStatus(StatusEnum status);
        Task<IEnumerable<Cliente>> PegarPeloNome(string nome);
        Task<Cliente> PegarPeloId(int id);

        Task InserirCliente(Cliente cliente);
        Task AtualizarCliente(Cliente cliente);
        Cliente LogarCliente(string nome, string senha);

        //TODO: Implementar assinatura para inativar o cliente com uma senha administrador.
    }
}
