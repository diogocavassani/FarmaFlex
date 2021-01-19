using APIFarmaFlex.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIFarmaFlex.Infra.Mapping
{
    public class FormaPagamentoMap : IEntityTypeConfiguration<FormaPagamento>
    {
        public void Configure(EntityTypeBuilder<FormaPagamento> builder)
        {
            builder.HasKey(f => f.FormaPagamentoId);
            builder.Property(f => f.Descricao).IsRequired();
            builder.Property(f => f.StatusForma).IsRequired();
            builder.ToTable("FormaPagamentos");
        }
    }
}
