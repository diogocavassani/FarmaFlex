using APIFarmaFlex.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIFarmaFlex.Infra.Mapping
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(e => e.EnderecoId);
            builder.Property(e => e.ClienteId).IsRequired();
            builder.Property(e => e.Logradouro).IsRequired();
            builder.Property(e => e.Numero).IsRequired();
            builder.Property(e => e.Complemento).IsRequired();
            builder.Property(e => e.Bairro).IsRequired();
            builder.Property(e => e.Cidade).IsRequired();
            builder.HasOne(e => e.Cliente).WithOne(e => e.Endereco);
            builder.ToTable("Enderecos");
        }
    }
}
