using Microsoft.AspNetCore.Mvc;
using CrudWindowsForm.Dominio.Modelo;
using CrudWindowsForm.Dominio.Interfaces;
using FluentValidation.Results;
using System.Net;
using FluentValidation;
using System.Text.Json;
using CrudWindowsForm.API.Models;

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
                return StatusCode(400, new JsonResult(JsonSerializer
                                            .Deserialize<RespostaDeErroJsonModel>(err.Message)));
            }
        }

        [Route("pesquisa")]
        [HttpGet]
        public IActionResult PesquisarUsuario([FromQuery(Name = "consulta")] string consulta)
        {
            try
            {

                var retorno = _repositorioUsuario.ObterPesquisa(consulta);

                return Ok(retorno);
            }
            catch (Exception err)
            {
                return StatusCode(404, new JsonResult(err.Message));
            }
        }


        [HttpGet]
        public IActionResult ObterTodos()
        {
            try
            {

                var retorno = _repositorioUsuario.ObterTodos();

                return Ok(retorno);
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
                
                return Ok(usuario);
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
                return NoContent();
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

                return NoContent();
            } catch (Exception err)
            {
                return StatusCode(404, new JsonResult(err.Message));
            }
        }

        public void ValidaCampos(Usuario usuario)
        {
            ValidationResult results = _validacaoDeUsuario.Validate(usuario);

            if (!results.IsValid)
            {
                foreach(ValidationFailure result in results.Errors)
                {
                    var respostaJson = new RespostaDeErroJsonModel
                    {
                        NomePropriedade = result.PropertyName,
                        MensagemErro = result.ErrorMessage
                    };

                    string jsonString = JsonSerializer.Serialize(respostaJson);
                    throw new Exception(jsonString);
                }
            }
        }
    }
}
