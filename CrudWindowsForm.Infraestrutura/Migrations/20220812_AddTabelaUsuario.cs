using FluentMigrator;

namespace CrudWindowsForm.Infraestrutura.Migrations
{
    [Migration(20220812102600)]
    public class _20220812_AddTabelaUsuario : Migration
    {
        public override void Up()
        {
            Create.Table("Usuario")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Nome").AsString().NotNullable()
                .WithColumn("Senha").AsString(1000).NotNullable()
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("DataNascimento").AsDate()
                .WithColumn("DataCriacao").AsDate().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Usuario");
        }
    }
}
