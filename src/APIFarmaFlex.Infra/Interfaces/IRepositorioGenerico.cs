using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIFarmaFlex.Infra.Interfaces
{
    public interface IRepositorioGenerico<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> PegarTodos();       
        Task<TEntity> PegarPeloId(int id);
        Task Inserir(TEntity entity);
        Task Atualizar(TEntity entity);
    }
}
