namespace CrudWindowsForm
{
    partial class TelaCadastroUsuario
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
            this.dataNascimentoUsuario = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSenhaUsuario = new System.Windows.Forms.TextBox();
            this.txtEmailUsuario = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dataCriacaoUsuario = new System.Windows.Forms.DateTimePicker();
            this.BtnCadastrar = new System.Windows.Forms.Button();
            this.Cancelar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtNomeUsuario = new System.Windows.Forms.TextBox();
            this.avisoNome = new System.Windows.Forms.Label();
            this.avisoSenha = new System.Windows.Forms.Label();
            this.avisoEmail = new System.Windows.Forms.Label();
            this.avisoDataNascimento = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dataNascimentoUsuario
            // 
            this.dataNascimentoUsuario.CustomFormat = "";
            this.dataNascimentoUsuario.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dataNascimentoUsuario.Location = new System.Drawing.Point(149, 289);
            this.dataNascimentoUsuario.MaxDate = new System.DateTime(2022, 12, 31, 0, 0, 0, 0);
            this.dataNascimentoUsuario.MinDate = new System.DateTime(1930, 1, 1, 0, 0, 0, 0);
            this.dataNascimentoUsuario.Name = "dataNascimentoUsuario";
            this.dataNascimentoUsuario.ShowCheckBox = true;
            this.dataNascimentoUsuario.Size = new System.Drawing.Size(100, 23);
            this.dataNascimentoUsuario.TabIndex = 9;
            this.dataNascimentoUsuario.Value = new System.DateTime(2022, 1, 27, 13, 49, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(17, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "NOME";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(17, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "SENHA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(17, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "E-MAIL";
            // 
            // txtSenhaUsuario
            // 
            this.txtSenhaUsuario.Location = new System.Drawing.Point(149, 162);
            this.txtSenhaUsuario.Name = "txtSenhaUsuario";
            this.txtSenhaUsuario.Size = new System.Drawing.Size(288, 23);
            this.txtSenhaUsuario.TabIndex = 6;
            // 
            // txtEmailUsuario
            // 
            this.txtEmailUsuario.Location = new System.Drawing.Point(149, 226);
            this.txtEmailUsuario.Name = "txtEmailUsuario";
            this.txtEmailUsuario.Size = new System.Drawing.Size(288, 23);
            this.txtEmailUsuario.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(17, 289);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "Data Nascimento";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(17, 334);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 19);
            this.label6.TabIndex = 10;
            this.label6.Text = "Data Criação";
            // 
            // dataCriacaoUsuario
            // 
            this.dataCriacaoUsuario.Enabled = false;
            this.dataCriacaoUsuario.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dataCriacaoUsuario.Location = new System.Drawing.Point(149, 334);
            this.dataCriacaoUsuario.Name = "dataCriacaoUsuario";
            this.dataCriacaoUsuario.Size = new System.Drawing.Size(100, 23);
            this.dataCriacaoUsuario.TabIndex = 11;
            // 
            // BtnCadastrar
            // 
            this.BtnCadastrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnCadastrar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnCadastrar.Location = new System.Drawing.Point(17, 400);
            this.BtnCadastrar.Name = "BtnCadastrar";
            this.BtnCadastrar.Size = new System.Drawing.Size(113, 40);
            this.BtnCadastrar.TabIndex = 12;
            this.BtnCadastrar.Text = "Cadastrar";
            this.BtnCadastrar.UseVisualStyleBackColor = true;
            this.BtnCadastrar.Click += new System.EventHandler(this.AoClicarEmCadastrar);
            // 
            // Cancelar
            // 
            this.Cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancelar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Cancelar.Location = new System.Drawing.Point(136, 400);
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.Size = new System.Drawing.Size(113, 40);
            this.Cancelar.TabIndex = 13;
            this.Cancelar.Text = "Cancelar";
            this.Cancelar.UseVisualStyleBackColor = true;
            this.Cancelar.Click += new System.EventHandler(this.AoClicarEmCancelar);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(17, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 19);
            this.label1.TabIndex = 15;
            this.label1.Text = "ID";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(149, 41);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(65, 23);
            this.txtId.TabIndex = 16;
            // 
            // txtNomeUsuario
            // 
            this.txtNomeUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNomeUsuario.Location = new System.Drawing.Point(149, 98);
            this.txtNomeUsuario.Name = "txtNomeUsuario";
            this.txtNomeUsuario.Size = new System.Drawing.Size(288, 23);
            this.txtNomeUsuario.TabIndex = 5;
            // 
            // avisoNome
            // 
            this.avisoNome.AutoSize = true;
            this.avisoNome.ForeColor = System.Drawing.Color.Red;
            this.avisoNome.Location = new System.Drawing.Point(149, 123);
            this.avisoNome.Name = "avisoNome";
            this.avisoNome.Size = new System.Drawing.Size(38, 15);
            this.avisoNome.TabIndex = 17;
            this.avisoNome.Text = "label7";
            this.avisoNome.Visible = false;
            // 
            // avisoSenha
            // 
            this.avisoSenha.AutoSize = true;
            this.avisoSenha.ForeColor = System.Drawing.Color.Red;
            this.avisoSenha.Location = new System.Drawing.Point(149, 188);
            this.avisoSenha.Name = "avisoSenha";
            this.avisoSenha.Size = new System.Drawing.Size(38, 15);
            this.avisoSenha.TabIndex = 18;
            this.avisoSenha.Text = "label7";
            this.avisoSenha.Visible = false;
            // 
            // avisoEmail
            // 
            this.avisoEmail.AutoSize = true;
            this.avisoEmail.ForeColor = System.Drawing.Color.Red;
            this.avisoEmail.Location = new System.Drawing.Point(149, 252);
            this.avisoEmail.Name = "avisoEmail";
            this.avisoEmail.Size = new System.Drawing.Size(38, 15);
            this.avisoEmail.TabIndex = 19;
            this.avisoEmail.Text = "label7";
            this.avisoEmail.Visible = false;
            // 
            // avisoDataNascimento
            // 
            this.avisoDataNascimento.AutoSize = true;
            this.avisoDataNascimento.ForeColor = System.Drawing.Color.Red;
            this.avisoDataNascimento.Location = new System.Drawing.Point(255, 293);
            this.avisoDataNascimento.Name = "avisoDataNascimento";
            this.avisoDataNascimento.Size = new System.Drawing.Size(38, 15);
            this.avisoDataNascimento.TabIndex = 20;
            this.avisoDataNascimento.Text = "label7";
            this.avisoDataNascimento.Visible = false;
            // 
            // TelaCadastroUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 452);
            this.Controls.Add(this.avisoDataNascimento);
            this.Controls.Add(this.avisoEmail);
            this.Controls.Add(this.avisoSenha);
            this.Controls.Add(this.avisoNome);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancelar);
            this.Controls.Add(this.BtnCadastrar);
            this.Controls.Add(this.dataCriacaoUsuario);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataNascimentoUsuario);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtEmailUsuario);
            this.Controls.Add(this.txtSenhaUsuario);
            this.Controls.Add(this.txtNomeUsuario);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "TelaCadastroUsuario";
            this.Text = "CadastrarUsuarios";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtSenhaUsuario;
        private TextBox txtEmailUsuario;
        private Label label5;
        private DateTimePicker dataNascimentoUsuario;
        private Label label6;
        private DateTimePicker dataCriacaoUsuario;
        private Button BtnCadastrar;
        private Button Cancelar;
        private Label label1;
        private TextBox txtId;
        private TextBox txtNomeUsuario;
        private Label avisoNome;
        private Label avisoSenha;
        private Label avisoEmail;
        private Label avisoDataNascimento;
    }
}