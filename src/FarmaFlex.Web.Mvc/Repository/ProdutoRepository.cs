using APIFarmaFlex.Domain.Enum;
using APIFarmaFlex.Domain.Models;
using FarmaFlex.Web.Mvc.Interface.IRepository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FarmaFlex.Web.Mvc.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiURL = "https://localhost:44367/api/produtos";
        
        public async Task<Produto> AlterarProduto(Produto produto)
        {
            Produto produtoRecebido = new Produto();
            StringContent body = new StringContent(JsonConvert.SerializeObject(produto), Encoding.UTF8, "application/json");
            using (var resposta = await _httpClient.PutAsync($"{_apiURL}/Atualizar/{produto.ProdutoId}", body))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                produtoRecebido = JsonConvert.DeserializeObject<Produto>(apiResposta);
            }
            return produtoRecebido;
        }
        public async Task<string> GerarURL(string base64)
        {
            string url;
            var parametros = new Dictionary<string, string>
            {
                {"key","10ac2aed03de38d35a8a7857f266d6e5" },
                {"image", base64 }
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var conteudo = new FormUrlEncodedContent(parametros);
            var resposta = await _httpClient.PostAsync("https://api.imgbb.com/1/upload", conteudo);
            var respostaString = await resposta.Content.ReadAsStringAsync();

            JObject joResposta = JObject.Parse(respostaString);
            JObject joDados = (JObject)joResposta["data"];
            url = joDados["display_url"].ToString();
            return url;
        }
        public async Task<Produto> InserirProduto(Produto produto)
        {
            produto.Ativo = true;
            Produto produtoRecebido = new Produto();
            StringContent body = new StringContent(JsonConvert.SerializeObject(produto), Encoding.UTF8, "application/json");
            using (var resposta = await _httpClient.PostAsync($"{_apiURL}/Registrar", body))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                produtoRecebido = JsonConvert.DeserializeObject<Produto>(apiResposta);
            }
            return produtoRecebido;
        }

        public async Task<IEnumerable<Produto>> ObterProdutos()
        {
            IEnumerable<Produto> produtos;
            using (var resposta = await _httpClient.GetAsync(_apiURL+"/buscartodos"))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                produtos = JsonConvert.DeserializeObject<IEnumerable<Produto>>(apiResposta);
            }
            return produtos;
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorNome(string filtro)
        {
            IEnumerable<Produto> produtos;
            using (var resposta = await _httpClient.GetAsync(_apiURL + filtro))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                produtos = JsonConvert.DeserializeObject<IEnumerable<Produto>>(apiResposta);
            }
            return produtos;
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorStatus(int status)
        {
            IEnumerable<Produto> produtos;
            using (var resposta = await _httpClient.GetAsync(_apiURL + status))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                produtos = JsonConvert.DeserializeObject<IEnumerable<Produto>>(apiResposta);
            }
            return produtos;
        }
        public async Task<Produto> ObterProdutosPorId(int id)
        {
            IEnumerable<Produto> produtos;
            using (var resposta = await _httpClient.GetAsync($"{_apiURL}/buscarporid/{id}"))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                produtos = JsonConvert.DeserializeObject<IEnumerable<Produto>>(apiResposta);
            }
            return produtos.First();
        }

        public async Task<Produto> InativarAtivarProduto(Produto produto)
        {
            if (produto.Ativo == true)
            {
                produto.Ativo = false;
                produto.StatusProduto = StatusEnum.Inativo;
            }
            else if (produto.Ativo == false)
            {
                produto.Ativo = true;
            }
            Produto produtoRecebida = new Produto();
            StringContent body = new StringContent(JsonConvert.SerializeObject(produto), Encoding.UTF8, "application/json");
            using (var resposta = await _httpClient.PutAsync($"{_apiURL}/Atualizar/{produto.ProdutoId}", body))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
               produtoRecebida = JsonConvert.DeserializeObject<Produto>(apiResposta);
            }
            return produtoRecebida;
        }
        public async Task<IEnumerable<Produto>> ObterProdutosAtivas()
        {
            IEnumerable<Produto> produtos;
            using (var resposta = await _httpClient.GetAsync($"{_apiURL}/BuscarAtivos"))
            {
                string resultado = await resposta.Content.ReadAsStringAsync();
                produtos = JsonConvert.DeserializeObject<IEnumerable<Produto>>(resultado);
            }
            return produtos;
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPromocionais()
        {
            IEnumerable<Produto> produtos;
            using (var resposta = await _httpClient.GetAsync($"{_apiURL}/BuscarPromocionais"))
            {
                string resultado = await resposta.Content.ReadAsStringAsync();
                produtos = JsonConvert.DeserializeObject<IEnumerable<Produto>>(resultado);
            }
            return produtos;
        }

        public async Task<IEnumerable<Produto>> ObterCategoriasInativas()
        {
            IEnumerable<Produto> produtos;
            using (var resposta = await _httpClient.GetAsync($"{_apiURL}/BuscarInativos"))
            {
                string resultado = await resposta.Content.ReadAsStringAsync();
                produtos = JsonConvert.DeserializeObject<IEnumerable<Produto>>(resultado);
            }
            return produtos;
        }

        public async Task<Produto> AlterarStatus(Produto produto)
        {
            Produto produtoRecebido = new Produto();
            StringContent body = new StringContent(JsonConvert.SerializeObject(produto), Encoding.UTF8, "application/json");
            using (var resposta = await _httpClient.PutAsync($"{_apiURL}/AlterarStatus/{produto.ProdutoId}", body))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                produtoRecebido = JsonConvert.DeserializeObject<Produto>(apiResposta);
            }
            return produtoRecebido;
        }
    }
}
