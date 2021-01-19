using APIFarmaFlex.Domain.Models;
using FarmaFlex.Web.Mvc.Interface.IRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FarmaFlex.Web.Mvc.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiURL = "https://localhost:44367/api/pedidos";
       

        public async Task<Pedido> AlterarPedido(Pedido pedido)
        {
            Pedido pedidoRecebido = new Pedido();
            StringContent body = new StringContent(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");
            using (var resposta = await _httpClient.PutAsync($"{_apiURL}/Atualizar/{pedido.PedidoId}", body))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                pedidoRecebido = JsonConvert.DeserializeObject<Pedido>(apiResposta);
            }
            return pedidoRecebido;

        }

        public async Task AlterarStatus(Pedido pedido, int status)
        {
            Pedido pedidoRecebido = new Pedido();
            StringContent body = new StringContent(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");
            using (var resposta = await _httpClient.PutAsync($"{_apiURL}/AlterarStatus/{pedido.PedidoId}/{status}", body))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                pedidoRecebido = JsonConvert.DeserializeObject<Pedido>(apiResposta);
            }
        }



        public async Task<IEnumerable<Pedido>> ObterPedidos()
        {
            IEnumerable<Pedido> pedidos;
            using (var resposta = await _httpClient.GetAsync($"{_apiURL}/ListarWeb"))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                pedidos = JsonConvert.DeserializeObject<IEnumerable<Pedido>>(apiResposta);
            }
            return pedidos;
        }

        public async Task<Pedido> ObterPorId(int id)
        {
            Pedido pedido;
            using (var resposta = await _httpClient.GetAsync($"{_apiURL}/DetalhesPedidos/{id}"))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                pedido = JsonConvert.DeserializeObject<Pedido>(apiResposta);
            }
            return pedido;
        }

        public async Task<IEnumerable<Pedido>> ObterPedidosPorStatus(int status)
        {
            IEnumerable<Pedido> pedidos;
            using (var resposta = await _httpClient.GetAsync($"{_apiURL}/ListarPorStatus/{status}"))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                pedidos = JsonConvert.DeserializeObject<IEnumerable<Pedido>>(apiResposta);
            }
            return pedidos;
        }



    }
}
