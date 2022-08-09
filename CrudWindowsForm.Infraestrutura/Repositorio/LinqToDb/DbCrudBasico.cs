using System.Configuration;
using LinqToDB;
using LinqToDB.Data;
using CrudWindowsForm.Dominio.Modelo;

namespace CrudWindowsForm.Infraestrutura.Repositorio.LinqToDb
{
    public class DbCrudBasico : DataConnection
    {
        static private ConnectionStringSettings _connectString = 
            new ConnectionStringSettings("CrudBasico",
                                         "Server=localhost\\SQLEXPRESS;Database=CrudBasico;User Id=sa;Password=AAASDSDsds@sdadsa@sdasds;Encrypt=false;",
                                         ProviderName.SqlServer);
        
        public DbCrudBasico() : base(_connectString.ProviderName, _connectString.ConnectionString) {
        }

        public ITable<Usuario> Usuarios => this.GetTable<Usuario>();
    }
}
