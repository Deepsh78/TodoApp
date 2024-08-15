using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TodoApp
{
    public class DataStorage
    {
        private readonly string filePath = @"C:\Users\Dell\source\repos\ToDoList\todos.txt"; //reads contents in the file

        public void SaveTodosToFile(List<TodoItem> todos)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var todo in todos)
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

        public List<TodoItem> LoadTodosFromFile()
        {
            var todos = new List<TodoItem>();
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split(',');
                            if (parts.Length == 5)
                            {
                                todos.Add(new TodoItem
                                {
                                    Id = int.Parse(parts[0]),
                                    Title = parts[1],
                                    Description = parts[2],
                                    IsCompleted = bool.Parse(parts[3]),
                                    DueDate = DateTime.Parse(parts[4])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading todos: {ex.Message}");
            }
            return todos;
        }
    }
}
