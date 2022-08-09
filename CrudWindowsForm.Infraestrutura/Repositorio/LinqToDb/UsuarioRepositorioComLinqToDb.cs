using System;
using LinqToDB;
using CrudWindowsForm.Dominio.Seguranca;
using CrudWindowsForm.Dominio.Interfaces;
using CrudWindowsForm.Dominio.Modelo;
using Microsoft.Data.SqlClient;

namespace CrudWindowsForm.Infraestrutura.Repositorio.LinqToDb
{
    public class UsuarioRepositorioComLinqToDb : IRepositorioUsuario
    {
        public void Criar(Usuario usuario)
        {
            try
            {
                DbCrudBasico dbCrudBasico = new();
                usuario.Senha = CriptografiaSenha.SenhaCriptografada(usuario.Senha);
                dbCrudBasico.Insert("TEste");
            }
            catch (Exception erro)
            {
                throw new Exception("Ocorreu um erro ao criar o usuário\n", erro);
            }
        }

        public List<Usuario> ObterTodos()
        {
            try
            {
                DbCrudBasico dbCrudBasico = new();

                var query = from usuarios
                            in dbCrudBasico.Usuarios
                            select usuarios;

                return query.ToList();
            }
            catch (Exception erro)
            {
                throw new Exception("Ocorreu um erro ao listar os usuários", erro);
            }
        }

        public Usuario ObterPorId(int id)
        {
            try
            {
                DbCrudBasico dbCrudBasico = new();
                var usuarioRetornado = from usuario
                                        in dbCrudBasico.Usuarios
                                       where usuario.Id == id
                                       select new Usuario
                                       {
                                           Id = usuario.Id,
                                           Nome = usuario.Nome,
                                           Senha = CriptografiaSenha.SenhaDescriptografada(usuario.Senha),
                                           Email = usuario.Email,
                                           DataNascimento = usuario.DataNascimento,
                                           DataCriacao = usuario.DataCriacao
                                       };

                return usuarioRetornado.Single() ?? throw new Exception($"Usuario com o id {id} não encontrado!");

            }
            catch (Exception erro)
            {
                throw new Exception($"Ocorreu um erro ao obter o usuário com o id {id}\n", erro);
            }
        }

        public void Atualizar(Usuario usuarioAtualizado)
        {
            try
            {
                DbCrudBasico dbCrudBasico = new();

                dbCrudBasico.Usuarios
                    .Where(usuario => usuario.Id == usuarioAtualizado.Id)
                    .Set(usuario => usuario.Nome, usuarioAtualizado.Nome)
                    .Set(usuario => usuario.Senha, CriptografiaSenha.SenhaCriptografada(usuarioAtualizado.Senha))
                    .Set(usuario => usuario.Email, usuarioAtualizado.Email)
                    .Set(usuario => usuario.DataNascimento, usuarioAtualizado.DataNascimento.GetValueOrDefault())
                    .Update();
            }
            catch (Exception erro)
            {
                throw new Exception($"Ocorreu um erro ao atualizar o usuário com id {usuarioAtualizado.Id}\n", erro);
            }

        }

        public void Deletar(int id)
        {
            try
            {
                DbCrudBasico dbCrudBasico = new();
                dbCrudBasico.Usuarios
                    .Where(usuario => usuario.Id == id)
                    .Delete();

            }
            catch (Exception erro)
            {
                throw new Exception($"Ocorreu um erro ao deletar o usuário\n", erro);
            }
        }

        public bool EmailEstaDuplicado(string email, string id)
        {
            try
            {
                DbCrudBasico dbCrudBasico = new();

                var query = from usuarios
                            in dbCrudBasico.Usuarios
                            where usuarios.Id.ToString() != id && usuarios.Email == email
                            select usuarios;

                return query.ToList().Count != 0;

            } catch (Exception erro)
            {
                throw new Exception($"Ocorreu um erro ao verificar o email do usuário com id {id}\n", erro);
            }
        }
    }
}
