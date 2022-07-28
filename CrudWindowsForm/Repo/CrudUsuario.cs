using CrudWindowsForm.Modelo;

namespace CrudWindowsForm.Repo
{
    public class CrudUsuario
    {
        private List<Usuario> listaUsuarios = ListaUsuarios.Instancia;
        public void CadastrarUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new Exception("Não foi informado nenhum usuário");
            }

            listaUsuarios.Add(new Usuario() {
                Id = ListaUsuarios.IdContador,
                Nome = usuario.Nome,
                Senha = usuario.Senha,
                Email = usuario.Email,
                DataNascimento = usuario.DataNascimento,
                DataCriacao = usuario.DataCriacao
            });
        }

        public bool EmailEstaDuplicado(string email, string id)
        {
            var encontrado = listaUsuarios
                .FirstOrDefault(usuarioLista => usuarioLista.Email == email && usuarioLista.Id.ToString() != id);

            return encontrado != null;
        }

        public List<Usuario> ListarUsuarios()
        {
            return listaUsuarios;
        }


        public Usuario ObterUsuario(int id)
        {
            return listaUsuarios
                .FirstOrDefault(usuario => usuario.Id == id) ??
                throw new Exception($"Não foi encontrado usuário com o Id {id}");
        }

        public void DeletarUsuario(int Id)
        {
            Usuario usuario = ObterUsuario(Id);
            listaUsuarios.Remove(usuario);
        }

        public void AtualizarUsuario(int id, Usuario usuario)
        {
            int indice = listaUsuarios.FindIndex(usuario => usuario.Id == id);
            
            if (indice == -1)
                throw new Exception($"Não foi encontrado usuário com o Id {id}");
            
            listaUsuarios[indice] = usuario;
        }
    }
}