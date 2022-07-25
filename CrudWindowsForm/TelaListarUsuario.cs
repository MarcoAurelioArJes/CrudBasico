using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModeloUsuarios;

namespace CrudWindowsForm
{
    public partial class TelaListarUsuario : Form
    {
        private int Id;

        CrudUsuario crudUsuario = new CrudUsuario();
        public TelaListarUsuario()
        {
            InitializeComponent();
        }

        private void CadastrarUsuario_Click(object sender, EventArgs e)
        {
            TelaCadastroUsuario cadastrarUsuarios = new TelaCadastroUsuario();
            cadastrarUsuarios.ShowDialog();
            ListaUsuarios();
        }

        public List<Usuario> ListaUsuarios()
        {
            dataGridUsuarios.DataSource = crudUsuario.ListarUsuarios().ToList();
            return crudUsuario.ListarUsuarios();
        }

        private void DeletarUsuario_Click(object sender, EventArgs e)
        {
            Usuario usuario = crudUsuario.ObterId(Id);
            if (usuario != null)
            {
                crudUsuario.DeletarUsuario(Id);
                MessageBox.Show("Usuário deletado com sucesso", "Deleta usuário");
                ListaUsuarios();
            } else
            {
                MessageBox.Show("Nenhum usuário selecionado", "Seleciona usuário");
            }
        }

        private void dataGridUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Id = int.Parse(dataGridUsuarios.CurrentRow.Cells[0].Value.ToString());
        }

        private void EditarUsuario_Click(object sender, EventArgs e)
        {
            Usuario usuario = crudUsuario.ObterId(Id);

            if (usuario != null)
            {
                TelaCadastroUsuario telaCadastroUsuario = new TelaCadastroUsuario(true);

                telaCadastroUsuario.PopularCampos(usuario);
                telaCadastroUsuario.ShowDialog();
                ListaUsuarios();
            } else if (usuario == null)
            {
                MessageBox.Show("Nenhum usuário cadastrado", "Não existe usuário");
            } else
            {
                MessageBox.Show("Nenhum usuário selecionado", "Seleciona usuário");
            }
        }
    }
}
