using APIFarmaFlex.Domain.Models;
using APIFarmaFlex.Infra.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIFarmaFlex.Infra.ORM
{
    public class DataContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<FormaPagamento> FormaPagamentos { get; set; }
        public DbSet<ItensPedido> ItensPedidos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Telefone> Telefone { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new CategoriaMap());
            builder.ApplyConfiguration(new ClienteMap());
            builder.ApplyConfiguration(new EnderecoMap());
            builder.ApplyConfiguration(new FormaPagamentoMap());
            builder.ApplyConfiguration(new ItensPedidoMap());
            builder.ApplyConfiguration(new PedidoMap());
            builder.ApplyConfiguration(new ProdutoMap());
            builder.ApplyConfiguration(new TelefoneMap());
            builder.ApplyConfiguration(new UsuarioMap());

        }
    }
}
