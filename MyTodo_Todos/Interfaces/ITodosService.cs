﻿using DataTransfer.DataTransferObjects;

namespace MyTodo_Todos.Interfaces
{
    public interface ITodosService
    {
        IEnumerable<TodoDto> GetAll();
        TodoDto? GetById(long id);
        void Create(TodoDto todoDto);
        bool Update(TodoDto todoDto);
        bool Delete(long id);
    }
}