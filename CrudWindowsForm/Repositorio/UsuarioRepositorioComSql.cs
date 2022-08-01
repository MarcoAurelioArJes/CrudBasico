using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
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

            comando.Parameters.AddWithValue("nome", usuario.Nome);
            comando.Parameters.AddWithValue("email", usuario.Email);
            comando.Parameters.AddWithValue("senha", usuario.Senha);
            comando.Parameters.AddWithValue("dataNascimento", usuario.DataNascimento);
            comando.Parameters.AddWithValue("dataCriacao", usuario.DataCriacao);

            comando.Connection = conexaoBd.Conectar();

            comando.ExecuteNonQuery();

            conexaoBd.Desconectar();
        }
        public void Atualizar(Usuario usuario)
        {
            comando.CommandText = "UPDATE Usuarios SET (@nome, @email, " +
                                    "@senha, @dataNascimento, @dataCriacao)" +
                                    "WHERE Id = @id";

            comando.Parameters.AddWithValue("id", usuario.Id);
            comando.Parameters.AddWithValue("nome", usuario.Nome);
            comando.Parameters.AddWithValue("email", usuario.Email);
            comando.Parameters.AddWithValue("senha", usuario.Senha);
            comando.Parameters.AddWithValue("dataNascimento", usuario.DataNascimento);
            comando.Parameters.AddWithValue("dataCriacao", usuario.DataCriacao);

            comando.Connection = conexaoBd.Conectar();

            comando.ExecuteNonQuery();

            conexaoBd.Desconectar();
        }


        public void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> Listar()
        {
            comando.CommandText = "SELECT * FROM Usuarios;";

            comando.Connection = conexaoBd.Conectar();

            SqlDataReader lista = comando.ExecuteReader();

            List<Usuario> listaUsuario = new List<Usuario>();
            while(lista.Read()) {
                listaUsuario.Add(new Usuario()
                {
                    Id = int.Parse(lista["Id"].ToString()),
                    Nome = lista["Nome"].ToString(),
                    Email = lista["Email"].ToString(),
                    Senha = lista["Senha"].ToString(),
                    DataNascimento = DateTime.Parse(lista["DataNascimento"].ToString()),
                    DataCriacao = DateTime.Parse(lista["DataCriacao"].ToString())
                });
            }
            
            conexaoBd.Desconectar();

            return listaUsuario;
        }

        public Usuario ObterPorId(int id)
        {
            comando.CommandText = "SELECT * FROM Usuarios WHERE Id = @id;";

            comando.Connection = conexaoBd.Conectar();

            comando.Parameters.AddWithValue("id", id);

            SqlDataReader lista = comando.ExecuteReader();
            List<Usuario> listaUsuario = new List<Usuario>();
            listaUsuario.Add(new Usuario()
            {
                Id = int.Parse(lista["Id"].ToString()),
                Nome = lista["Nome"].ToString(),
                Email = lista["Email"].ToString(),
                Senha = lista["Senha"].ToString(),
                DataNascimento = DateTime.Parse(lista["DataNascimento"].ToString()),
                DataCriacao = DateTime.Parse(lista["DataCriacao"].ToString())
            });

            return listaUsuario[0];
        }

        public bool EmailEstaDuplicado(string email, string id)
        {
            comando.CommandText = "SELECT * FROM Usuarios WHERE Id = @id AND Email = @email;";

            comando.Connection = conexaoBd.Conectar();

            comando.Parameters.AddWithValue("id", id);
            comando.Parameters.AddWithValue("email", email);

            SqlDataReader lista = comando.ExecuteReader();

            conexaoBd.Desconectar();

            return lista != null;
        }
    }
}
