using CrudWindowsForm.Dominio.Modelo;

namespace CrudWindowsForm.Dominio.Interfaces
{
    public interface IRepositorioUsuario : IRepositorio<Usuario>
    {
        public bool EmailEstaDuplicado(string email, string id);
    }
}
