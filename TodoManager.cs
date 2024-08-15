using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TodoApp
{
    public class TodoManager : ITodoOperations
    {
        private List<TodoItem> _todos = new List<TodoItem>(); //creating list for complete navako
        private List<TodoItem> _completedTodos = new List<TodoItem>(); //for complete vako
        private readonly string filePath = @"C:\Users\Dell\source\repos\ToDoList\todos.txt"; //reads contents in the file

        public void AddTodoItem(TodoItem item)
        {
            item.Id = GetNextId(); //give new id to every new todo

            _todos.Add(item); //adds the todo
        }

        public void RemoveTodoItem(int id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id); //todos first (value that matches)or default (null)value is returned
            // t refers to the items in todos checks wether the given id matches existing id
            if (todo != null) //not null
            {
                _todos.Remove(todo); //removed
                _completedTodos.Remove(todo); //completed bata ni removed
                Console.WriteLine("Todo removed successfully."); 
            }
            else
            {
                Console.WriteLine($"Todo item with Id {id} not found in active todos.");
            }
        }

        public void MarkTodoAsCompleted(int id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo != null)
            {
                todo.IsCompleted = true;
                _completedTodos.Add(todo);
                Console.WriteLine("Todo marked as completed.");
            }
            else
            {
                Console.WriteLine($"Todo item with Id {id} not found.");
            }
        }

        public void EditTodoItem(int id, string title, string description, DateTime dueDate, bool isCompleted)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo != null)
            {
                todo.Title = title;
                todo.Description = description;
                todo.DueDate = dueDate;
                todo.IsCompleted = isCompleted;

                if (isCompleted && !_completedTodos.Contains(todo))
                {
                    _completedTodos.Add(todo);
                }
                else if (!isCompleted && _completedTodos.Contains(todo))
                {
                    _completedTodos.Remove(todo);
                }

                Console.WriteLine("Todo edited successfully.");
            }
            else
            {
                Console.WriteLine($"Todo item with Id {id} not found.");
            }
        }

        public List<TodoItem> RetrieveAllTodos()
        {
            return _todos;
        }

        public List<TodoItem> RetrieveCompletedTodos()
        {
            return _completedTodos;
        }

        public void SaveTodosToFile()
        {
            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    foreach (var todo in _todos) //instance is todo 
                    {
                        writer.WriteLine($"{todo.Id},{todo.Title},{todo.Description},{todo.IsCompleted},{todo.DueDate:yyyy-MM-dd}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving todos: {ex.Message}");
            }
        }

        public void LoadTodosFromFile()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    using (var reader = new StreamReader(filePath)) //A class used to read characters from a byte stream in a particular encoding.
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split(',');
                            var todo = new TodoItem
                            {
                                Id = int.Parse(parts[0]),
                                Title = parts[1],
                                Description = parts[2],
                                IsCompleted = bool.Parse(parts[3]),
                                DueDate = DateTime.Parse(parts[4])
                            };
                            _todos.Add(todo);
                            if (todo.IsCompleted)
                            {
                                _completedTodos.Add(todo);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading todos: {ex.Message}");
                }
            }
        }

        public int GetNextId()
        {
            return _todos.Count > 0 ? _todos.Max(t => t.Id) + 1 : 1;
        }
    }
}
