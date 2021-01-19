using APIFarmaFlex.Domain.Enum;
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
    public class CategoriaRepository : ICategoriaRepository
    {

        private readonly HttpClient _httpcliente = new HttpClient();
        private readonly string _apiUrl = "https://localhost:44367/api/categorias";
      
        public async Task<Categoria> AtualizarCategoria(Categoria categoria)
        {
            Categoria categoriaRecebida = new Categoria();
            StringContent body = new StringContent(JsonConvert.SerializeObject(categoria), Encoding.UTF8, "application/json");
            using (var resposta = await _httpcliente.PutAsync($"{_apiUrl}/Atualizar/{categoria.CategoriaId}", body))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                categoriaRecebida = JsonConvert.DeserializeObject<Categoria>(apiResposta);
            }
            return categoriaRecebida;
        }

        public async Task<Categoria> AlterarStatus(Categoria categoria)
        {
            Categoria categoriaRecebida = new Categoria();
            StringContent body = new StringContent(JsonConvert.SerializeObject(categoria), Encoding.UTF8, "application/json");
            using (var resposta = await _httpcliente.PutAsync($"{_apiUrl}/AlterarStatus/{categoria.CategoriaId}", body))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                categoriaRecebida = JsonConvert.DeserializeObject<Categoria>(apiResposta);
            }
            return categoriaRecebida;
        }
        public async Task<Categoria> InativarAtivarCategoria(Categoria categoria)
        {
            if (categoria.Ativo ==true)
            {
                categoria.Ativo = false;
                categoria.StatusCategoria = StatusEnum.Inativo;
            }
            else if (categoria.Ativo ==false)
            {
                categoria.Ativo = true;
            }
            Categoria categoriaRecebida = new Categoria();
            StringContent body = new StringContent(JsonConvert.SerializeObject(categoria), Encoding.UTF8, "application/json");
            using (var resposta = await _httpcliente.PutAsync($"{_apiUrl}/Alterar/{categoria.CategoriaId}", body))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                categoriaRecebida = JsonConvert.DeserializeObject<Categoria>(apiResposta);
            }
            return categoriaRecebida;
        }

        public async Task<Categoria> InserirCategoria(Categoria categoria)
        {
            categoria.Ativo = true;
            Categoria categoriaRecebida = new Categoria();
            StringContent body = new StringContent(JsonConvert.SerializeObject(categoria), Encoding.UTF8, "application/json");
            using (var resposta = await _httpcliente.PostAsync($"{_apiUrl}/Registrar", body))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                categoriaRecebida = JsonConvert.DeserializeObject<Categoria>(apiResposta);
            }
            return categoriaRecebida;
        }

        public async Task<IEnumerable<Categoria>> ObterCategorias()
        {
            IEnumerable<Categoria> categorias;
            using (var resposta = await _httpcliente.GetAsync($"{_apiUrl}/Buscar"))
            {
                string resultado = await resposta.Content.ReadAsStringAsync();
                categorias = JsonConvert.DeserializeObject<IEnumerable<Categoria>>(resultado);
            }
            return categorias;
        }
        public async Task<IEnumerable<Categoria>> ObterCategoriasAtivas()
        {
            IEnumerable<Categoria> categorias;
            using (var resposta = await _httpcliente.GetAsync($"{_apiUrl}/BuscarAtivos"))
            {
                string resultado = await resposta.Content.ReadAsStringAsync();
                categorias = JsonConvert.DeserializeObject<IEnumerable<Categoria>>(resultado);
            }
            return categorias;
        }

        public async Task<IEnumerable<Categoria>> ObterCategoriasInativas()
        {
            IEnumerable<Categoria> categorias;
            using (var resposta = await _httpcliente.GetAsync($"{_apiUrl}/BuscarInativos"))
            {
                string resultado = await resposta.Content.ReadAsStringAsync();
                categorias = JsonConvert.DeserializeObject<IEnumerable<Categoria>>(resultado);
            }
            return categorias;
        }

        public async Task<IEnumerable<Categoria>> ObterCategoriasPorNome(string nome)
        {
            IEnumerable<Categoria> categorias;

            using (var resposta = await _httpcliente.GetAsync($"{_apiUrl}/BuscarPorNome/{nome}"))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                categorias = JsonConvert.DeserializeObject<IEnumerable<Categoria>>(apiResposta);
            }
            return categorias;
        }

        public async Task<Categoria> ObterCategoriaPorId(int id)
        {
            Categoria categorias;

            using (var resposta = await _httpcliente.GetAsync($"{_apiUrl}/BuscarPorId/{id}"))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                categorias = JsonConvert.DeserializeObject<Categoria>(apiResposta);
            }
            return categorias;
        }
    }
}
