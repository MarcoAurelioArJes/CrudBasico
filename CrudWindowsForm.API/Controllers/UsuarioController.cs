using Microsoft.AspNetCore.Mvc;
using CrudWindowsForm.Dominio.Modelo;
using CrudWindowsForm.Dominio.Interfaces;
using Microsoft.AspNetCore.Cors;

namespace CrudWindowsForm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public UsuarioController(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }

        [HttpPost]
        public void Criar([FromBody] Usuario usuario)
        {
            _repositorioUsuario.Criar(usuario);
        }

        [HttpGet]
        public List<Usuario> ObterTodos()
        {
            return _repositorioUsuario.ObterTodos();
        }

        [HttpGet("{id}")]
        public Usuario ObterPorId(int id)
        {
            return _repositorioUsuario.ObterPorId(id);
        }

        [HttpPut]
        public void Atualizar([FromForm] Usuario usuarioAtualizado)
        {
            _repositorioUsuario.Atualizar(usuarioAtualizado);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repositorioUsuario.Deletar(id);
        }
    }
}
