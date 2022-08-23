using CrudWindowsForm.Dominio.Interfaces;
using CrudWindowsForm.Dominio.Validacoes;
using CrudWindowsForm.Infraestrutura.Repositorio.LinqToDb;

namespace CrudWindowsForm.API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection servicos)
        {
            servicos.AddScoped<IRepositorioUsuario, UsuarioRepositorioComLinqToDb>()
                .AddScoped<DbCrudBasico>()
                .AddScoped<IValidacaoDeUsuario, ValidacaoDeUsuario>();
            servicos.AddControllers();
            servicos.AddCors();
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(opcao => opcao.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
