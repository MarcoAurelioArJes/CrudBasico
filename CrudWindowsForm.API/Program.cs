using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using CrudWindowsForm.Infraestrutura.Migrations;

namespace CrudWindowsForm.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new RealizaMigracoes();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
    }
}