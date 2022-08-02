﻿using CrudWindowsForm.Modelo;
using CrudWindowsForm.Repositorio;

namespace CrudWindowsForm
{
    public partial class TelaListarUsuario : Form
    {
        private Usuario _usuario;

        UsuarioRepositorioComSql usuarioRepositorioComSql = new();

        public TelaListarUsuario()
        {
            InitializeComponent();
            ListaUsuarios();
        }

        private void AoClicarEmNovo(object sender, EventArgs e)
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
            UsuarioRepositorioComSql usuarioRepositorioComSql = new();
            dataGridUsuarios.DataSource = usuarioRepositorioComSql.ObterTodos().ToList();
            dataGridUsuarios.Columns["Senha"].Visible = false;
            return usuarioRepositorioComSql.ObterTodos();
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
                    usuarioRepositorioComSql.Deletar(_usuario.Id);
                    MessageBox.Show("Usuário deletado com sucesso", "Deleta usuário");
                    _usuario = null;
                    ListaUsuarios();
                }
            } catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void AoClicarNaLinhaDaGrid(object sender, DataGridViewCellEventArgs e)
        {
            _usuario = (Usuario)dataGridUsuarios.CurrentRow.DataBoundItem;
        }

        private void AoClicarEmEditar(object sender, EventArgs e)
        {
            try
            {
                if (_usuario == null) throw new Exception("Nenhum usuário foi selecionado");

                TelaCadastroUsuario telaCadastroUsuario = new TelaCadastroUsuario(_usuario);
                telaCadastroUsuario.ShowDialog();
                ListaUsuarios();
            } catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}