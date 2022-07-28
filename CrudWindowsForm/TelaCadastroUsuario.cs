using System.Text.RegularExpressions;
using CrudWindowsForm.Modelo;
using CrudWindowsForm.Repo;

namespace CrudWindowsForm
{
    public partial class TelaCadastroUsuario : Form
    {
        private Usuario _usuario;

        public TelaCadastroUsuario()
        {
            InitializeComponent();
            txtSenhaUsuario.PasswordChar = '*';
            dataCriacaoUsuario.Value = DateTime.Today;
        }

        public TelaCadastroUsuario(Usuario usuario) : this()
        {
            PopularCampos(usuario);
            BtnCadastrar.Text = "Atualizar";
        }

        private void AoClicarEmCadastrar(object sender, EventArgs e)
        {
            try
            {
                CrudUsuario crudUsuario = new CrudUsuario();

                bool validaCampos = ValidaCampos(txtNomeUsuario.Text,
                                            txtSenhaUsuario.Text,
                                            txtEmailUsuario.Text,
                                            dataNascimentoUsuario);
                if (validaCampos) return;

                bool emailExiste = crudUsuario.EmailEstaDuplicado(txtEmailUsuario.Text, txtId.Text);
                if (emailExiste) throw new Exception("Email já existe escolha outro");

                if (this._usuario != null) RealizaAtualizacaoUsuario();
                else RealizaCadastro();

                this.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
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

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool MensagemErros(Label campo, string mensagem)
        {
            campo.Visible = true;
            campo.Text = mensagem;
            return true;
        }

        public bool ValidaCampos(string nome, string senha, string email, DateTimePicker dataNascimento)
        {
            bool validaTodos = false;
            string valorPadrao = string.Empty;

            avisoNome.Text = valorPadrao;
            avisoSenha.Text = valorPadrao;
            avisoEmail.Text = valorPadrao;
            avisoDataNascimento.Text = valorPadrao;

            Regex regex = new Regex(@"\w+.*@\w+\.com");
            if (email.Length <= 0) validaTodos = MensagemErros(avisoEmail, "Informe um email");
            else if (!regex.IsMatch(email)) validaTodos = MensagemErros(avisoEmail, "Por favor insira um email válido");

            if (senha.Length <= 0) validaTodos = MensagemErros(avisoSenha, "Informe a senha");

            if (nome.Length <= 0) validaTodos = MensagemErros(avisoNome, "Informe um nome");

            if ((dataNascimento.Value > DateTime.Today || dataNascimento.Value < new DateTime(1930, 01, 01)) && dataNascimento.Checked)
                validaTodos = MensagemErros(avisoDataNascimento, "Data de nascimento inválida");

            return validaTodos;
        }

        public Usuario InsereValoresCampos(Usuario usuario)
        {
            usuario.Nome = txtNomeUsuario.Text;
            usuario.Senha = txtSenhaUsuario.Text;
            usuario.Email = txtEmailUsuario.Text;
            usuario.DataNascimento = dataNascimentoUsuario.Value.Date;
            usuario.DataCriacao = dataCriacaoUsuario.Value.Date;
            if (dataNascimentoUsuario.Checked == false) usuario.DataNascimento = null;

            return usuario;
        }
        public void RealizaCadastro()
        {
            CrudUsuario cadastrarUsuarios = new CrudUsuario();
            Usuario usuario = new Usuario();
            cadastrarUsuarios.CadastrarUsuario(InsereValoresCampos(usuario));
            MessageBox.Show("Usuário cadastrado com sucesso", "Cadastro usuário");
        }

        public void RealizaAtualizacaoUsuario()
        {
            CrudUsuario crudUsuario = new CrudUsuario();

            Usuario usuario = InsereValoresCampos(this._usuario);
            crudUsuario.AtualizarUsuario(usuario.Id, usuario);

            MessageBox.Show("Informações atualizadas", "Editar usuário");
        }
    }
}
