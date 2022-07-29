namespace CrudWindowsForm.Modelo
{
    public sealed class ListaDeUsuarios
    {
        private static List<Usuario>? _instancia;
        private static readonly object _bloqueador = new();
        private static int _idContador = 1;

        private ListaDeUsuarios() { }

        public static List<Usuario> Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (_bloqueador)
                        if (_instancia == null)
                            _instancia = new List<Usuario>();
                }
                return _instancia;
            }
        }

        public static int IdContador { 
            get { return _idContador++; }
        }
    }
}
