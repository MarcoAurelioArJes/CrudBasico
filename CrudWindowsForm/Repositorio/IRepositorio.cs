﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudWindowsForm.Repositorio
{
    public interface IRepositorio<T> where T : class
    {
        public void Criar(T entidade);
        public List<T> ObterTodos();
        public T ObterPorId(int id);
        public void Atualizar(T entidade);
        public void Deletar(int id);
    }
}
