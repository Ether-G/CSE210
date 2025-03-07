using System;
using System.Collections.Generic;

public class ActivityLog
{
    private Dictionary<string, int> _activityCounts;
    
    public ActivityLog()
    {
        _activityCounts = new Dictionary<string, int>
        {
            { "Breathing Activity", 0 },
            { "Reflection Activity", 0 },
            { "Listing Activity", 0 },
            { "Gratitude Activity", 0 }
        };
    }
    
    public void LogActivity(string activityName)
    {
        if (_activityCounts.ContainsKey(activityName))
        {
            _activityCounts[activityName]++;
        }
    }
    
    public void DisplayLog()
    {
        Console.Clear();
        Console.WriteLine("Activity Log");
        Console.WriteLine("------------");
        foreach (var activity in _activityCounts)
        {
            Console.WriteLine($"{activity.Key}: {activity.Value} times");
        }
        Console.WriteLine("\nPress Enter to continue...");
        Console.ReadLine();
    }
}