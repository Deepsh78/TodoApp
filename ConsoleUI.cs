using System;
using System.Collections.Generic;

namespace TodoApp
{
    public class ConsoleUI
    {
        private TodoManager _todoManager = new TodoManager(); //TodoManager ko private instance to manage it

        public void Run() //main loop 
        {
            _todoManager.LoadTodosFromFile(); //file bata loading todos

            while (true)
            {
                Console.WriteLine("\nTodo List Application");
                Console.WriteLine("1. Add Todo");
                Console.WriteLine("2. Remove Todo");
                Console.WriteLine("3. Mark Todo as Completed");
                Console.WriteLine("4. Edit Todo");
                Console.WriteLine("5. View Todos");
                Console.WriteLine("6. View Completed Todos");
                Console.WriteLine("7. Save and Exit");
                Console.Write("Select an option: ");
                var option = Console.ReadLine(); //var use garya to store the option as variable

                switch (option)
                {
                    case "1":
                        AddTodo();
                        break;
                    case "2":
                        RemoveTodo();
                        break;
                    case "3":
                        MarkAsCompleted();
                        break;
                    case "4":
                        EditTodo();
                        break;
                    case "5":
                        ViewTodos();
                        break;
                    case "6":
                        ViewCompletedTodos();
                        break;
                    case "7":
                        SaveAndExit();
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void AddTodo() //for add
        {
            var item = new TodoItem(); //item is a variable that holds the reference of instance of TodoItem class
            Console.Write("Enter Title: ");
            item.Title = Console.ReadLine(); //Title ma gayera store huncha 

            Console.Write("Enter Description: ");
            item.Description = Console.ReadLine();

            Console.Write("Enter Due Date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dueDate)) //is not valid returns false
            {
                Console.WriteLine("Invalid date format.");//error
                return;
            }
            item.DueDate = dueDate;

            item.IsCompleted = false;

            _todoManager.AddTodoItem(item); //_todoManager if from TodoManager class and sab items are added to the list of that class
            Console.WriteLine("Todo added successfully.");
        }

        private void RemoveTodo()
        {
            Console.Write("Enter Todo Id to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }
            _todoManager.RemoveTodoItem(id);
            Console.WriteLine("Todo removed successfully.");
        }

        private void MarkAsCompleted()
        {
            Console.Write("Enter Todo Id to mark as completed: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }
            _todoManager.MarkTodoAsCompleted(id);
            Console.WriteLine("Todo marked as completed.");
        }

        private void EditTodo()
        {
            Console.Write("Enter Todo Id to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            Console.Write("Enter New Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter New Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter New Due Date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            Console.Write("Is Completed (true/false): ");
            if (!bool.TryParse(Console.ReadLine(), out bool isCompleted))
            {
                Console.WriteLine("Invalid boolean format.");
                return;
            }

            _todoManager.EditTodoItem(id, title, description, dueDate, isCompleted);
            Console.WriteLine("Todo edited successfully.");
        }

        private void ViewTodos()
        {
            var todos = _todoManager.RetrieveAllTodos();
            if (todos.Count == 0)//todos chaina vaney dont
            {
                Console.WriteLine("No todos available.");
                return;
            }

            foreach (var todo in todos)
            {
                Console.WriteLine($"Id: {todo.Id}/n, Title: {todo.Title}/n, Description: {todo.Description}/n, Due Date: {todo.DueDate:yyyy-MM-dd}/n, Completed: {todo.IsCompleted}/n"); //for each le one by one sabai dekhaucha
            }
        }

        private void ViewCompletedTodos()
        {
            var completedTodos = _todoManager.RetrieveCompletedTodos();
            if (completedTodos.Count == 0)
            {
                Console.WriteLine("No completed todos available.");
                return;
            }

            foreach (var todo in completedTodos)
            {
                Console.WriteLine($"Id: {todo.Id}, Title: {todo.Title}, Description: {todo.Description}, Due Date: {todo.DueDate:yyyy-MM-dd}, Completed: {todo.IsCompleted}");
            }
        }

        private void SaveAndExit()
        {
            _todoManager.SaveTodosToFile();
            Console.WriteLine("Todos saved successfully. Exiting...");
        }
    }
}
