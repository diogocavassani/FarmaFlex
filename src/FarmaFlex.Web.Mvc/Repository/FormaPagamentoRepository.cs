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
    public class FormaPagamentoRepository : IFormaPagamentoRepository
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiURL = "https://localhost:44367/api/formaPagamento";

        public StatusEnum StattusEnum { get; private set; }

        public async Task<FormaPagamento> AtualizarFormaPagamento(FormaPagamento formaPagamento)
        {
            FormaPagamento formaPagamentoRecebida = new FormaPagamento();
            StringContent body = new StringContent(JsonConvert.SerializeObject(formaPagamento), Encoding.UTF8, "application/json");
            using (var resposta = await _httpClient.PutAsync($"{_apiURL}/Atualizar/{formaPagamento.FormaPagamentoId}", body))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                formaPagamentoRecebida = JsonConvert.DeserializeObject<FormaPagamento>(apiResposta);
            }
            return formaPagamento;
        }

        public async Task<FormaPagamento> InserirFormaPagamento(FormaPagamento formaPagamento)
        {
            formaPagamento.Ativo = true;
            FormaPagamento formaPagamentoRecebida = new FormaPagamento();
            StringContent body = new StringContent(JsonConvert.SerializeObject(formaPagamento), Encoding.UTF8, "application/json");
            using (var resposta = await _httpClient.PostAsync($"{_apiURL}/Registrar", body))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                formaPagamentoRecebida = JsonConvert.DeserializeObject<FormaPagamento>(apiResposta);
            }
            return formaPagamentoRecebida;
        }

        public async Task<IEnumerable<FormaPagamento>> ObterFormasPagamentos()
        {
            IEnumerable<FormaPagamento> formaPagamentos;
            using (var resposta = await _httpClient.GetAsync($"{_apiURL}/Buscar"))
            {
                string resultado = await resposta.Content.ReadAsStringAsync();
                formaPagamentos = JsonConvert.DeserializeObject<IEnumerable<FormaPagamento>>(resultado);
            }
            return formaPagamentos;
        }

        public async Task<IEnumerable<FormaPagamento>> ObterFormasPagamentosPorNome(string filtro)
        {
            IEnumerable<FormaPagamento> formaPagamentos;
            using (var resposta = await _httpClient.GetAsync($"{_apiURL}/BuscarPorNome/{filtro}"))
            {
                string resultado = await resposta.Content.ReadAsStringAsync();
                formaPagamentos = JsonConvert.DeserializeObject<IEnumerable<FormaPagamento>>(resultado);
            }
            return formaPagamentos;
        }
        public async Task<FormaPagamento> ObterFormasPagamentosPorId(int id)
        {
            FormaPagamento formaPagamentos;
            using (var resposta = await _httpClient.GetAsync($"{_apiURL}/BuscarPorId/{id}"))
            {
                string resultado = await resposta.Content.ReadAsStringAsync();
                formaPagamentos = JsonConvert.DeserializeObject<FormaPagamento>(resultado);
            }
            return formaPagamentos;
        }
        public async Task<FormaPagamento> InativarAtivarFormaPagamento(FormaPagamento formapagamento)
        {
            if (formapagamento.Ativo == true)
            {
                formapagamento.Ativo = false;
                formapagamento.StatusForma = StatusEnum.Inativo;
            }
            else if (formapagamento.Ativo == false)
            {
                formapagamento.Ativo = true;
            }
            FormaPagamento formapagamentoRecebida = new FormaPagamento();
            StringContent body = new StringContent(JsonConvert.SerializeObject(formapagamento), Encoding.UTF8, "application/json");
            using (var resposta = await _httpClient.PutAsync($"{_apiURL}/Alterar/{formapagamento.FormaPagamentoId}", body))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                 formapagamentoRecebida= JsonConvert.DeserializeObject<FormaPagamento>(apiResposta);
            }
            return formapagamentoRecebida;
        }

        public async Task<IEnumerable<FormaPagamento>> ObterFormaPagamentosAtivas()
        {
            IEnumerable<FormaPagamento> formaPagamentos;
            using (var resposta = await _httpClient.GetAsync($"{_apiURL}/BuscarAtivos"))
            {
                string resultado = await resposta.Content.ReadAsStringAsync();
                formaPagamentos = JsonConvert.DeserializeObject<IEnumerable<FormaPagamento>>(resultado);
            }
            return formaPagamentos;
        }

        public async Task<IEnumerable<FormaPagamento>> ObterFormaPagamentosInativas()
        {
            IEnumerable<FormaPagamento> formaPagamentos;
            using (var resposta = await _httpClient.GetAsync($"{_apiURL}/BuscarInativos"))
            {
                string resultado = await resposta.Content.ReadAsStringAsync();
                formaPagamentos = JsonConvert.DeserializeObject<IEnumerable<FormaPagamento>>(resultado);
            }
            return formaPagamentos;
        }

        public async Task<FormaPagamento> AlterarStatus(FormaPagamento formaPagamento)
        {
            FormaPagamento formaPagamentoRecebida = new FormaPagamento();
            StringContent body = new StringContent(JsonConvert.SerializeObject(formaPagamento), Encoding.UTF8, "application/json");
            using (var resposta = await _httpClient.PutAsync($"{_apiURL}/AlterarStatus/{formaPagamento.FormaPagamentoId}", body))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                formaPagamentoRecebida = JsonConvert.DeserializeObject<FormaPagamento>(apiResposta);
            }
            return formaPagamentoRecebida;
        }
    }
}
