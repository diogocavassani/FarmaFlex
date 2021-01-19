using APIFarmaFlex.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIFarmaFlex.Infra.Mapping
{
    public class TelefoneMap : IEntityTypeConfiguration<Telefone>
    {
        public void Configure(EntityTypeBuilder<Telefone> builder)
        {
            builder.HasKey(t => t.TelefoneId);
            builder.Property(t => t.ClienteId).IsRequired();
            builder.Property(t => t.NumeroTelefone).IsRequired();
            builder.HasOne(t => t.Cliente).WithOne(t => t.Telefone);

            builder.ToTable("Telefones");
        }
    }
}
