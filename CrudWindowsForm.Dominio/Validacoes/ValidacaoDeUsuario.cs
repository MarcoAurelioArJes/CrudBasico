using FluentValidation;
using CrudWindowsForm.Dominio.Modelo;
using CrudWindowsForm.Dominio.Interfaces;
using System.Text.RegularExpressions;

namespace CrudWindowsForm.Dominio.Validacoes
{
    public class ValidacaoDeUsuario : AbstractValidator<Usuario>, IValidacaoDeUsuario
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public ValidacaoDeUsuario(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;

            RuleFor(usuarioNome => usuarioNome.Nome)
                .NotEmpty()
                .WithMessage("Informe o {PropertyName} do usuário");

            RuleFor(usuarioSenha => usuarioSenha.Senha)
                .NotEmpty()
                .MinimumLength(8)
                .WithMessage("A {PropertyName} do usuário é obrigatória \ne precisa ter no mínimo 8 caracteres");

            RuleFor(usuarioEmail => usuarioEmail.Email)
                .NotEmpty()
                .WithMessage("Informe um {PropertyName}")
                .Must(email => VerificaEmail(email))
                .WithMessage("Um {PropertyName} precisa ter um @ e .com para ser válido")
                .Must((usuario, email) => EmailPodeSerCadastrado(usuario, email))
                .WithMessage("Já existe outro usuário com esse {PropertyName}");


            RuleFor(usuarioDataNasc => usuarioDataNasc.DataNascimento)
                .InclusiveBetween(DateTime.Parse("1930/01/01"), DateTime.Today)
                .WithMessage("{PropertyName} precisa\n" +
                             "ser maior que 31/12/1929\n" +
                             "e menor que a data atual: \n" +
                             $"{DateTime.Today:dd/MM/yyyy}");
        }

        public bool EmailPodeSerCadastrado(Usuario usuario, string email)
        {
            return !_repositorioUsuario.EmailEstaDuplicado(email, usuario.Id.ToString());
        }
        
        public static bool VerificaEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) == false)
            {
                var regex = new Regex(@"\w+.*@\w+\.+\w+");
                return regex.IsMatch(email);
            }

            return true;
        }
    }
}
