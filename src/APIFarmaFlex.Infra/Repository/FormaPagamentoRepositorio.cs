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
    public class FormaPagamentoRepositorio : RepositorioGenerico<FormaPagamento>, IFormaPagamentoRepositorio
    {
        private readonly DataContext _contexto;

        public FormaPagamentoRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public  async Task<IEnumerable<FormaPagamento>> PegarPeloNome(string nome)
        {
            return await _contexto.Set<FormaPagamento>().Where(f => f.Descricao == nome).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<FormaPagamento>> PegarPeloSatus(StatusEnum status)
        {
            return await _contexto.Set<FormaPagamento>().Where(f => f.StatusForma == status).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<FormaPagamento>> PegarFormaPagamentosAtivas()
        {
            return await _contexto.Set<FormaPagamento>().Where(f => f.StatusForma == StatusEnum.Ativo).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<FormaPagamento>> PegarAtivos()
        {
            return await _contexto.Set<FormaPagamento>().Where(f => f.Ativo == true).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<FormaPagamento>> PegarInativos()
        {
            return await _contexto.Set<FormaPagamento>().Where(c => c.Ativo == false).AsNoTracking().ToListAsync();
        }
    }
}
