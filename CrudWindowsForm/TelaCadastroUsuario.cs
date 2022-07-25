using ModeloUsuarios;

namespace CrudWindowsForm
{
    public partial class TelaCadastroUsuario : Form
    {
        Usuario usuario;

        public TelaCadastroUsuario()
        {
            InitializeComponent();

            txtSenhaUsuario.PasswordChar = '*';

            dataCriacaoUsuario.Value = DateTime.Today;
        }

        public TelaCadastroUsuario(bool editar) {
            InitializeComponent();

            dataCriacaoUsuario.Value = DateTime.Today;

            if (editar)
            {
                BtnCadastrar.Text = "Atualizar";
            }
        }

        private void BtnCadastrar_Click(object sender, EventArgs e)
        {
            if (ValidaCampos(txtNomeUsuario.Text, txtSenhaUsuario.Text, txtEmailUsuario.Text) == false)
            {
                if (BtnCadastrar.Text != "Cadastrar")
                {
                    RealizaAtualizacaoUsuario();
                }
                else
                {
                    RealizaCadastro();
                }

                this.Close();
            }
        }

        public void PopularCampos(Usuario usuario)
        {
            txtId.Text = usuario.Id.ToString();
            txtNomeUsuario.Text = usuario.Nome;
            txtEmailUsuario.Text = usuario.Email;
            txtSenhaUsuario.Text = usuario.Senha;
            dataNascimentoUsuario.Text = usuario.DataNascimento.ToString();
            dataNascimentoUsuario.Text = usuario.DataNascimento.ToString();

            this.usuario = usuario;
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool ValidaCampos(string nome, string senha, string email)
        {
            string mensagem = "";

            if (nome.Length <= 0)
            {
                mensagem += "Campo nome vazio \n";
            } 
            
            if (senha.Length <= 0)
            {
                mensagem += "Campo senha vazio \n";
            } 
            if (email.Length <= 0)
            {
                mensagem += "Campo email vazio\n";
            }

            if (mensagem.Length > 0)
            {
                MessageBox.Show(mensagem, "Campo vazio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;            
            } else
            {
                return false;
            }
        }

        public void RealizaCadastro()
        {
            BtnCadastrar.Visible = true;
            CrudUsuario cadastrarUsuarios = new CrudUsuario();
            Usuario usuario = new Usuario();
            usuario.Nome = txtNomeUsuario.Text;
            usuario.Senha = txtSenhaUsuario.Text;
            usuario.Email = txtEmailUsuario.Text;
            usuario.DataNascimento = dataNascimentoUsuario.Value.Date;
            usuario.DataCriacao = dataCriacaoUsuario.Value.Date;

            int retorno = cadastrarUsuarios.CadastrarUsuario(usuario);

            if (retorno == 1)
            {
                MessageBox.Show("Usuário cadastrado com sucesso", "Cadastro usuário");
            }
            else
            {
                MessageBox.Show("Usuário não cadastrado", "Erro");
            }
        }

        public void RealizaAtualizacaoUsuario()
        {
            CrudUsuario crudUsuario = new CrudUsuario();

            usuario.Nome = txtNomeUsuario.Text;
            usuario.Email = txtEmailUsuario.Text;
            usuario.Senha = txtSenhaUsuario.Text;
            usuario.DataNascimento = dataNascimentoUsuario.Value.Date;
            usuario.DataNascimento = dataNascimentoUsuario.Value.Date;

            int retorno = crudUsuario.AtualizarUsuario(usuario.Id, usuario);

            if (retorno == 1)
            {
                MessageBox.Show("Informações atualizadas", "Editar usuário");
            }
            else
            {
                MessageBox.Show("Informações não foram atualizadas", "Erro");
            }
        }
    }
}
