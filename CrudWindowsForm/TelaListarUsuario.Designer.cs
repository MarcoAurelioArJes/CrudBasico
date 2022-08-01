using System;

namespace CrudWindowsForm
{
    partial class TelaListarUsuario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CadastrarUsuario = new System.Windows.Forms.Button();
            this.EditarUsuario = new System.Windows.Forms.Button();
            this.DeletarUsuario = new System.Windows.Forms.Button();
            this.dataGridUsuarios = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // CadastrarUsuario
            // 
            this.CadastrarUsuario.Location = new System.Drawing.Point(211, 369);
            this.CadastrarUsuario.Name = "CadastrarUsuario";
            this.CadastrarUsuario.Size = new System.Drawing.Size(111, 32);
            this.CadastrarUsuario.TabIndex = 1;
            this.CadastrarUsuario.Text = "Novo";
            this.CadastrarUsuario.UseVisualStyleBackColor = true;
            this.CadastrarUsuario.Click += new System.EventHandler(this.AoClicarEmNovo);
            // 
            // EditarUsuario
            // 
            this.EditarUsuario.Location = new System.Drawing.Point(328, 369);
            this.EditarUsuario.Name = "EditarUsuario";
            this.EditarUsuario.Size = new System.Drawing.Size(111, 32);
            this.EditarUsuario.TabIndex = 2;
            this.EditarUsuario.Text = "Editar";
            this.EditarUsuario.UseVisualStyleBackColor = true;
            this.EditarUsuario.Click += new System.EventHandler(this.AoClicarEmEditar);
            // 
            // DeletarUsuario
            // 
            this.DeletarUsuario.Location = new System.Drawing.Point(445, 369);
            this.DeletarUsuario.Name = "DeletarUsuario";
            this.DeletarUsuario.Size = new System.Drawing.Size(111, 32);
            this.DeletarUsuario.TabIndex = 3;
            this.DeletarUsuario.Text = "Deletar";
            this.DeletarUsuario.UseVisualStyleBackColor = true;
            this.DeletarUsuario.Click += new System.EventHandler(this.AoClicarEmDeletar);
            // 
            // dataGridUsuarios
            // 
            this.dataGridUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridUsuarios.Location = new System.Drawing.Point(12, 12);
            this.dataGridUsuarios.MultiSelect = false;
            this.dataGridUsuarios.Name = "dataGridUsuarios";
            this.dataGridUsuarios.RowTemplate.Height = 25;
            this.dataGridUsuarios.Size = new System.Drawing.Size(543, 351);
            this.dataGridUsuarios.TabIndex = 4;
            this.dataGridUsuarios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AoClicarNaLinhaDaGrid);
            // 
            // TelaListarUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 420);
            this.Controls.Add(this.dataGridUsuarios);
            this.Controls.Add(this.DeletarUsuario);
            this.Controls.Add(this.EditarUsuario);
            this.Controls.Add(this.CadastrarUsuario);
            this.Name = "TelaListarUsuario";
            this.Text = "Lista de Usuarios";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUsuarios)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Button CadastrarUsuario;
        private Button EditarUsuario;
        private Button DeletarUsuario;
        private DataGridView dataGridUsuarios;
    }
}