using System;
using System.Collections.Generic;
using System.Threading;

public abstract class Activity
{
    // Private member vars
    private string _name;
    private string _description;
    private int _duration;

    // constructor
    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    // get and set
    protected string GetName()
    {
        return _name;
    }

    protected string GetDescription()
    {
        return _description;
    }

    protected int GetDuration()
    {
        return _duration;
    }

    protected void SetDuration(int duration)
    {
        _duration = duration;
    }

    // display starting message
    public void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name}.");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();
        
        Console.Write("How long, in seconds, would you like for your session? (All smaller than 10 seconds will default to 10 seconds.)");
        int duration = int.Parse(Console.ReadLine());
        SetDuration(duration);
        
        Console.Clear();
        Console.WriteLine("Get ready...");
        ShowSpinner(5);
    }

    // display ending message
    public void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!!");
        ShowSpinner(3);
        Console.WriteLine();
        Console.WriteLine($"You have completed another {_duration} seconds of the {_name}.");
        ShowSpinner(5);
    }

    // animation
    public void ShowSpinner(int seconds)
    {
        List<string> spinnerAnimation = new List<string> { "|", "/", "-", "\\" };
        
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(seconds);
        
        int animationIndex = 0;
        
        while (DateTime.Now < endTime)
        {
            string spinner = spinnerAnimation[animationIndex];
            Console.Write(spinner);
            Thread.Sleep(250);
            Console.Write("\b \b");
            
            animationIndex++;
            if (animationIndex >= spinnerAnimation.Count)
            {
                animationIndex = 0;
            }
        }
    }

    // animation 2
    public void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    // abstract to run the activity
    public abstract void Run();
}