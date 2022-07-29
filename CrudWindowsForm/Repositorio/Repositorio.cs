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
        
        public virtual void Criar(T entidade)
        {
            if (entidade == null)
                throw new NotImplementedException();

            _lista.Add(entidade);
        }

        public virtual List<T> Listar()
        {
            return _lista;
        }

        public virtual void Atualizar(T entidade)
        {

        }

        public virtual void Deletar(T entidade)
        {
            if (entidade == null)
                throw new NotImplementedException();

            _lista.Remove(entidade);
        }
    }
}
