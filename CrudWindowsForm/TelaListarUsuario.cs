using CrudWindowsForm.Modelo;
using CrudWindowsForm.Repo;

namespace CrudWindowsForm
{
    public partial class TelaListarUsuario : Form
    {
        private int Id;

        CrudUsuario crudUsuario = new();

        public TelaListarUsuario()
        {
            InitializeComponent();
            ListaUsuarios();
        }

        private void AoClicarEmUsuario(object sender, EventArgs e)
        {
            try
            {
                TelaCadastroUsuario telaDeCadastro = new();
                telaDeCadastro.ShowDialog();
                ListaUsuarios();
            } catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public List<Usuario> ListaUsuarios()
        {
            dataGridUsuarios.DataSource = crudUsuario.ListarUsuarios().ToList();
            dataGridUsuarios.Columns["Senha"].Visible = false;
            return crudUsuario.ListarUsuarios();
        }

        private void AoClicarEmDeletar(object enviar, EventArgs e)
        {
            try
            {
                if (Id == decimal.Zero) throw new Exception("Nenhum usuário foi selecionado");
                DialogResult result = MessageBox.Show($"Deseja realmente excluir o usuário {crudUsuario.ObterUsuario(Id).Nome} ?\n" +
                                                      "Essa ação não poderá ser desfeita",
                                                      "Deleta usuário", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes) { 
                    crudUsuario.DeletarUsuario(Id);
                    MessageBox.Show("Usuário deletado com sucesso", "Deleta usuário");
                    ListaUsuarios();
                }
            } catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void AoClicarNaLinhaDaGrid(object sender, DataGridViewCellEventArgs e)
        {
            Id = int.Parse(dataGridUsuarios.CurrentRow.Cells[0].Value.ToString());
        }

        private void AoClicarEmEditar(object sender, EventArgs e)
        {
            try
            {
                if (Id == decimal.Zero) throw new Exception("Nenhum usuário foi selecionado");

                Usuario usuario = crudUsuario.ObterUsuario(Id);

                TelaCadastroUsuario telaCadastroUsuario = new TelaCadastroUsuario(usuario);
                telaCadastroUsuario.ShowDialog();
                ListaUsuarios();
            } catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}