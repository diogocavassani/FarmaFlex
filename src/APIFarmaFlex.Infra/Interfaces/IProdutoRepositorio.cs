using APIFarmaFlex.Domain.Enum;
using APIFarmaFlex.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIFarmaFlex.Infra.Interfaces
{
    public interface IProdutoRepositorio : IRepositorioGenerico<Produto>
    {
        Task<IEnumerable<Produto>> PegarProdutoComCategoria();
        Task<IEnumerable<Produto>> PegarPorId(int id);
        Task<IEnumerable<Produto>> PegarProdutoPorCategoria(int id);
        Task<IEnumerable<Produto>> PegarProdutoPorNome(string nome);
        Task<IEnumerable<Produto>> PegarProdutoPorStatus(StatusEnum status);
        
    }
}
