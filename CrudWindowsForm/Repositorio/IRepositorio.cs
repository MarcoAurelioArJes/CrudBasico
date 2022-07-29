using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudWindowsForm.Repositorio
{
    public interface IRepositorio<T> where T : class
    {
        public void Criar(T entidade);
        public List<T> Listar();
        public void Atualizar(T entidade);
        public void Deletar(T entidade);
    }
}
