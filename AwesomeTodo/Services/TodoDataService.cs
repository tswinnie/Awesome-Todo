using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using AwesomeTodo.Models;

namespace AwesomeTodo.Services
{
    public class TodoDataService : ITodoDataService
    {
        private HttpClient _http;
        private string URL = "https://jsonplaceholder.typicode.com/todos/";

        public TodoDataService()
        {
            _http = new HttpClient();
        }

        public ObservableCollection<Todo> AddTodo(ObservableCollection<Todo>_todos, Todo todo)
        {
            _todos.Add(todo);
            return _todos;

        }

        public async Task<Todo> GetTodo(int id)
        {

            var response = await _http.GetAsync($"https://jsonplaceholder.typicode.com/todos/{id}");
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(Todo));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (Todo)serializer.ReadObject(ms);

            return data;

        }

        public async Task<ObservableCollection<Todo>> GetTodos()
        {
            var response = await _http.GetAsync(URL);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(ObservableCollection<Todo>));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (ObservableCollection<Todo>)serializer.ReadObject(ms);
            return data;
        }

        public ObservableCollection<Todo> RemoveTodo(ObservableCollection<Todo> _todos, int id)
        {

            _todos.Remove(_todos.Where(i => i.id == id).Single());
            return _todos;

        }
    }
}
