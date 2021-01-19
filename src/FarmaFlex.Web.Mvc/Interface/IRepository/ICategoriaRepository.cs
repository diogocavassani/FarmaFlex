using APIFarmaFlex.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaFlex.Web.Mvc.Interface.IRepository
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> ObterCategorias();
        Task<IEnumerable<Categoria>> ObterCategoriasPorNome(string nome);
        Task<Categoria> AtualizarCategoria(Categoria categoria);
        Task<Categoria> InserirCategoria(Categoria categoria);

    }
}
