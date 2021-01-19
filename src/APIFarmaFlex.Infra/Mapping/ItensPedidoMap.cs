using APIFarmaFlex.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIFarmaFlex.Infra.Mapping
{
    public class ItensPedidoMap : IEntityTypeConfiguration<ItensPedido>
    {
        public void Configure(EntityTypeBuilder<ItensPedido> builder)
        {
            builder.HasKey(i => i.ItensPedidoId);
            builder.Property(i => i.PedidoId).IsRequired();
            builder.Property(i => i.ProdutoId).IsRequired();
            builder.Property(i => i.QuantidadeProduto).IsRequired();
            builder.HasOne(i => i.Pedido).WithMany(i => i.ItensPedidos).HasForeignKey(i => i.PedidoId);
            builder.HasOne(i => i.Produto).WithMany(i => i.ItensPedidos).HasForeignKey(i => i.ProdutoId);
            builder.ToTable("ItensPedidos");
        }
    }
}
