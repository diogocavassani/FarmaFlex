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
    public class ClienteRepository : IClienteRepository
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiURL = "https://localhost:44367/api/clientes";
       
        public async Task<IEnumerable<Cliente>> ObterClientes()
        {
            IEnumerable<Cliente> clientes;
            using (var resposta = await _httpClient.GetAsync(_apiURL+"/buscartodos"))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                clientes = JsonConvert.DeserializeObject<IEnumerable<Cliente>>(apiResposta);
            }
            return clientes;
        }

        public async Task<IEnumerable<Cliente>> ObterClientesPorNome(string filtro)
        {
            IEnumerable<Cliente> clientes;
            using (var resposta = await _httpClient.GetAsync(_apiURL+"/BuscarPorNome" + filtro))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                clientes = JsonConvert.DeserializeObject<IEnumerable<Cliente>>(apiResposta);
            }
            return clientes;
        }

        public async Task<Cliente> ObterClientesPorId(int id)
        {
            Cliente clientes;
            using (var resposta = await _httpClient.GetAsync(_apiURL +"/cliente/" +id))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                clientes = JsonConvert.DeserializeObject<Cliente>(apiResposta);
            }
            return clientes;
        }
        public async Task<Cliente> AlterarStatus(Cliente cliente, int id) {
            
            Cliente clienteRecebido;
            StringContent body = new StringContent(JsonConvert.SerializeObject(cliente),Encoding.UTF8,"application/json");
            using (var resposta  = await _httpClient.PutAsync(_apiURL+"/status/"+id,body)) {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                clienteRecebido = JsonConvert.DeserializeObject<Cliente>(apiResposta);
            }
            return clienteRecebido;

        }
    }
}
