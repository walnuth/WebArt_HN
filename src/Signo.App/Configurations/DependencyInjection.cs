using Microsoft.Extensions.DependencyInjection;
using Signo.Business.Interfaces;
using Signo.Data.Context;
using Signo.Data.Repository;

namespace Signo.App.Configurations
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {

            services.AddScoped<MeuDbContext>();

            services.AddScoped<IIntegranteRepository, IntegranteRepository>();

            //services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            //services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            //services.AddSingleton<IValidationAttributeAdapterProvider, ModelValidationAttributeAdapterProvider>();

            //services.AddScoped<INotificador, Notificador>();
            //services.AddScoped<IFornecedorService, FornecedorService>();
            //services.AddScoped<IProdutoService, ProdutoService>();



            return services;
        }
    }
}
