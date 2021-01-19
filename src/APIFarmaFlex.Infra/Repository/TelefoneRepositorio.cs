using APIFarmaFlex.Domain.Models;
using APIFarmaFlex.Infra.Interfaces;
using APIFarmaFlex.Infra.ORM;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIFarmaFlex.Infra.Repository
{
    public class TelefoneRepositorio : RepositorioGenerico<Telefone>, ITelefoneRepositorio
    {
        private readonly DataContext _contexto;
        public TelefoneRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
    }
}
