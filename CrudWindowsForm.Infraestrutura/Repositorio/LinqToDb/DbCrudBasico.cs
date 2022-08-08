using System.Configuration;
using LinqToDB;
using LinqToDB.Data;

namespace CrudWindowsForm.Infraestrutura.Repositorio.LinqToDb
{
    public class DbCrudBasico : DataConnection
    {
        public DbCrudBasico()
        {
            var db = new DataConnection(
              ProviderName.SqlServer2019,
              "Server=localhost\\SQLEXPRESS;Database=CrudBasico;Trusted_Connection=True;Enlist=False;");
        }


        public ITable<UsuarioRepositorioComLinqToDb> Usuarios
            => this.GetTable<UsuarioRepositorioComLinqToDb>();

    }
}
