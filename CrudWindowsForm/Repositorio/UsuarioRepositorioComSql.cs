using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using CrudWindowsForm.Modelo;
using CrudWindowsForm.Servicos;

namespace CrudWindowsForm.Repositorio
{
    public class UsuarioRepositorioComSql : IRepositorioUsuario
    {
        ConexaoBancoDeDados conexaoBd = new();
        SqlCommand comando = new();

        public void Criar(Usuario usuario)
        {
            comando.CommandText = "INSERT INTO Usuarios VALUES (@nome, @email, " +
                                    "@senha, @dataNascimento, @dataCriacao)";

            AtribuiValoresParametros(usuario, comando);

            comando.Connection = conexaoBd.Conectar();

            comando.ExecuteNonQuery();

            conexaoBd.Desconectar();
        }
        public void Atualizar(Usuario usuario)
        {
            comando.CommandText = "UPDATE Usuarios SET Nome = @nome, " +
                                  "Email = @email, " +
                                  "Senha = @senha, " +
                                  "DataNascimento = @dataNascimento, " +
                                  "DataCriacao = @dataCriacao " +
                                  "WHERE Id = @id";

            comando.Parameters.AddWithValue("id", usuario.Id);
            AtribuiValoresParametros(usuario, comando);

            comando.Connection = conexaoBd.Conectar();

            comando.ExecuteNonQuery();

            conexaoBd.Desconectar();
        }


        public void Deletar(int id)
        {
            comando.CommandText = "DELETE FROM Usuarios WHERE Id = @id;";
            comando.Parameters.AddWithValue("id", id);

            comando.Connection = conexaoBd.Conectar();

            comando.ExecuteNonQuery();

            conexaoBd.Desconectar();
        }

        public List<Usuario> ObterTodos()
        {
            comando.CommandText = "SELECT * FROM Usuarios;";
            comando.Connection = conexaoBd.Conectar();

            SqlDataReader retornoConsulta = comando.ExecuteReader();

            List<Usuario> listaUsuarios = ObtemListaUsuarios(retornoConsulta);

            conexaoBd.Desconectar();

            return listaUsuarios;
        }

        public Usuario ObterPorId(int id)
        {
            comando.CommandText = "SELECT * FROM Usuarios WHERE Id = @id;";
            comando.Connection = conexaoBd.Conectar();

            comando.Parameters.AddWithValue("id", id);

            SqlDataReader retornoConsulta = comando.ExecuteReader();

            Usuario usuario = ObtemListaUsuarios(retornoConsulta)[0];

            return usuario;
        }

        public bool EmailEstaDuplicado(string email, string id)
        {
            comando.CommandText = "SELECT * FROM Usuarios WHERE Id != @id AND Email = @email;";
            comando.Connection = conexaoBd.Conectar();

            comando.Parameters.AddWithValue("id", id);
            comando.Parameters.AddWithValue("email", email);

            SqlDataReader retornoConsulta = comando.ExecuteReader();
            bool encontrado = retornoConsulta.HasRows;

            conexaoBd.Desconectar();

            return encontrado;
        }

        public void AtribuiValoresParametros(Usuario usuario, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("nome", usuario.Nome);
            comando.Parameters.AddWithValue("email", usuario.Email);
            comando.Parameters.AddWithValue("senha", SenhaCriptografada(usuario.Senha));
            comando.Parameters.AddWithValue("dataNascimento",
                                            usuario.DataNascimento == null ? DBNull.Value : usuario.DataNascimento);
            comando.Parameters.AddWithValue("dataCriacao", usuario.DataCriacao);
        }

        public List<Usuario> ObtemListaUsuarios(SqlDataReader retornoConsulta)
        {
            List<Usuario> listaUsuario = new List<Usuario>();

            retornoConsulta.ToString().ToList();
            while (retornoConsulta.Read())
            {
                Usuario usuario = new Usuario();

                usuario.Id = retornoConsulta.GetInt32("Id");
                usuario.Nome = retornoConsulta.GetString("Nome");
                usuario.Email = retornoConsulta.GetString("Email");
                usuario.Senha = retornoConsulta.GetString("Senha");
                usuario.DataNascimento = retornoConsulta.IsDBNull("DataNascimento") ? null : 
                                                                  retornoConsulta.GetDateTime("DataNascimento");
                usuario.DataCriacao = retornoConsulta.GetDateTime("DataCriacao");

                listaUsuario.Add(usuario);
            }

            return listaUsuario;
        }

        public string SenhaCriptografada(string senha)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

            RSA.ImportParameters(RSA.ExportParameters(false));

            byte[] senhaCriptografadaEmBytes = RSA.Encrypt(Encoding.ASCII.GetBytes(senha), false);

            string senhaCriptografadaEmString = Encoding.ASCII.GetString(senhaCriptografadaEmBytes);

            return senhaCriptografadaEmString;
        }

        public string SenhaDescriptografada(string senha)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

            RSA.ImportParameters(RSA.ExportParameters(true));

            byte[] senhaCriptografadaEmBytes = RSA.Decrypt(Encoding.ASCII.GetBytes(senha), false);

            string senhaDescriptografada = Encoding.ASCII.GetString(senhaCriptografadaEmBytes);

            return senhaDescriptografada;
        }
    }
}
