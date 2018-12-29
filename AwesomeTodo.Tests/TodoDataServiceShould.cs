using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeTodo.Services;
using AwesomeTodo.Models;
using System.Collections.ObjectModel;

namespace AwesomeTodo.Tests
{
    [TestClass]
    public class TodoDataServiceShould
    {
        private TodoDataService _todoService;
        private ObservableCollection<Todo> _todos;

        //test AddTodo
        //test GetTodo
        //test GetTodos
        //test RemoveTodo
        [TestInitialize]
        public void TodoDataServiceInitialize()
        {
            _todoService = new TodoDataService();
            _todos = new ObservableCollection<Todo>();
        }
        
        [TestMethod]
        public void TodoDataService_AddTodo()
        {
            //create todo to add
            var newTodo = new Todo()
            {
                userId = 1,
                id = 1,
                title = "new todo title",
                completed = false
            };

            var todoCollectionReturned = _todoService.AddTodo(_todos, newTodo);
            var todoReturned = todoCollectionReturned.SingleOrDefault(t => t.id == 1);

            Assert.AreEqual(newTodo.title, todoReturned.title);
             
        }

        [TestMethod]
        public async Task TodoDataService_GetTodo()
        {
            var expectedData = new Todo()
            {
                userId = 1,
                id = 1,
                title = "delectus aut autem",
                completed = false
            };

            Todo returnedTodo =  await _todoService.GetTodo(1);
            Assert.AreEqual(expectedData.title, returnedTodo.title);
        }

        [TestMethod]
        public async Task TodoDataService_GetTodos()
        {
            int expectedTodoCount = 200;
            var returneTodos = await _todoService.GetTodos();
            Assert.AreEqual(expectedTodoCount, returneTodos.Count());
        }

        [TestMethod]
        public void TodoDataService_RemoveTodo()
        {
            int expectedCount = 0;

            //create new todo
            var newTodo = new Todo()
            {
                userId = 1,
                id = 1,
                title = "new todo title",
                completed = false
            };

            //add one new todo collection
            _todos.Add(newTodo);

            //call service
            var returnedTodos = _todoService.RemoveTodo(_todos, 1);

            Assert.AreEqual(expectedCount, returnedTodos.Count());

        }


    }
}
