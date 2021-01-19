using APIFarmaFlex.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaFlex.Web.Mvc.Interface.IRepository
{
    public interface IFormaPagamentoRepository
    {
        Task<IEnumerable<FormaPagamento>> ObterFormasPagamentos();
        Task<IEnumerable<FormaPagamento>> ObterFormasPagamentosPorNome(string filtro);

        Task<FormaPagamento> InserirFormaPagamento(FormaPagamento formaPagamento);
        Task<FormaPagamento> AtualizarFormaPagamento(FormaPagamento formaPagamento);

    }
}
