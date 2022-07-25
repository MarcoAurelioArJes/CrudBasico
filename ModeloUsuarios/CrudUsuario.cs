namespace ModeloUsuarios
{
    public class CrudUsuario
    {
        private static List<Usuario> listaUsuarios = new List<Usuario>();

        public int CadastrarUsuario(Usuario usuario)
        {
            listaUsuarios.Add(new Usuario()
            {
                Id = usuario.IncrementaId(),
                Nome = usuario.Nome,
                Senha = usuario.Senha,
                Email = usuario.Email,
                DataNascimento = usuario.DataNascimento,
                DataCriacao = usuario.DataCriacao
            });

            return 1;
        }

        public bool VerificaEmailExistente(string email)
        {
            foreach(Usuario lista in listaUsuarios)
            {
                if (lista.Email == email)
                {
                    return true;
                }
            }

            return false;
        }

        public List<Usuario> ListarUsuarios()
        {
            return listaUsuarios;
        }

        public Usuario ObterId(int Id)
        {
            int retorno = PercorrerLista(Id);

            if (retorno >= 0)
            {
                return listaUsuarios[retorno];
            } else
            {
                return null;
            }

        }

        public void DeletarUsuario(int Id)
        {
            int retornoLista = PercorrerLista(Id);
            if (retornoLista != -1)
            {
                CrudUsuario.listaUsuarios.RemoveAt(retornoLista);
            }
        }

        public int AtualizarUsuario(int Id, Usuario usuario)
        {
            int posicaoUsuario = PercorrerLista(Id);
            listaUsuarios[posicaoUsuario] = usuario;

            return 1;
        }

        public int PercorrerLista(int Id)
        {
            if (listaUsuarios != null)
            {
                for (int i = 0; i < listaUsuarios.Count; i++)
                {
                    if (listaUsuarios[i].Id == Id)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
    
    }
}