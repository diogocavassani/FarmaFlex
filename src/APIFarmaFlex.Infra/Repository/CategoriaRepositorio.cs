using APIFarmaFlex.Domain.Enum;
using APIFarmaFlex.Domain.Models;
using APIFarmaFlex.Infra.Interfaces;
using APIFarmaFlex.Infra.ORM;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFarmaFlex.Infra.Repository
{
    public class CategoriaRepositorio : RepositorioGenerico<Categoria>, ICategoriaRepositorio
    {
        private readonly DataContext _contexto;
        public CategoriaRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Categoria>> PegarAtivos()
        {
            return await _contexto.Set<Categoria>().Where(c => c.Ativo == true).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Categoria>> PegarInativos()
        {
            return await _contexto.Set<Categoria>().Where(c => c.Ativo == true).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Categoria>> PegarCategoriasAtivas()
        {
            return await _contexto.Set<Categoria>().Where(c => c.StatusCategoria == StatusEnum.Ativo).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Categoria>> PegarPeloNome(string nome)
        {
            return await _contexto.Set<Categoria>().Where(c => c.Descricao.Contains(nome)).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Categoria>> PegarPeloStatus(StatusEnum status)
        {
            return await _contexto.Set<Categoria>().Where(c => c.StatusCategoria ==status).AsNoTracking().ToListAsync();
        }
    }
}
