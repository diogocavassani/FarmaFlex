using APIFarmaFlex.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIFarmaFlex.Infra.Mapping
{
   public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.ProdutoId);
            builder.Property(p => p.CategoriaId).IsRequired();
            builder.Property(p => p.Descricao);
            builder.Property(p => p.Nome).IsRequired();
            builder.Property(p => p.EAN).IsRequired();
            builder.Property(p => p.Foto).IsRequired();
            builder.Property(p => p.Preco).IsRequired();
            builder.Property(p => p.StatusProduto).IsRequired();
            builder.Property(p => p.PrecoPromocional);
            builder.HasOne(p => p.Categoria).WithMany(p => p.Produtos).HasForeignKey(p => p.CategoriaId);
            builder.HasMany(p => p.ItensPedidos).WithOne(p => p.Produto);
            builder.ToTable("Produtos");
        }
    }
}
