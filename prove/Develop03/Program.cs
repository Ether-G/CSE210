using System;
using System.IO;

class Program
{
    public static void Main(string[] args)
    {
        string scripturesDirectory = "scriptures";
        string[] scriptureFiles = {
            Path.Combine(scripturesDirectory, "old-testament.json"),
            Path.Combine(scripturesDirectory, "new-testament.json"),
            Path.Combine(scripturesDirectory, "book-of-mormon.json"),
            Path.Combine(scripturesDirectory, "doctrine-and-covenants.json")
        };

        // Verify scripture files exist
        foreach (string file in scriptureFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"Warning: {file} not found!");
                return;
            }
        }

        // Load a random scripture
        Scripture scripture = ScriptureLoader.LoadRandomScriptureFromMultipleFiles(scriptureFiles);

        // Main
        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nType the complete scripture to continue, or type 'quit' to finish:");
            
            string input = Console.ReadLine() ?? "";

            if (input.ToLower() == "quit")
                break;

            // Check if correct
            if (!scripture.CheckRecitation(input))
            {
                Console.Clear();
                Console.WriteLine("Incorrect. Here's the complete scripture:");
                Console.WriteLine(scripture.GetFullText());
                Thread.Sleep(2000);  // Show the correct scripture for 2 seconds
                continue;  // Stay at current stage until correct
            }

            Console.Clear();
            Console.WriteLine("Correct! Moving to next stage...");
            Thread.Sleep(1000);  // Show success message for 1 second
            
            // Only hide more words if the recitation was correct
            scripture.HideRandomWords(3);
            
            if (scripture.IsCompletelyHidden())
            {
                Console.Clear();
                Console.WriteLine("Congratulations! You've successfully memorized the entire scripture!");
                Console.WriteLine("\nFinal Scripture:");
                Console.WriteLine(scripture.GetFullText());
                break;
            }
        }
    }
}