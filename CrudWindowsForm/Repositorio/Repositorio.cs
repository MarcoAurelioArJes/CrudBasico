using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudWindowsForm.Servicos;

namespace CrudWindowsForm.Repositorio
{
    public abstract class Repositorio<T> : IRepositorio<T> where T : class {
        protected List<T> _lista = ListaSingleton<T>.Instancia;

        public abstract void Criar(T entidade);

        public virtual List<T> Listar()
        {
            return _lista;
        }

        public abstract T ObterPorId(int id);

        public abstract void Atualizar(T entidade);

        public abstract void Deletar(int id);
    }
}
