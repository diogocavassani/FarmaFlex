using APIFarmaFlex.Domain.Models;
using APIFarmaFlex.Infra.Interfaces;
using APIFarmaFlex.Infra.ORM;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIFarmaFlex.Infra.Repository
{
    public class EnderecoRepositorio : RepositorioGenerico<Endereco>, IEnderecoRepositorio
    {
        private readonly DataContext _contexto;
        public EnderecoRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
    }
}
