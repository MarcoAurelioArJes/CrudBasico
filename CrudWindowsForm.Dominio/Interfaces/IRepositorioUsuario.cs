using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudWindowsForm.Dominio.Modelo;

namespace CrudWindowsForm.Dominio.Interfaces
{
    public interface IRepositorioUsuario : IRepositorio<Usuario>
    {
        public bool EmailEstaDuplicado(string email, string id);
    }
}
