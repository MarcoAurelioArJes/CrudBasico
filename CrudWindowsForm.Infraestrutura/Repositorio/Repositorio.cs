using CrudWindowsForm.Dominio.Interfaces;
using CrudWindowsForm.Servicos;

namespace CrudWindowsForm.Infraestrutura.Repositorio
{
    public abstract class Repositorio<T> : IRepositorio<T> where T : class {

        protected List<T> _lista = ListaSingleton<T>.Instancia;

        public abstract void Criar(T entidade);

        public virtual List<T> ObterTodos()
        {
            return _lista;
        }

        public abstract T ObterPorId(int id);

        public abstract void Atualizar(T entidade);

        public abstract void Deletar(int id);

        public List<T> ObterPesquisa(string consulta)
        {
            throw new NotImplementedException();
        }
    }
}
