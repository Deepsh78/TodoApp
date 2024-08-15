using System;

namespace TodoApp
{
    public class TodoItem : Entity
    {
        private string _title;
        private string _description;
        private bool _isCompleted;
        private DateTime _dueDate;
        //initialize

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public bool IsCompleted
        {
            get { return _isCompleted; }
            set { _isCompleted = value; }
        }

        public DateTime DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }
    }
}
