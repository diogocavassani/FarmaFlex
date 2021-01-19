using APIFarmaFlex.Aplication.Services;
using APIFarmaFlex.Infra.Interfaces;
using APIFarmaFlex.Infra.ORM;
using APIFarmaFlex.Infra.Repository;
using APIFarmaFlex.Infra.UOW;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace APIFarmaFlex
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //UseLazyLoadingProxies.
            services.AddDbContext<DataContext>(opcoes => opcoes.UseMySql(Configuration.GetConnectionString("ConexaoAzure")));
            services.AddScoped<DataContext, DataContext>();
            services.AddTransient<ICategoriaRepositorio, CategoriaRepositorio>();            
            services.AddTransient<IClienteRepositorio, ClienteRepositorio>();
            services.AddTransient<IEnderecoRepositorio, EnderecoRepositorio>();
            services.AddTransient<IFormaPagamentoRepositorio, FormaPagamentoRepositorio>();
            services.AddTransient<IItensPedidosRepositorio, ItensPedidoRepositorio>();
            services.AddTransient<IPedidoRepositorio, PedidoRepositorio>();
            services.AddTransient<IProdutoRepositorio, ProdutoRepositorio>();
            services.AddTransient<ITelefoneRepositorio, TelefoneRepositorio>();
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddTransient<IUnityOfWork, UnityOfWork>();

            services.AddScoped<CategoriaRepositorio, CategoriaRepositorio>();
            services.AddScoped<ClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<FormaPagamentoRepositorio, FormaPagamentoRepositorio>();
            services.AddScoped<ProdutoRepositorio, ProdutoRepositorio>();
            services.AddScoped<PedidoRepositorio, PedidoRepositorio>();
            services.AddScoped<UsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<UnityOfWork, UnityOfWork>();
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(options =>
     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
 );
            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.Secret)),
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //    };
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           // app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
