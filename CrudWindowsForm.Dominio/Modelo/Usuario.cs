using LinqToDB.Mapping;

namespace CrudWindowsForm.Dominio.Modelo
{
    [Table(Name = "Usuario")]
    public class Usuario
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column(Name = "Nome"), NotNull]
        public string Nome { get; set; }

        [Column(Name = "Senha"), NotNull]
        public string Senha { get; set; }

        [Column(Name = "Email"), NotNull]
        public string Email { get; set; }

        [Column(Name = "DataNascimento")]
        public DateTime? DataNascimento { get; set; }

        [Column(Name = "DataCriacao"), NotNull]
        public DateTime DataCriacao { get; set; }
    }
}