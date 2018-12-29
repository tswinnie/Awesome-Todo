using AwesomeTodo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AwesomeTodo.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AwesomeTodo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Todo> _todos;
        private TodoDataService _todoService;
        public ObservableCollection<Todo> Todos => _todos ?? (_todos = new ObservableCollection<Todo>());
        private ObservableCollection<Todo> _singleTodo;
        public ObservableCollection<Todo> SelectedTodo
        {
            get { return _singleTodo ?? (_singleTodo = new ObservableCollection<Todo>()); }
            set { _singleTodo = value; }
        }
        public MainPage()
        {
            this.InitializeComponent();
            _todoService = new TodoDataService();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var myTodos = await _todoService.GetTodos();
            foreach (var todo in myTodos)
            {
                Todos.Add(todo);

                //populate details view with first todo that comes back
                if (todo.id == 1)
                {
                    SelectedTodo.Add(todo);
                }

            }



        }

        //set the details view to what the user clicks
        private void MyTodoListItems_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                SelectedTodo.Clear();
                var todoSelected = (Todo)e.ClickedItem;
                var converteID = Convert.ToInt32(todoSelected.id);
                int todoSelectedId = Convert.ToInt32(converteID);
                //get the todo from the current list of todos
                foreach(var todo in Todos)
                {
                    if(todo.id == Convert.ToInt64(todoSelectedId))
                    {
                        SelectedTodo.Add(todo);
                        break;
                    }
                }
                
           
            }
            catch 
            {

            }
        }

        private void AddTodo_Click(object sender, RoutedEventArgs e)
        {
            //set default values to be added to todo collection
            int userIDToPass = 1;
            var lastTodo = Todos.Last();
            int lastTodoId = lastTodo.id += 1;
            bool newCompleted = false;
            string titleCreated = AddNewTodoField.Text;

            var newTodo = new Todo()
            {
                userId = userIDToPass,
                id = lastTodoId,
                title = titleCreated,
                completed = newCompleted
            };

            //add to list of todos at the beginning of the list
            Todos.Insert(0, newTodo);

            //show new todo in details view
            SelectedTodo.Clear();
            SelectedTodo.Add(newTodo);

            //clear searhc field
            AddNewTodoField.Text = "";


        }
    }
}
