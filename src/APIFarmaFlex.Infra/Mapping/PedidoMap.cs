using APIFarmaFlex.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIFarmaFlex.Infra.Mapping
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.PedidoId);            
            builder.Property(p => p.ClienteId).IsRequired();
            builder.Property(p => p.FormapagamentoId).IsRequired();
            builder.Property(p => p.UsuarioId);
            builder.Property(p => p.DataPedido).IsRequired();
            builder.Property(p => p.DataUltimaAlteracao).IsRequired();
            builder.Property(p => p.Observacao);
            builder.Property(p => p.StatusPedido).IsRequired();
            builder.Property(p => p.Total).IsRequired();
            builder.HasOne(p => p.Cliente).WithMany(p => p.Pedidos).HasForeignKey(p => p.ClienteId);
            builder.HasOne(p => p.FormaPagamento).WithMany(p => p.Pedidos).HasForeignKey(p => p.FormapagamentoId);
            builder.HasOne(p => p.Usuario).WithMany(p => p.Pedidos).HasForeignKey(p => p.UsuarioId);
            builder.HasMany(p => p.ItensPedidos).WithOne(p => p.Pedido);

            builder.ToTable("Pedidos");
        }
    }
}
