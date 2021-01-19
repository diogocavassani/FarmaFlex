using APIFarmaFlex.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaFlex.Web.Mvc.Interface.IRepository
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ObterProdutos();
        Task<IEnumerable<Produto>> ObterProdutosPorNome(string filtro);

        Task<IEnumerable<Produto>> ObterProdutosPorStatus(int status);

        Task<Produto> InserirProduto(Produto produto);
        Task<Produto> AlterarProduto(Produto produto);

    }
}
