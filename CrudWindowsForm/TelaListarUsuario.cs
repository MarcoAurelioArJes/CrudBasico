using CrudWindowsForm.Dominio.Modelo;
using CrudWindowsForm.Dominio.Interfaces;
using CrudWindowsForm.Dominio.Validacoes;

namespace CrudWindowsForm
{
    public partial class TelaListarUsuario : Form
    {
        private Usuario? _usuario;

        private readonly IRepositorioUsuario? _usuarioRepositorioComSql;
        private readonly IValidacaoDeUsuario? _validacaoDeUsuario;
        public TelaListarUsuario(IRepositorioUsuario usuarioRepositorioComSql, IValidacaoDeUsuario validacaoDeUsuario)
        {
            InitializeComponent();

            _usuarioRepositorioComSql = usuarioRepositorioComSql;
            _validacaoDeUsuario = validacaoDeUsuario;

            ListaUsuarios();
        }

        private void AoClicarEmNovo(object sender, EventArgs e)
        {
            try
            {
                var telaDeCadastro = new TelaCadastroUsuario(_usuarioRepositorioComSql, _validacaoDeUsuario);
                telaDeCadastro.ShowDialog();
                ListaUsuarios();
            } catch (Exception error)
            {
                var msg = $"{error.Message}{error.InnerException?.Message}";
                MessageBox.Show(msg);
            }
        }

        public List<Usuario> ListaUsuarios()
        {
            dataGridUsuarios.DataSource = _usuarioRepositorioComSql.ObterTodos();
            dataGridUsuarios.Columns["Senha"].Visible = false;
            return _usuarioRepositorioComSql.ObterTodos();
        }

        private void AoClicarEmDeletar(object enviar, EventArgs e)
        {
            try
            {
                if (_usuario == null) throw new Exception("Nenhum usuário foi selecionado");

                DialogResult result = MessageBox.Show($"Deseja realmente excluir o usuário {_usuario.Nome} ?\n" +
                                                      "Essa ação não poderá ser desfeita",
                                                      "Deleta usuário", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes) {
                    _usuarioRepositorioComSql.Deletar(_usuario.Id);
                    MessageBox.Show("Usuário deletado com sucesso", "Deleta usuário");
                    _usuario = null;
                    ListaUsuarios();
                }
            } catch (Exception error)
            {
                var msg = $"{error.Message}{error.InnerException?.Message}";
                MessageBox.Show(msg);
            }
        }

        private void AoClicarNaLinhaDaGrid(object sender, DataGridViewCellEventArgs e)
        {
            string? id = dataGridUsuarios.CurrentRow.Cells["Id"].Value.ToString();
            if (id == null)
                throw new Exception("Nenhum usuário selecionado");

            _usuario = _usuarioRepositorioComSql.ObterPorId(int.Parse(id));
        }

        private void AoClicarEmEditar(object sender, EventArgs e)
        {
            try
            {
                if (_usuario == null) throw new Exception("Nenhum usuário foi selecionado");

                TelaCadastroUsuario telaCadastroUsuario = new(_usuario, _usuarioRepositorioComSql, _validacaoDeUsuario);
                telaCadastroUsuario.ShowDialog();
                ListaUsuarios();
            } catch (Exception error)
            {
                var msg = $"{error.Message}{error.InnerException?.Message}";
                MessageBox.Show(msg);
            }
        }
    }
}