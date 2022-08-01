namespace CrudWindowsForm.Servicos
{
    public sealed class ListaSingleton<T>
    {
        private static List<T>? _instancia;
        private static readonly object _bloqueador = new();
        private static int _idContador = 1;

        private ListaSingleton() { }

        public static List<T> Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (_bloqueador)
                        if (_instancia == null)
                            _instancia = new List<T>();
                }
                return _instancia;
            }
        }

        public static int IdContador { 
            get { return _idContador++; }
        }
    }
}
