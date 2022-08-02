using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;

namespace CrudWindowsForm.Servicos
{
    public class ConexaoBancoDeDados
    {
        private SqlConnection _conexao = new();
        public ConexaoBancoDeDados()
        {
            _conexao.ConnectionString = @"Server=localhost\SQLEXPRESS;Database=CrudBasico;Trusted_Connection=True;";
        }

        public SqlConnection Conectar()
        {
            if (_conexao.State == ConnectionState.Closed)
            {
                _conexao.Open();
            }

            return _conexao;
        }

        public void Desconectar()
        {
            if (_conexao.State == ConnectionState.Open)
            {
                _conexao.Close();
            }
        }
    }
}
