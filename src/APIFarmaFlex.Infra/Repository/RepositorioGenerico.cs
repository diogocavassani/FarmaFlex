using APIFarmaFlex.Infra.Interfaces;
using APIFarmaFlex.Infra.ORM;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFarmaFlex.Infra.Repository
{
    public class RepositorioGenerico<TEntity> : IRepositorioGenerico<TEntity> where TEntity : class
    {
        private readonly DataContext _contexto;
        public RepositorioGenerico(DataContext contexto)
        {
            _contexto = contexto;
        }

        public async Task Atualizar(TEntity entity)
        {
            //TODO: Se nao tem awai nao precisa de TASK
            _contexto.Set<TEntity>().Update(entity);
        }

        public async Task Inserir(TEntity entity)
        {
            await _contexto.Set<TEntity>().AddAsync(entity);
        }

        public async Task<TEntity> PegarPeloId(int id)
        {
            return await _contexto.Set<TEntity>().FindAsync(id);
        }
        

        public async Task<IEnumerable<TEntity>> PegarTodos()
        {
            return await _contexto.Set<TEntity>().AsNoTracking().ToListAsync();
        }
    }
}
