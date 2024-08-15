using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using TodoApp;


namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleUI runapp = new ConsoleUI(); //creating instance
            runapp.Run(); //starts program by calling main loop
        }
    }
}

