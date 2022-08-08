using System;
using LinqToDB;
using LinqToDB.Mapping;
using CrudWindowsForm.Interfaces;
using CrudWindowsForm.Modelo;

namespace CrudWindowsForm.Infraestrutura.Repositorio.LinqToDb
{
    [Table(Name = "Usuarios")]
    public class UsuarioRepositorioComLinqToDb : Usuario, IRepositorioUsuario 
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column(Name = "Nome"), NotNull]
        public string Nome { get; set; }
        [Column(Name = "Senha"), NotNull]
        public string Senha { get; set; }
        [Column(Name = "Email"), NotNull]
        public string Email { get; set; }
        [Column(Name = "DataNascimento")]
        public DateTime? DataNascimento { get; set; }
        [Column(Name = "DataCriacao"), NotNull]
        public DateTime DataCriacao { get; set; }

        public void Criar(Usuario usuario)
        {
            using (DbCrudBasico dbCrudBasico = new())
            {
                dbCrudBasico.Insert(usuario);
            }
        }
        public List<Usuario> ObterTodos()
        {
            using (DbCrudBasico dbCrudBasico = new())
            {
                var query = from usuarios
                            in dbCrudBasico.Usuarios
                            select usuarios;

                return query.ToList<Usuario>();
            }
        }

        public Usuario ObterPorId(int id)
        {
            using (DbCrudBasico dbCrudBasico = new())
            {
                var query = from usuarios
                            in dbCrudBasico.Usuarios
                            where usuarios.Id == id
                            select usuarios;

                return query as Usuario ?? throw new Exception($"Usuario com o id {id} não encontrado!");
            }
        }

        public void Atualizar(Usuario usuarioAtualizado)
        {
            using(DbCrudBasico dbCrudBasico = new())
            {
                dbCrudBasico.Usuarios
                    .Where(usuario => usuario.Id == usuarioAtualizado.Id)
                    .Set(usuario => usuario.Nome, usuarioAtualizado.Nome)
                    .Set(usuario => usuario.Senha, usuarioAtualizado.Senha)
                    .Set(usuario => usuario.Email, usuarioAtualizado.Email)
                    .Set(usuario => usuario.DataNascimento, usuarioAtualizado.DataNascimento.GetValueOrDefault())
                    .Update();
            }
        }

        public void Deletar(int id)
        {
            using (DbCrudBasico dbCrudBasico = new())
            {
                dbCrudBasico.Usuarios
                    .Where(usuario => usuario.Id == id)
                    .Delete();
            }
        }

        public bool EmailEstaDuplicado(string email, string id)
        {
            using (DbCrudBasico dbCrudBasico = new())
            {
                var query = from usuarios
                            in dbCrudBasico.Usuarios
                            where usuarios.Id.ToString() == id && usuarios.Email == email
                            select usuarios;

                return query != null;
            }
        }

    }
}
