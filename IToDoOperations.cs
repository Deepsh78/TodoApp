using System;
using System.Collections.Generic;

namespace TodoApp
{
    public interface ITodoOperations
    {
        void AddTodoItem(TodoItem item); //takes a TodoItem object as a parameter and adds it to the list of todo items.
        void RemoveTodoItem(int id); //removes
        void MarkTodoAsCompleted(int id);
        void EditTodoItem(int id, string title, string description, DateTime dueDate, bool isCompleted);
        List<TodoItem> RetrieveAllTodos(); // returns a list of all todo items.
        List<TodoItem> RetrieveCompletedTodos();
        void SaveTodosToFile();
        void LoadTodosFromFile();
        int GetNextId(); //returns the next unique ID for a new todo item.
    }
}
