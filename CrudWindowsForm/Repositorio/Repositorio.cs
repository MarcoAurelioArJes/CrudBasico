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
            _lista.Add(entidade);
        }

        public virtual List<T> Listar()
        {
            return _lista;
        }

        public virtual T ObterPorId(int id) { 
            return _lista[id];
        }

        public virtual void Atualizar(T entidade) { }

        public virtual void Deletar(int id) { }
    }
}
