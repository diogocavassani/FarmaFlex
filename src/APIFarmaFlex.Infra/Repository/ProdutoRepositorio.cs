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
    public class ProdutoRepositorio : RepositorioGenerico<Produto>, IProdutoRepositorio
    {
        private readonly DataContext _contexto;
        public ProdutoRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Produto>> PegarPorId(int id)
        {
            return await _contexto.Produtos.Include(p => p.Categoria).Where(p => p.ProdutoId == id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Produto>> PegarProdutoComCategoria()
        {
            
            return await _contexto.Produtos.Include(p => p.Categoria).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Produto>> PegarProdutosAtivos()
        {
            return await _contexto.Produtos.Include(p => p.Categoria).Where(p => p.StatusProduto == StatusEnum.Ativo && p.Categoria.StatusCategoria == StatusEnum.Ativo).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Produto>> PegarProdutoPorCategoria(int categoriaId)
        {
            return await _contexto.Produtos.Include(p => p.Categoria).Where(p => p.CategoriaId == categoriaId).Where(p => p.StatusProduto == StatusEnum.Ativo).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Produto>> PegarProdutoPorNome(string nome)
        {
            return await _contexto.Produtos.Include(p => p.Categoria).Where(p => p.Descricao.Contains(nome)).Where(p => p.StatusProduto == StatusEnum.Ativo).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Produto>> PegarProdutoPorStatus(StatusEnum status)
        {
            return await _contexto.Produtos.Include(p => p.Categoria).Where(p => p.StatusProduto == status).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Produto>> PegarAtivos()
        {
            return await _contexto.Set<Produto>().Where(c => c.Ativo == true).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Produto>> PegarInativos()
        {
            return await _contexto.Set<Produto>().Where(c => c.Ativo == true).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Produto>> PegarPromocionais()
        {
            return await _contexto.Set<Produto>().Include(p => p.Categoria).Where(p => p.PrecoPromocional != 0 && p.StatusProduto==StatusEnum.Ativo && p.Categoria.StatusCategoria==StatusEnum.Ativo).AsNoTracking().ToListAsync();
        }
    }
}
