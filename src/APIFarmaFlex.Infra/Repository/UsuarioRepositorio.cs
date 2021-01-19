using APIFarmaFlex.Domain.Enum;
using APIFarmaFlex.Domain.Models;
using APIFarmaFlex.Infra.Interfaces;
using APIFarmaFlex.Infra.ORM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFarmaFlex.Infra.Repository
{
    public class UsuarioRepositorio : RepositorioGenerico<Usuario>, IUsuarioRepositorio
    {
        private readonly DataContext _contexto;
        public UsuarioRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public  Usuario LogarUsuario(string email, string senha)
        {
            return  _contexto.Set<Usuario>().Where(u => u.Email.ToLower() == email.ToLower() && u.Senha.ToLower() == senha.ToLower()).AsNoTracking().FirstOrDefault();
        }

        public async Task<IEnumerable<Usuario>> PegarPeloNome(string nome)
        {
            return await _contexto.Set<Usuario>().Where(u => u.NomeUsuario.Contains(nome)).AsNoTracking().ToListAsync();
        }

        public  async Task<IEnumerable<Usuario>> PegarPeloStatus(StatusEnum status)
        {
            return await _contexto.Set<Usuario>().Where(u => u.StatusUsuario == status).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Usuario>> PegarAtivos()
        {
            return await _contexto.Set<Usuario>().Where(c => c.Ativo == true).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Usuario>> PegarInativos()
        {
            return await _contexto.Set<Usuario>().Where(c => c.Ativo == true).AsNoTracking().ToListAsync();
        }
    }
}
