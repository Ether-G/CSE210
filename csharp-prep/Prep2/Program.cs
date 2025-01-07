using System;

class Program
{
    static void Main(string[] args)
    {
        // Decided to use ternary operators instead od a linear if else chain to make code more compact.
        
        // Prompting the user for input of grade percentage
        Console.Write("Enter your grade percentage: ");
        int percentage = int.Parse(Console.ReadLine());

        // Determination of the letter grade
        string letter = percentage >= 90 ? "A" :
                        percentage >= 80 ? "B" :
                        percentage >= 70 ? "C" :
                        percentage >= 60 ? "D" : "F";

        string sign = (letter != "A" && letter != "F") ? 
                      (percentage % 10 >= 7 ? "+" : (percentage % 10 < 3 ? "-" : "")) : 
                      (letter == "A" && percentage < 93 ? "-" : "");

        Console.WriteLine($"Your final grade is: {letter}{sign}");

        // pass/fail message
        Console.WriteLine(percentage >= 70 
                          ? "Congratulations! You passed the course." 
                          : "Don't give up! Better luck next time.");
    }
}
