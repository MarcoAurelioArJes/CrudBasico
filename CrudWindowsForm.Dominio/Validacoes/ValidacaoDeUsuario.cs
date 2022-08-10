using System;
using FluentValidation;
using CrudWindowsForm.Dominio.Modelo;
using CrudWindowsForm.Dominio.Interfaces;

namespace CrudWindowsForm.Dominio.Validacoes
{
    public class ValidacaoDeUsuario : AbstractValidator<Usuario>, IValidacaoDeUsuario
    {
        public ValidacaoDeUsuario()
        {
            RuleFor(usuarioNome => usuarioNome.Nome)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe o nome do usuário");

            RuleFor(usuarioSenha => usuarioSenha.Senha)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe a senha do usuário");

            RuleFor(usuarioEmail => usuarioEmail.Email)
                .EmailAddress()
                .WithMessage("Informe um email válido");
                

            RuleFor(usuarioDataNasc => usuarioDataNasc.DataNascimento)
                .InclusiveBetween(DateTime.Parse("1930/01/01"), DateTime.Today)
                .WithMessage("Data de Nascimento inválida");
        }
    }
}
