using System;
using System.Collections.Generic;

public class GratitudeActivity : Activity
{
    private List<string> _gratitudePrompts;

    public GratitudeActivity() : base("Gratitude Activity", 
        "This activity will help you develop gratitude by focusing on the positive aspects of your day and life.")
    {
        _gratitudePrompts = new List<string>
        {
            "What made you smile today?",
            "What is something beautiful you saw today?",
            "What is something someone did for you that you're grateful for?",
            "What is a simple pleasure that you enjoyed today?",
            "What is something you're looking forward to?"
        };
    }

    private string GetRandomPrompt()
    {
        Random random = new Random();
        return _gratitudePrompts[random.Next(_gratitudePrompts.Count)];
    }

    public override void Run()
    {
        DisplayStartingMessage();
        
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(GetDuration());
        
        while (DateTime.Now < endTime)
        {
            string prompt = GetRandomPrompt();
            Console.WriteLine($"Reflect on: {prompt}");
            Console.WriteLine("Take a moment to write down your thoughts...");
            ShowSpinner(15);
            Console.WriteLine("\nPress Enter when you've written your gratitude...");
            Console.ReadLine();
            Console.Clear();
        }
        
        DisplayEndingMessage();
    }
}