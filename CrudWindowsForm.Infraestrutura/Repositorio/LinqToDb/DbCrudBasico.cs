using System.Configuration;
using LinqToDB;
using LinqToDB.Data;
using CrudWindowsForm.Dominio.Modelo;

namespace CrudWindowsForm.Infraestrutura.Repositorio.LinqToDb
{
    public class DbCrudBasico : DataConnection
    {
        static private ConnectionStringSettings _stringConexao = 
            new ConnectionStringSettings("CrudBasico",
                                         "Server=localhost\\B1;Database=CrudBasico;User Id=sa;Password=AAASDSDsds@sdadsa@sdasds;Encrypt=false;",
                                         ProviderName.SqlServer);
        
        public DbCrudBasico() : base(_stringConexao.ProviderName, _stringConexao.ConnectionString) {
        }

        public ITable<Usuario> Usuarios => this.GetTable<Usuario>();
    }
}
