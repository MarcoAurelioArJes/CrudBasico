using System.Text;
using System.Data;
using System.Data.SqlClient;
using CrudWindowsForm.Dominio.Modelo;
using CrudWindowsForm.Dominio.Seguranca;
using System.Security.Cryptography;
using CrudWindowsForm.Dominio.Interfaces;

namespace CrudWindowsForm.Infraestrutura.Repositorio
{
    public class UsuarioRepositorioComSql : IRepositorioUsuario
    {
        private string _stringConexao = @"Server=localhost\SQLEXPRESS;Database=CrudBasico;Trusted_Connection=True;";

        public void Criar(Usuario usuario)
        {
            string consulta = "INSERT INTO Usuarios VALUES (@nome, @email, " +
                                    "@senha, @dataNascimento, @dataCriacao)";

            using (SqlConnection conexao = new SqlConnection(_stringConexao))
            {
                using (SqlCommand comando = new(consulta, conexao))
                {
                    conexao.Open();
                    AtribuiValoresParametros(usuario, comando);

                    comando.ExecuteNonQuery();
                }
            }

        }
        public void Atualizar(Usuario usuario)
        {
            string consulta = "UPDATE Usuarios SET Nome = @nome, " +
                                  "Email = @email, " +
                                  "Senha = @senha, " +
                                  "DataNascimento = @dataNascimento, " +
                                  "DataCriacao = @dataCriacao " +
                                  "WHERE Id = @id";

            using (SqlConnection conexao = new SqlConnection(_stringConexao))
            {
                using (SqlCommand comando = new(consulta, conexao))
                {
                    comando.Parameters.AddWithValue("id", usuario.Id);
                    conexao.Open();

                    AtribuiValoresParametros(usuario, comando);

                    comando.ExecuteNonQuery();
                }
            }
        }


        public void Deletar(int id)
        {
            string consulta = "DELETE FROM Usuarios WHERE Id = @id;";

            using (SqlConnection conexao = new SqlConnection(_stringConexao))
            {
                using (SqlCommand comando = new(consulta, conexao))
                {
                    comando.Parameters.AddWithValue("id", id);

                    conexao.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Usuario> ObterTodos()
        {
            string consulta = "SELECT * FROM Usuario;";

            using (SqlConnection conexao = new SqlConnection(_stringConexao))
            {
                using (SqlCommand comando = new(consulta, conexao))
                {
                    conexao.Open();
                    SqlDataReader retornoConsulta = comando.ExecuteReader();

                    List<Usuario> listaUsuarios = ObtemListaUsuarios(retornoConsulta);

                    return listaUsuarios;
                }
            }
        }

        public Usuario ObterPorId(int id)
        {
            string consulta = "SELECT * FROM Usuarios WHERE Id = @id;";

            using (SqlConnection conexao = new SqlConnection(_stringConexao))
            {
                using (SqlCommand comando = new(consulta, conexao))
                {
                    conexao.Open();

                    comando.Parameters.AddWithValue("id", id);

                    SqlDataReader retornoConsulta = comando.ExecuteReader();

                    Usuario usuario = ObtemListaUsuarios(retornoConsulta).FirstOrDefault()
                                      ?? throw new Exception($"Usuário não encontrado com o id {id}");

                    return usuario;
                }
            }
        }

        public bool EmailEstaDuplicado(string email, string id)
        {
            string consulta = "SELECT * FROM Usuarios WHERE Id != @id AND Email = @email;";

            using (SqlConnection conexao = new SqlConnection(_stringConexao))
            {
                using (SqlCommand comando = new(consulta, conexao))
                {
                    conexao.Open();

                    comando.Parameters.AddWithValue("id", id);
                    comando.Parameters.AddWithValue("email", email);

                    SqlDataReader retornoConsulta = comando.ExecuteReader();
                    bool encontrado = retornoConsulta.HasRows;

                    return encontrado;
                }
            }
            
        }

        public void AtribuiValoresParametros(Usuario usuario, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("nome", usuario.Nome);
            comando.Parameters.AddWithValue("email", usuario.Email);
            comando.Parameters.AddWithValue("senha", CriptografiaSenha.CriptografarSenha(usuario.Senha));
            comando.Parameters.AddWithValue("dataNascimento",
                                            usuario.DataNascimento == null ? DBNull.Value : usuario.DataNascimento);
            comando.Parameters.AddWithValue("dataCriacao", usuario.DataCriacao);
        }

        public List<Usuario> ObtemListaUsuarios(SqlDataReader retornoConsulta)
        {
            List<Usuario> listaUsuario = new List<Usuario>();

            while (retornoConsulta.Read())
            {
                Usuario usuario = new Usuario();

                usuario.Id = retornoConsulta.GetInt32("Id");
                usuario.Nome = retornoConsulta.GetString("Nome");
                usuario.Email = retornoConsulta.GetString("Email");
                usuario.Senha = CriptografiaSenha.DescriptografarSenha(retornoConsulta.GetString("Senha"));
                usuario.DataNascimento = retornoConsulta.IsDBNull("DataNascimento")
                                         ? null : retornoConsulta.GetDateTime("DataNascimento");
                usuario.DataCriacao = retornoConsulta.GetDateTime("DataCriacao");

                listaUsuario.Add(usuario);
            }

            return listaUsuario;
        }

        public List<Usuario> ObterPesquisa(string consulta)
        {
            throw new NotImplementedException();
        }
    }
}
