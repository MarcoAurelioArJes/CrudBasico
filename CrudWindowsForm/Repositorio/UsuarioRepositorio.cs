using CrudWindowsForm.Modelo;
using CrudWindowsForm.Servicos;

namespace CrudWindowsForm.Repositorio
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

        public override void Atualizar(Usuario usuarioAtualizado)
        {
            Usuario usuarioEncontrado = _lista.FirstOrDefault(usuario => usuario == usuarioAtualizado);

            if (usuarioEncontrado == null) throw new Exception($"Não foi encontrado usuário com o Id {usuarioAtualizado.Id}");

            usuarioEncontrado.Nome = usuarioAtualizado.Nome;
            usuarioEncontrado.Senha = usuarioAtualizado.Senha;
            usuarioEncontrado.Email = usuarioAtualizado.Email;
            usuarioEncontrado.DataNascimento = usuarioAtualizado.DataNascimento;
        }

        //private List<Usuario> listaUsuarios = ListaSingleton<Usuario>.Instancia;

        //public void CadastrarUsuario(Usuario usuario)
        //{
        //    if (usuario == null)
        //    {
        //        throw new Exception("Não foi informado nenhum usuário");
        //    }

        //    listaUsuarios.Add(new Usuario() {
        //        Id = ListaSingleton<Usuario>.IdContador,
        //        Nome = usuario.Nome,
        //        Senha = usuario.Senha,
        //        Email = usuario.Email,
        //        DataNascimento = usuario.DataNascimento,
        //        DataCriacao = usuario.DataCriacao
        //    });
        //}
        
        //public List<Usuario> ListarUsuarios()
        //{
        //    return _lista;
        //}
        

        //public override Usuario ObterPorId(Usuario usuario)
        //{
        //    return _lista
        //        .FirstOrDefault(usuarioLista => usuarioLista.Id == usuario.Id) ??
        //        throw new Exception($"Não foi encontrado usuário com o Id {usuario.Id}");
        //}

        //public void DeletarUsuario(int Id)
        //{
        //    Usuario usuario = ObterUsuario(Id);
        //    _lista.Remove(usuario);
        //}
    }
}