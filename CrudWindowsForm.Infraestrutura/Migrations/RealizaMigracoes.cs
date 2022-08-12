using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace CrudWindowsForm.Infraestrutura.Migrations
{
    public class RealizaMigracoes
    {
        static private ConnectionStringSettings _stringDeConexao = new ConnectionStringSettings("CrudBasico",
                                                             "Server=localhost\\SQLEXPRESS;Database=CrudBasico;User Id=sa;Password=AAASDSDsds@sdadsa@sdasds;Encrypt=false;",
                                                             "SqlServer");
        public RealizaMigracoes()
        {
            var serviceProvider = CriarServicos();

            using (var scope = serviceProvider.CreateScope())
            {
                AtualizarBaseDeDados(scope.ServiceProvider);
            }
        }

        private static IServiceProvider CriarServicos()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(runnerBuilder => 
                    runnerBuilder.AddSqlServer()
                    .WithGlobalConnectionString(_stringDeConexao.ConnectionString)
                    .ScanIn(typeof(_20220812_AddTabelaUsuario).Assembly).For.Migrations())
                    .AddLogging(loginBuilder => loginBuilder.AddFluentMigratorConsole())
                    .BuildServiceProvider(false);
        }
        private static void AtualizarBaseDeDados(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }
    }
}
