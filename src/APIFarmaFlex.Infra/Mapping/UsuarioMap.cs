using APIFarmaFlex.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIFarmaFlex.Infra.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.UsuarioId);
            builder.Property(u => u.NomeUsuario).IsRequired();
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Senha).IsRequired();
            builder.Property(u => u.Funcao).IsRequired();

            builder.ToTable("Usuarios");
        }
    }
}
