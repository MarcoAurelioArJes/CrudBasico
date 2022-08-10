using System.Text.RegularExpressions;
using CrudWindowsForm.Dominio.Modelo;
using CrudWindowsForm.Dominio.Interfaces;
using CrudWindowsForm.Dominio.Validacoes;
using FluentValidation.Results;

namespace CrudWindowsForm
{
    public partial class TelaCadastroUsuario : Form
    {
        private Usuario _usuario;
        private readonly IRepositorioUsuario? _repositorioUsuario;
        private readonly IValidacaoDeUsuario? _validacaoDeUsuario;
        public TelaCadastroUsuario(IRepositorioUsuario repositorioUsuario, IValidacaoDeUsuario validacaoDeUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
            _validacaoDeUsuario = validacaoDeUsuario;

            InitializeComponent();
            //txtSenhaUsuario.PasswordChar = '*';
            dataCriacaoUsuario.Value = DateTime.Today;
        }

        public TelaCadastroUsuario(Usuario usuario, IRepositorioUsuario repositorioUsuario, IValidacaoDeUsuario validacaoDeUsuario)
        : this(repositorioUsuario, validacaoDeUsuario)
        {
            PopularCampos(usuario);
            BtnCadastrar.Text = "Atualizar";
        }

        private void AoClicarEmCadastrar(object sender, EventArgs e)
        {
            try
            {
                bool emailExiste = _repositorioUsuario.EmailEstaDuplicado(txtEmailUsuario.Text, txtId.Text);
                if (emailExiste) throw new Exception("Email já existe escolha outro");

                if (this._usuario != null) RealizaAtualizacaoUsuario();
                else RealizaCadastro();

                this.Close();
            }
            catch (Exception error)
            {
                var msg = $"{error.Message}{error.InnerException?.Message}";
                MessageBox.Show(msg);
            }
        }

        public void PopularCampos(Usuario usuario)
        {
            txtId.Text = usuario.Id.ToString();
            txtNomeUsuario.Text = usuario.Nome;
            txtEmailUsuario.Text = usuario.Email;
            txtSenhaUsuario.Text = usuario.Senha;
            dataNascimentoUsuario.Text = usuario.DataNascimento.ToString();
            dataCriacaoUsuario.Text = usuario.DataCriacao.ToString();

            this._usuario = usuario;
        }

        private void AoClicarEmCancelar(object sender, EventArgs e)
        {
            this.Close();
        }

        public void MensagensLabel(Label campo, string mensagem)
        {
            campo.Visible = true;
            campo.Text = mensagem;
        }

        public bool ValidaCampos(Usuario usuario)
        {
            var results = _validacaoDeUsuario.Validate(usuario);

            bool validaTodos = false;
            string valorPadrao = string.Empty;

            avisoNome.Text = valorPadrao;
            avisoSenha.Text = valorPadrao;
            avisoEmail.Text = valorPadrao;
            avisoDataNascimento.Text = valorPadrao;

            if (!results.IsValid)
            {
                foreach (ValidationFailure erros in results.Errors)
                {
                    if (erros.PropertyName == "Nome")
                        MensagensLabel(avisoNome, erros.ErrorMessage);

                    if (erros.PropertyName == "Senha")
                        MensagensLabel(avisoSenha, erros.ErrorMessage);

                    if (erros.PropertyName == "Email")
                        MensagensLabel(avisoEmail, erros.ErrorMessage);

                    if (erros.PropertyName == "DataNascimento")
                        MensagensLabel(avisoDataNascimento, erros.ErrorMessage);
                }
                validaTodos = true;
            }

            return validaTodos;
        }

        public Usuario InsereValoresCampos(Usuario usuario)
        {
            usuario.Nome = txtNomeUsuario.Text;
            usuario.Senha = txtSenhaUsuario.Text;
            usuario.Email = txtEmailUsuario.Text.ToLower();
            usuario.DataNascimento = dataNascimentoUsuario.Value.Date;
            usuario.DataCriacao = dataCriacaoUsuario.Value.Date;
            if (dataNascimentoUsuario.Checked == false) usuario.DataNascimento = null;

            return usuario;
        }
        public void RealizaCadastro()
        {
            var usuario = new Usuario();
            var novoUsuario = InsereValoresCampos(usuario);

            if (ValidaCampos(novoUsuario))
                throw new Exception("Não foi possível cadastrar o usuário");

            _repositorioUsuario.Criar(novoUsuario);

            MessageBox.Show("Usuário cadastrado com sucesso", "Cadastro usuário");
        }

        public void RealizaAtualizacaoUsuario()
        {
            if (ValidaCampos(this._usuario))
                throw new Exception("Não foi possível editar o usuário");

            Usuario usuario = InsereValoresCampos(this._usuario);
            _repositorioUsuario.Atualizar(usuario);

            MessageBox.Show("Informações atualizadas", "Editar usuário");
        }
    }
}
