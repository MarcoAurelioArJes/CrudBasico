namespace ModeloUsuarios
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public DateTime DataCriacao { get; set; }

        private static int idContador = 0;
        
        public int IncrementaId()
        {
            idContador++;
            return Id += idContador;
        }
    }
}