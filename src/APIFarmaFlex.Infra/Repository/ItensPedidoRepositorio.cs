using APIFarmaFlex.Domain.Models;
using APIFarmaFlex.Infra.Interfaces;
using APIFarmaFlex.Infra.ORM;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIFarmaFlex.Infra.Repository
{
    public class ItensPedidoRepositorio : RepositorioGenerico<ItensPedido>, IItensPedidosRepositorio
    {
        private readonly DataContext _contexto;
        public ItensPedidoRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
    }
}
