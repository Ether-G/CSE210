using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

/* 
 * CSE 210: Programming with Classes
 * Eternal Quest Program
 * 
 * Exceeding Requirements:
 * 1. Added a leveling system where users gain levels based on their score, with unique titles for each level
 * 2. Implemented an achievements system that tracks and displays accomplishments
 * 3. Added two additional goal types beyond the requirements:
 *    - ProgressiveGoal: Allows tracking progress toward a larger goal with partial credit
 *    - NegativeGoal: Tracks bad habits and deducts points when they occur (:<
 * 4. Added visual feedback animations when recording events (silly point gain/loss animation)
 * 5. Implemented proper error handling throughout the application (Found some error cases that I couldnt quite figure out... but they are extreeme. I spent way too much time doing this)
 */

class Program
{
    private static QuestManager _questManager = new QuestManager();

    static void Main(string[] args)
    {
        bool quit = false;
        
        while (!quit)
        {
            DisplayHeader();
            DisplayMenu();
            
            string choice = Console.ReadLine();
            Console.WriteLine();
            
            switch (choice)
            {
                case "1":
                    CreateNewGoal();
                    break;
                case "2":
                    ListGoals();
                    break;
                case "3":
                    SaveGoals();
                    break;
                case "4":
                    LoadGoals();
                    break;
                case "5":
                    RecordEvent();
                    break;
                case "6":
                    DisplayAchievements();
                    break;
                case "7":
                    quit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            
            if (!quit)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        
        Console.WriteLine("Thank you for using the Eternal Quest Program!");
    }

    // program header with current status
    static void DisplayHeader()
    {
        Console.WriteLine("===========================================");
        Console.WriteLine("          ETERNAL QUEST PROGRAM           ");
        Console.WriteLine("===========================================");
        Console.WriteLine($"Level: {_questManager.Level} - {_questManager.LevelTitle}");
        Console.WriteLine($"Score: {_questManager.Score} points");
        Console.WriteLine("===========================================");
        Console.WriteLine();
    }

    // main menu
    static void DisplayMenu()
    {
        Console.WriteLine("Menu Options:");
        Console.WriteLine("  1. Create New Goal");
        Console.WriteLine("  2. List Goals");
        Console.WriteLine("  3. Save Goals");
        Console.WriteLine("  4. Load Goals");
        Console.WriteLine("  5. Record Event");
        Console.WriteLine("  6. View Achievements");
        Console.WriteLine("  7. Quit");
        Console.Write("Select a choice from the menu: ");
    }

    // new goal
    static void CreateNewGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.WriteLine("  4. Progressive Goal");
        Console.WriteLine("  5. Negative Goal");
        Console.Write("Which type of goal would you like to create? ");
        
        string goalType = Console.ReadLine();
        Console.WriteLine();
        
        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();
        
        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();
        
        Console.Write("What is the amount of points associated with this goal? ");
        int points = int.Parse(Console.ReadLine());
        
        Goal newGoal = null;
        
        switch (goalType)
        {
            case "1": // Simple
                newGoal = new SimpleGoal(name, description, points);
                break;
                
            case "2": // Eternal
                newGoal = new EternalGoal(name, description, points);
                break;
                
            case "3": // Checklist
                Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                int target = int.Parse(Console.ReadLine());
                
                Console.Write("What is the bonus for accomplishing it that many times? ");
                int bonus = int.Parse(Console.ReadLine());
                
                newGoal = new ChecklistGoal(name, description, points, target, bonus);
                break;
                
            case "4": // Progressive
                Console.Write("What is the target value to reach? ");
                double targetValue = double.Parse(Console.ReadLine());
                
                Console.Write("What is the unit of measurement (e.g., miles, chapters, pounds)? ");
                string unit = Console.ReadLine();
                
                Console.Write("What is the bonus for completing the entire goal? ");
                int progressBonus = int.Parse(Console.ReadLine());
                
                newGoal = new ProgressiveGoal(name, description, points, targetValue, unit, progressBonus);
                break;
                
            case "5": // Negative
                Console.WriteLine($"This goal will deduct {points} points each time it happens.");
                newGoal = new NegativeGoal(name, description, points);
                break;
                
            default:
                Console.WriteLine("Invalid goal type.");
                return;
        }
        
        if (newGoal != null)
        {
            _questManager.AddGoal(newGoal);
            Console.WriteLine($"\nGoal '{name}' created successfully!");
        }
    }

    // list all
    static void ListGoals()
    {
        List<Goal> goals = _questManager.GetGoals();
        
        if (goals.Count == 0)
        {
            Console.WriteLine("You have no goals yet. Create some goals first!");
            return;
        }
        
        Console.WriteLine("Your Goals:");
        Console.WriteLine("-----------------------------------");
        
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetDetailsString()}");
        }
    }

    // save
    static void SaveGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();
        
        _questManager.SaveToFile(filename);
        Console.WriteLine($"Goals saved to {filename} successfully!");
    }

    // load
    static void LoadGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();
        
        if (!File.Exists(filename))
        {
            Console.WriteLine($"Error: File '{filename}' does not exist.");
            return;
        }
        
        try
        {
            _questManager.LoadFromFile(filename);
            Console.WriteLine($"Goals loaded from {filename} successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }

    // Record event
    static void RecordEvent()
    {
        List<Goal> goals = _questManager.GetGoals();
        
        if (goals.Count == 0)
        {
            Console.WriteLine("You have no goals yet. Create some goals first!");
            return;
        }
        
        Console.WriteLine("The goals are:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name}");
        }
        
        Console.Write("Which goal did you accomplish? ");
        if (!int.TryParse(Console.ReadLine(), out int goalNumber) || goalNumber < 1 || goalNumber > goals.Count)
        {
            Console.WriteLine("Invalid goal number.");
            return;
        }
        
        int index = goalNumber - 1;
        Goal selectedGoal = goals[index];
        
        // Handle progressive
        if (selectedGoal is ProgressiveGoal)
        {
            Console.Write($"How much progress did you make? ");
            if (!double.TryParse(Console.ReadLine(), out double amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
                return;
            }
            
            int pointsEarned = _questManager.RecordProgressiveEvent(index, amount);
            DisplayPointsAnimation(pointsEarned);
        }
        else
        {
            int pointsEarned = _questManager.RecordEvent(index);
            DisplayPointsAnimation(pointsEarned);
        }
    }

    // achievement
    static void DisplayAchievements()
    {
        List<string> achievements = _questManager.Achievements;
        
        if (achievements.Count == 0)
        {
            Console.WriteLine("You haven't earned any achievements yet. Keep working on your goals!");
            return;
        }
        
        Console.WriteLine("Your Achievements:");
        Console.WriteLine("-----------------------------------");
        
        for (int i = 0; i < achievements.Count; i++)
        {
            Console.WriteLine($"🏆 {achievements[i]}");
        }
    }

    // animation
    static void DisplayPointsAnimation(int points)
    {
        if (points == 0)
        {
            Console.WriteLine("No points earned. Goal may already be complete.");
            return;
        }
        
        string message = points > 0 
            ? $"Congratulations! You have earned {points} points!" 
            : $"Oops! You lost {Math.Abs(points)} points.";
            
        string symbol = points > 0 ? "+" : "-";
        ConsoleColor color = points > 0 ? ConsoleColor.Green : ConsoleColor.Red;
        
        Console.WriteLine();
        
        // Store color
        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        
        // animation
        for (int i = 0; i < 3; i++)
        {
            Console.Write($"\r{symbol}{Math.Abs(points)} points!");
            Thread.Sleep(200);
            Console.Write("\r                  ");
            Thread.Sleep(200);
        }
        
        Console.WriteLine($"\r{message}");
        
        // reset color
        Console.ForegroundColor = originalColor;
        
        // display level info
        Console.WriteLine($"You now have {_questManager.Score} points.");
        Console.WriteLine($"You are a Level {_questManager.Level} {_questManager.LevelTitle}!");
    }
}