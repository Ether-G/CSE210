using System;

class Program
{
    static void Main(string[] args)
    {
        // Prompting User for First Name
        Console.Write("What is your first name? ");
        string firstName = Console.ReadLine();
        // Prompting for last Name
        Console.Write("What is your last name? ");
        string lastName = Console.ReadLine();
        // Print statement
        Console.WriteLine($"Your name is {lastName}, {firstName} {lastName}.");
    }
}
