using CrudWindowsForm.Dominio.Interfaces;
using CrudWindowsForm.Dominio.Modelo;
using CrudWindowsForm.Servicos;

namespace CrudWindowsForm.Infraestrutura.Repositorio
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IRepositorioUsuario
    {
        public override void Criar(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new Exception("Não foi informado nenhum usuário");
            }

            _lista.Add(new Usuario()
            {
                Id = ListaSingleton<Usuario>.IdContador,
                Nome = usuario.Nome,
                Senha = usuario.Senha,
                Email = usuario.Email,
                DataNascimento = usuario.DataNascimento,
                DataCriacao = usuario.DataCriacao
            });
        }

        public bool EmailEstaDuplicado(string email, string id)
        {
            var encontrado = _lista
                .FirstOrDefault(usuarioLista => usuarioLista.Email == email && usuarioLista.Id.ToString() != id);

            return encontrado != null;
        }

        public override Usuario ObterPorId(int id)
        {
            return _lista.FirstOrDefault(usuarioLista => usuarioLista.Id == id) 
                ?? throw new Exception($"Não foi encontrado o usuário com o Id {id}");
        }

        public override void Atualizar(Usuario usuarioAtualizado)
        {
            Usuario usuarioEncontrado = ObterPorId(usuarioAtualizado.Id);

            usuarioEncontrado.Nome = usuarioAtualizado.Nome;
            usuarioEncontrado.Senha = usuarioAtualizado.Senha;
            usuarioEncontrado.Email = usuarioAtualizado.Email;
            usuarioEncontrado.DataNascimento = usuarioAtualizado.DataNascimento;
        }

        public override void Deletar(int id)
        {
            Usuario usuario = ObterPorId(id);
            _lista.RemoveAll(usuario => usuario.Id == id);
        }

        public string SenhaHash(string senha)
        {
            throw new NotImplementedException();
        }
    }
}