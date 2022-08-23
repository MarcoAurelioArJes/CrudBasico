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

        private readonly DbCrudBasico _dbCrudBasico;

        public UsuarioRepositorioComLinqToDb(DbCrudBasico crudBasico)
        {
            _dbCrudBasico = crudBasico;
        }

        public void Criar(Usuario usuario)
        {
            try
            {
                usuario.Senha = CriptografiaSenha.CriptografarSenha(usuario.Senha);
                _dbCrudBasico.Insert(usuario);
            }
            catch (Exception erro)
            {
                throw new Exception("Ocorreu um erro ao criar o usuário\n", erro);
            }
            finally
            {
                _dbCrudBasico.Close();
            }
        }

        public List<Usuario> ObterTodos()
        {
            try
            {
                var query = from usuarios
                            in _dbCrudBasico.Usuarios
                            select usuarios;

                return query.ToList();
            }
            catch (Exception erro)
            {
                throw new Exception("Ocorreu um erro ao listar os usuários", erro);
            }
            finally
            {
                _dbCrudBasico.Close();
            }
        }

        public Usuario ObterPorId(int id)
        {
            try
            {
                var usuarioRetornado = from usuario
                                        in _dbCrudBasico.Usuarios
                                       where usuario.Id == id
                                       select new Usuario
                                       {
                                           Id = usuario.Id,
                                           Nome = usuario.Nome,
                                           Senha = CriptografiaSenha.DescriptografarSenha(usuario.Senha),
                                           Email = usuario.Email,
                                           DataNascimento = usuario.DataNascimento.HasValue ? usuario.DataNascimento : null,
                                           DataCriacao = usuario.DataCriacao
                                       };

                return usuarioRetornado.Single() ?? throw new Exception($"Usuario com o id {id} não encontrado!");

            }
            catch (Exception erro)
            {
                throw new Exception($"Ocorreu um erro ao obter o usuário com o id {id}\n", erro);
            }
            finally
            {
                _dbCrudBasico.Close();
            }
        }

        public void Atualizar(Usuario usuarioAtualizado)
        {
            try
            {
                DateTime? dataNascimento = usuarioAtualizado.DataNascimento.HasValue ? usuarioAtualizado.DataNascimento : null;

                _dbCrudBasico.Usuarios
                    .Where(usuario => usuario.Id == usuarioAtualizado.Id)
                    .Set(usuario => usuario.Nome, usuarioAtualizado.Nome)
                    .Set(usuario => usuario.Senha, CriptografiaSenha.CriptografarSenha(usuarioAtualizado.Senha))
                    .Set(usuario => usuario.Email, usuarioAtualizado.Email)
                    .Set(usuario => usuario.DataNascimento, dataNascimento)
                    .Update();
            }
            catch (Exception erro)
            {
                throw new Exception($"Ocorreu um erro ao atualizar o usuário com id {usuarioAtualizado.Id}\n", erro);
            }
            finally
            {
                _dbCrudBasico.Close();
            }

        }

        public void Deletar(int id)
        {
            try
            {
                ObterPorId(id);
                _dbCrudBasico.Usuarios
                    .Where(usuario => usuario.Id == id)
                    .Delete();
            }
            catch (Exception erro)
            {
                throw new Exception($"Ocorreu um erro ao deletar o usuário\n", erro);
            }
            finally
            {
                _dbCrudBasico.Close();
            }
        }

        public bool EmailEstaDuplicado(string email, string id)
        {
            try
            {
                var query = from usuarios
                            in _dbCrudBasico.Usuarios
                            where usuarios.Id.ToString() != id && usuarios.Email == email
                            select usuarios;

                return query.ToList().Any();

            }
            catch (Exception erro)
            {
                throw new Exception($"Ocorreu um erro ao verificar o email do usuário com id {id}\n", erro);
            }
            finally
            {
                _dbCrudBasico.Close();
            }
        }
    }
}
