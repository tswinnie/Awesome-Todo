using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeTodo.Models;

namespace AwesomeTodo.Services
{
    public interface ITodoDataService
    {
        Task<ObservableCollection<Todo>> GetTodos();
        Task<Todo> GetTodo(int id);
        ObservableCollection<Todo> AddTodo(ObservableCollection<Todo>_todos, Todo todo);
        ObservableCollection<Todo> RemoveTodo(ObservableCollection<Todo> _todos, int id);
    }
}
