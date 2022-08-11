using System;
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
                .NotNull()
                .WithMessage("Informe o nome do usuário");

            RuleFor(usuarioSenha => usuarioSenha.Senha)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe a senha do usuário");

            RuleFor(usuarioEmail => usuarioEmail.Email)
                .Must(email => VerificaEmail(email))
                .WithMessage("Informe um email válido")
                .Must((usuario, email) => EmailPodeSerCadastrado(usuario, email))
                .WithMessage("Email está duplicado");


            RuleFor(usuarioDataNasc => usuarioDataNasc.DataNascimento)
                .InclusiveBetween(DateTime.Parse("1930/01/01"), DateTime.Today)
                .WithMessage("Data de Nascimento inválida");
        }

        public bool EmailPodeSerCadastrado(Usuario usuario, string email)
        {
            return !_repositorioUsuario.EmailEstaDuplicado(email, usuario.Id.ToString());
        }
        
        public bool VerificaEmail(string email)
        {
            Regex regex = new Regex(@"\w+.*@\w+\.+\w+");
            return regex.IsMatch(email);
        }
    }
}
