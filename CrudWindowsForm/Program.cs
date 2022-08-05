using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CrudWindowsForm.Interfaces;
using CrudWindowsForm.Repositorio;

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
            
            IHost host = CreateHostBuilder().Build();
            Application.Run(host.Services.GetService<TelaListarUsuario>());
        }

        static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                    services.AddTransient<IRepositorioUsuario, UsuarioRepositorioComSql>()
                            .AddSingleton<TelaListarUsuario>());
    }
}