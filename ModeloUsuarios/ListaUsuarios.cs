using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloUsuarios
{
    public sealed class ListaUsuarios
    {
        private static List<Usuario>? _instancia;
        private static readonly object _bloqueador = new();
        private static int _idContador = 0;

        private ListaUsuarios() { }

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
            get { return ++_idContador; }
        }
    }
}
