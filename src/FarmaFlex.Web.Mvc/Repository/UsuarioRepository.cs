using APIFarmaFlex.Domain.Enum;
using APIFarmaFlex.Domain.Models;
using FarmaFlex.Web.Mvc.Interface.IRepository;
using FarmaFlex.Web.Mvc.Services.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FarmaFlex.Web.Mvc.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiURL = "https://localhost:44367/api/usuarios";


        public async Task<Usuario> AtualizarUsuario(Usuario usuario)
        {
            Usuario usuarioRecebido = new Usuario();
            StringContent body = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
            using (var resposta = await _httpClient.PutAsync($"{ _apiURL}/Atualizar/{usuario.UsuarioId}", body))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                usuarioRecebido = JsonConvert.DeserializeObject<Usuario>(apiResposta);
            }
            return usuarioRecebido;
        }

        public async Task<Usuario> InserirUsuario(Usuario usuario)
        {
            usuario.Ativo = true;
            Usuario usuarioRecebido = new Usuario();
            StringContent body = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
            using (var resposta = await _httpClient.PostAsync($"{ _apiURL}/Registrar", body))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                usuarioRecebido = JsonConvert.DeserializeObject<Usuario>(apiResposta);
            }
            return usuarioRecebido;
        }

        public async Task<UsuarioDTO> LogarUsuario(UsuarioDTO usuario)
        {
            UsuarioDTO usuarioRecebido = new UsuarioDTO();
            StringContent body = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
            using (var resposta = await _httpClient.PostAsync($"{ _apiURL}/Login", body))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                usuarioRecebido = JsonConvert.DeserializeObject<UsuarioDTO>(apiResposta);

            }
            return usuarioRecebido;
        }

        public async Task<IEnumerable<Usuario>> ObterUsuarios()
        {
            IEnumerable<Usuario> usuarios;
            using (var resposta = await _httpClient.GetAsync($"{ _apiURL}/BuscarTodos"))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                usuarios = JsonConvert.DeserializeObject<IEnumerable<Usuario>>(apiResposta);
            }
            return usuarios;
        }
        public async Task<Usuario> ObterUsuariosPorId(int id)
        {
            Usuario usuarios;
            using (var resposta = await _httpClient.GetAsync($"{ _apiURL}/BucarPorId/{id}"))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                usuarios = JsonConvert.DeserializeObject<Usuario>(apiResposta);
            }
            return usuarios;
        }

        public async Task<Usuario> InativarAtivarUsuario(Usuario usuario)
        {
            if (usuario.Ativo == true)
            {
                usuario.Ativo = false;
                usuario.StatusUsuario = StatusEnum.Inativo;
            }
            else if (usuario.Ativo == false)
            {
                usuario.Ativo = true;
            }
            Usuario UsuarioRecebida = new Usuario();
            StringContent body = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
            using (var resposta = await _httpClient.PutAsync($"{_apiURL}/Atualizar/{usuario.UsuarioId}", body))
            {
                string apiResposta = await resposta.Content.ReadAsStringAsync();
                UsuarioRecebida = JsonConvert.DeserializeObject<Usuario>(apiResposta);
            }
            return UsuarioRecebida;
        }
        public async Task<IEnumerable<Usuario>> ObterUsuariosAtivos()
        {
            IEnumerable<Usuario> usuarios;
            using (var resposta = await _httpClient.GetAsync($"{_apiURL}/BuscarAtivos"))
            {
                string resultado = await resposta.Content.ReadAsStringAsync();
                usuarios = JsonConvert.DeserializeObject<IEnumerable<Usuario>>(resultado);
            }
            return usuarios;
        }

        public async Task<IEnumerable<Usuario>> ObterUsuariosInativos()
        {
            IEnumerable<Usuario> usuarios;
            using (var resposta = await _httpClient.GetAsync($"{_apiURL}/BuscarInativos"))
            {
                string resultado = await resposta.Content.ReadAsStringAsync();
                usuarios = JsonConvert.DeserializeObject<IEnumerable<Usuario>>(resultado);
            }
            return usuarios;
        }

    }
}
