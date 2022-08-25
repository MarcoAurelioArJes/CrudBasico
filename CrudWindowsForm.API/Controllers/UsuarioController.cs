using Microsoft.AspNetCore.Mvc;
using CrudWindowsForm.Dominio.Modelo;
using CrudWindowsForm.Dominio.Interfaces;
using FluentValidation.Results;
using System.Net;

namespace CrudWindowsForm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IValidacaoDeUsuario _validacaoDeUsuario;

        public UsuarioController(IRepositorioUsuario repositorioUsuario, IValidacaoDeUsuario validacaoDeUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
            _validacaoDeUsuario = validacaoDeUsuario;
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Usuario usuario)
        {
            try
            {
                ValidaCampos(usuario);

                usuario.DataCriacao = DateTime.Today;
                _repositorioUsuario.Criar(usuario);
                return StatusCode(201, new JsonResult(""));
            } catch (Exception err)
            {
                return StatusCode(400, new JsonResult(err.Message));
            }
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            try
            {
                var retorno = _repositorioUsuario.ObterTodos();

                return Ok(retorno, new JsonResult(""));
            } catch (Exception err)
            {
                return StatusCode(404, new JsonResult(err.Message));
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                var usuario = _repositorioUsuario.ObterPorId(id);
                
                return Ok(usuario, new JsonResult(""));
            } catch (Exception err)
            {
                return StatusCode(404, new JsonResult(err.Message));
            }

        }

        [HttpPut]
        public IActionResult Atualizar([FromBody] Usuario usuarioAtualizado)
        {
            try
            {
                ValidaCampos(usuarioAtualizado);

                _repositorioUsuario.Atualizar(usuarioAtualizado);
                return StatusCode(204, new JsonResult(""));
            } catch (Exception err)
            {
                return StatusCode(400, new JsonResult(err.Message));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                ObterPorId(id);
                _repositorioUsuario.Deletar(id);

                return StatusCode(204, new JsonResult(""));
            } catch (Exception err)
            {
                return StatusCode(404, new JsonResult(err.Message));
            }
        }

        public bool ValidaCampos(Usuario usuario)
        {
            var results = _validacaoDeUsuario.Validate(usuario);

            bool validaTodos = false;

            if (!results.IsValid)
            {
                foreach (ValidationFailure erros in results.Errors)
                {
                   throw new Exception(erros.ErrorMessage);
                }
                validaTodos = true;
            }

            return validaTodos;
        }
    }
}
