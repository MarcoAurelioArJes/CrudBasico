using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace CrudWindowsForm.Infraestrutura.Migrations
{
    public class RealizaMigracoes
    {
        static private ConnectionStringSettings _connectString = new ConnectionStringSettings("CrudBasico",
                                                             "Server=localhost\\SQLEXPRESS;Database=CrudBasico;User Id=sa;Password=AAASDSDsds@sdadsa@sdasds;Encrypt=false;",
                                                             "SqlServer");
        public RealizaMigracoes()
        {
            var serviceProvider = CreateServices();

            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        private IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(_connectString.ConnectionString)
                    .ScanIn(typeof(_20220811_AddTableUsuario).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);

        }
        private void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }
    }
}
