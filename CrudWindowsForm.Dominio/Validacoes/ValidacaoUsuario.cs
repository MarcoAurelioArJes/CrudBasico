using System;
using FluentValidation;
using CrudWindowsForm.Dominio.Modelo;

namespace CrudWindowsForm.InterfaceUsuario.ValidacoesFormularios
{
    public class ValidacaoUsuario : AbstractValidator<Usuario>
    {
        public ValidacaoUsuario()
        {
            RuleFor(usuarioNome => usuarioNome.Nome)
                .NotEmpty().NotNull().WithMessage("Informe o nome do usuário");

            RuleFor(usuarioSenha => usuarioSenha.Senha)
                .NotEmpty().NotNull().WithMessage("Informe a senha do usuário");

            RuleFor(usuarioEmail => usuarioEmail.Email)
                .NotEmpty().NotNull().WithMessage("Informe o email do usuário");

            RuleFor(usuarioDataNasc => usuarioDataNasc.DataNascimento)
                .GreaterThan(DateTime.Today)
                .LessThan(new DateTime(1930, 01, 01));
        }
    }
}
