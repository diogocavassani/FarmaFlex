using APIFarmaFlex.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIFarmaFlex.Infra.Mapping
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.ClienteId);
            builder.Property(c => c.Nome).IsRequired();
            builder.Property(c => c.CPF).IsRequired();           
            builder.Property(c => c.DataNascimento).IsRequired();
            builder.Property(c => c.Status).IsRequired();
            builder.Property(c => c.Funcao).IsRequired();
            builder.Property(c => c.Email).IsRequired();            
            builder.Property(c => c.Senha).IsRequired();
            builder.ToTable("Clientes");
        }
    }
}
