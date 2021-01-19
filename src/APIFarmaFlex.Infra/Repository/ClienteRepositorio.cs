using APIFarmaFlex.Domain.Enum;
using APIFarmaFlex.Domain.Models;
using APIFarmaFlex.Infra.Interfaces;
using APIFarmaFlex.Infra.ORM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFarmaFlex.Infra.Repository
{
    public class ClienteRepositorio : RepositorioGenerico<Cliente>, IClienteRepositorio
    {
        private readonly DataContext _contexto;
        public ClienteRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task AtualizarCliente(Cliente cliente)
        {
            //TODO: Verificar a necessidade de um metodo assincrono.
            Telefone telefone = cliente.Telefone;
            Endereco endereco = cliente.Endereco;
            _contexto.Update(cliente);
            _contexto.Update(telefone);
            _contexto.Update(endereco);
        }

        public async Task InserirCliente(Cliente cliente)
        {
            Telefone telefone = cliente.Telefone;
            Endereco endereco = cliente.Endereco;
            await _contexto.AddAsync(cliente);
            await _contexto.AddAsync(endereco);
            await _contexto.AddAsync(telefone);
        }

        public Cliente LogarCliente(string email, string senha)
        {

            return _contexto.Set<Cliente>().Where(c => c.Email.ToLower() == email.ToLower() && c.Senha.ToLower() == senha.ToLower()).Include(c => c.Endereco).Include(c => c.Telefone).AsNoTracking().FirstOrDefault();
        }

        public async Task<IEnumerable<Cliente>> PegarClienteCompleto()
        {
            try
            {
                return await _contexto.Clientes.Include(c => c.Telefone).Include(c => c.Endereco).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Cliente>> PegarPeloNome(string nome)
        {
            return await _contexto.Set<Cliente>().Where(c => c.Nome.Contains(nome) && c.SobreNome.Contains(nome)).AsNoTracking().ToListAsync();
        }
        //TODO: Verificar 
        public async Task<Cliente> PegarPeloId(int id)
        {
            return await _contexto.Set<Cliente>().Include(c => c.Endereco).Include(c => c.Telefone).Where(c => c.ClienteId == id).AsNoTracking().FirstAsync();
        }

        public async Task<IEnumerable<Cliente>> PegarPeloStatus(StatusEnum status)
        {
            try
            {
                return await _contexto.Clientes.Where(c => c.Status == status).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
