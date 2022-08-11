using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CrudWindowsForm.Dominio.Interfaces;
using CrudWindowsForm.Dominio.Validacoes;
using CrudWindowsForm.Infraestrutura.Repositorio.LinqToDb;
using CrudWindowsForm.Infraestrutura.Migrations;

namespace CrudWindowsForm
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            new RealizaMigracoes();

            IHost host = CreateHostBuilder().Build();
            var repositorioUsuario = host.Services.GetService<IRepositorioUsuario>();
            var validacaoDeUsuario = host.Services.GetService<IValidacaoDeUsuario>();
            Application.Run(new TelaListarUsuario(repositorioUsuario, validacaoDeUsuario));
        }

        static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                    services.AddScoped<IRepositorioUsuario, UsuarioRepositorioComLinqToDb>()
                    .AddScoped<DbCrudBasico>()
                    .AddScoped<IValidacaoDeUsuario, ValidacaoDeUsuario>());
    }
}