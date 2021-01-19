using APIFarmaFlex.Aplication.ViewModels.PedidoViewModels;
using APIFarmaFlex.Domain.Models;
using APIFarmaFlex.Infra.Interfaces;
using APIFarmaFlex.Infra.ORM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace APIFarmaFlex.Infra.Repository
{
    public class PedidoRepositorio : RepositorioGenerico<Pedido>, IPedidoRepositorio
    {
        private List<int> listadeId = new List<int>();
        private List<string> listadeNome = new List<string>();
        private List<string> listadeDescricao = new List<string>();
        private List<string> listadeEAN = new List<string>();
        private List<int> listadeQuatidade = new List<int>();
        private List<float> listaValorUnitario = new List<float>();
        private List<float> listaPrecoPromocional = new List<float>();
        private readonly DataContext _contexto;
        private List<ItensPedido> _listaItensPedidos;
        public PedidoRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;

        }
        public async Task AlterarPedido(PedidoViewModel pedidoViewModel)
        {
            var pedido = new Pedido();
            var itensPedido = new ItensPedido();
            pedido.ClienteId = pedidoViewModel.ClienteId;
            pedido.DataPedido = pedidoViewModel.DataPedido;
            pedido.DataUltimaAlteracao = DateTime.Now;
            pedido.FormapagamentoId = pedidoViewModel.FormaPagamentoId;
            pedido.Observacao = pedidoViewModel.Observacoes;
            pedido.StatusPedido = pedidoViewModel.StatusPedido;
            pedido.Total = pedidoViewModel.TotalPedido;
            pedido.UsuarioId = pedidoViewModel.UsuarioId;
            _contexto.Pedidos.Update(pedido);
            for (int i = 0; i <= pedidoViewModel.ProdutoId.Count(); i++)
            {
                itensPedido.Pedido = pedido;
                itensPedido.ProdutoId = pedidoViewModel.ProdutoId[i];
                itensPedido.QuantidadeProduto = pedidoViewModel.ProdutoId[i];
                _contexto.Update(itensPedido);
            }
        }
        public async Task AlterarStatusPedido(int id, int status) {
            var pedido = await _contexto.Set<Pedido>().Where(p => p.PedidoId == id).FirstAsync();
            if (status == 2)
            {
                pedido.StatusPedido = StatusEnumPedido.Iniciado;
                _contexto.Pedidos.Update(pedido);
                
            }
            else if (status == 3)
            {
                pedido.StatusPedido = StatusEnumPedido.EmEntrega;
                _contexto.Pedidos.Update(pedido);
            }
            else if (status == 4)
            {
                pedido.StatusPedido = StatusEnumPedido.Finalizado;
                _contexto.Pedidos.Update(pedido);
            }
            else if (status == 5)
            {
                pedido.StatusPedido = StatusEnumPedido.Cancelado;
                _contexto.Pedidos.Update(pedido);
            }
            
            
           
        }

        public async Task InserirPedido(PedidoViewModel pedidoViewModel)
        {
            var pedido = new Pedido();
            var itensPedido = new ItensPedido();
            pedido.ClienteId = pedidoViewModel.ClienteId;
            pedido.DataPedido = DateTime.Now;
            pedido.DataPedido = DateTime.Now;
            pedido.FormapagamentoId = pedidoViewModel.FormaPagamentoId;
            pedido.Observacao = pedidoViewModel.Observacoes;
            pedido.StatusPedido = pedidoViewModel.StatusPedido;
            pedido.Total = pedidoViewModel.TotalPedido;
            pedido.UsuarioId = pedidoViewModel.UsuarioId;

            await _contexto.AddAsync(pedido);
            for (int i = 0; i < pedidoViewModel.ProdutoId.Count(); i++)
            {

                itensPedido.Pedido = pedido;
                itensPedido.ProdutoId = pedidoViewModel.ProdutoId[i];
                itensPedido.QuantidadeProduto = pedidoViewModel.Quantidade[i];
                await _contexto.AddAsync(itensPedido);
            }

        }

        public async Task<IEnumerable<Pedido>> ListarPorStatus(StatusEnumPedido status)
        {
            return await _contexto.Pedidos.Include(p => p.Cliente).Include(p => p.Usuario).Include(p => p.ItensPedidos).Where(p => p.StatusPedido == status).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<DetalhesPedidoViewModel>> PedidosDetalhes(int id)
        {

            _listaItensPedidos = await _contexto.ItensPedidos.Include(i => i.Produto).Where(i => i.PedidoId == id).AsNoTracking().ToListAsync();

            for (int i = 0; i < _listaItensPedidos.Count(); i++)
            {
                if (_listaItensPedidos[i] != null)
                {
                    listadeId.Add(_listaItensPedidos[i].Produto.ProdutoId);
                    listadeNome.Add(_listaItensPedidos[i].Produto.Nome);
                    listadeDescricao.Add(_listaItensPedidos[i].Produto.Descricao);
                    listadeEAN.Add(_listaItensPedidos[i].Produto.EAN);
                    listadeQuatidade.Add(_listaItensPedidos[i].QuantidadeProduto);
                    listaValorUnitario.Add(_listaItensPedidos[i].Produto.Preco);
                    listaPrecoPromocional.Add(_listaItensPedidos[i].Produto.PrecoPromocional);
                }
            }
            return await _contexto.Pedidos.Include(p => p.Cliente).Include(p => p.Usuario).Include(p => p.ItensPedidos).Include(p => p.FormaPagamento).Where(p => p.PedidoId == id).Select(p => new
            DetalhesPedidoViewModel
            {
                ClienteId = p.ClienteId,
                NomeCliente = p.Cliente.Nome,
                SobreNomeCliente = p.Cliente.SobreNome,
                TelefoneCliente = p.Cliente.Telefone.NumeroTelefone,
                DescricaoProduto = listadeDescricao,
                EAN = listadeEAN,
                ProdutoId = listadeId,
                NomeProduto = listadeNome,
                UsuarioId = p.UsuarioId,
                Quantidade = listadeQuatidade,
                ValorUnitario = listaValorUnitario,
                ValorPromocao = listaPrecoPromocional,
                Logradouro = p.Cliente.Endereco.Logradouro,
                Numero = p.Cliente.Endereco.Numero,
                Bairro = p.Cliente.Endereco.Bairro,
                Cidade = p.Cliente.Endereco.Cidade,
                Observacoes = p.Observacao,
                FormaPagamento = p.FormaPagamento.Descricao,
                FormaPagamentoId = p.FormapagamentoId,
                TotalPedido = p.Total,
                PedidoId = p.PedidoId,
                StatusPedido = p.StatusPedido,
                DataPedido = p.DataPedido,
                UltimaAlteracao = p.DataUltimaAlteracao
            }).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ListarPedidoViewModel>> PedidosPorCliente(int id)
        {
            return await _contexto.Pedidos.Include(p => p.Cliente).Include(p => p.Usuario).Include(p => p.ItensPedidos).Where(p => p.ClienteId == id).Select(p => new ListarPedidoViewModel
            {
                NomeCliente = p.Cliente.Nome,
                SobreNomeCliente = p.Cliente.SobreNome,
                PedidoId = p.PedidoId,
                DataPedido = p.DataPedido,
                TelefoneCliente = p.Cliente.Telefone.NumeroTelefone,
                Status = p.StatusPedido,
                TotalPedido = p.Total

            }).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ListarPedidoViewModel>> PegarPedidoCompleto()
        {
            return await _contexto.Pedidos.Include(p => p.Cliente).Include(p => p.Usuario).Include(p => p.ItensPedidos).Select(p => new ListarPedidoViewModel
            {
                NomeCliente = p.Cliente.Nome,
                SobreNomeCliente = p.Cliente.SobreNome,
                PedidoId = p.PedidoId,
                DataPedido = p.DataPedido,
                TelefoneCliente = p.Cliente.Telefone.NumeroTelefone,
                Status = p.StatusPedido,
                TotalPedido = p.Total
            }).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Pedido>> PegarListarPedidos()
        {
            return await _contexto.Pedidos.Include(p => p.Cliente).Include(p => p.Usuario).Include(p => p.ItensPedidos).Include(p=>p.FormaPagamento).AsNoTracking().ToListAsync();
        }
        public async Task<Pedido> DetalhesPedidos(int id)
        {

            return await _contexto.Pedidos.Include(p => p.Cliente).Include(p => p.Usuario).Include(p => p.ItensPedidos).Include(p => p.FormaPagamento).Where(p => p.PedidoId == id).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
