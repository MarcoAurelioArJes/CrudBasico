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
            ListaUsuarios();
        }

        private void CadastrarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                TelaCadastroUsuario cadastrarUsuarios = new TelaCadastroUsuario();
                cadastrarUsuarios.ShowDialog();
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

        private void DeletarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (Id == decimal.Zero) throw new Exception("Nenhum usuário foi selecionado");

                crudUsuario.DeletarUsuario(Id);
                MessageBox.Show("Usuário deletado com sucesso", "Deleta usuário");
                ListaUsuarios();
            } catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void dataGridUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Id = int.Parse(dataGridUsuarios.CurrentRow.Cells[0].Value.ToString());
        }

        private void EditarUsuario_Click(object sender, EventArgs e)
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