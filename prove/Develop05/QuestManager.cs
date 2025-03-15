using System;
using System.Collections.Generic;
using System.IO;

// Class that manages all goals and quest progress
public class QuestManager
{
    private List<Goal> _goals;
    private int _score;
    private int _level;
    private Dictionary<int, string> _levelTitles;
    private List<string> _achievements;
    public QuestManager()
    {
        _goals = new List<Goal>();
        _score = 0;
        _level = 1;
        _achievements = new List<string>();
        InitializeLevelTitles();
    }

    public int Score => _score;
    public int Level => _level;
    public string LevelTitle => _levelTitles.ContainsKey(_level) ? _levelTitles[_level] : "Master Quester";
    public List<string> Achievements => _achievements;

    // Initialize
    private void InitializeLevelTitles()
    {
        _levelTitles = new Dictionary<int, string>
        {
            { 1, "Novice Quester" },
            { 2, "Apprentice Quester" },
            { 3, "Journeyman Quester" },
            { 4, "Expert Quester" },
            { 5, "Master Quester" },
            { 6, "Legendary Quester" },
            { 7, "Divine Quester" },
            { 8, "Transcendent Quester" },
            { 9, "Eternal Quester" },
            { 10, "Celestial Quester" }
        };
    }

    // Add a goal
    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    // Get all
    public List<Goal> GetGoals()
    {
        return _goals;
    }

    // event for a goal
    public int RecordEvent(int index)
    {
        if (index < 0 || index >= _goals.Count)
        {
            return 0;
        }

        Goal goal = _goals[index];
        int pointsEarned = goal.RecordEvent();
        _score += pointsEarned;
        
        // level up????
        CheckForLevelUp();
        
        // achievements ???
        CheckForAchievements(goal, pointsEarned);
        
        return pointsEarned;
    }

    // eecord progress for a progressive goal
    public int RecordProgressiveEvent(int index, double amount)
    {
        if (index < 0 || index >= _goals.Count || !(_goals[index] is ProgressiveGoal))
        {
            return 0;
        }

        ProgressiveGoal goal = (ProgressiveGoal)_goals[index];
        int pointsEarned = goal.RecordProgress(amount);
        _score += pointsEarned;
        
        // level ???
        CheckForLevelUp();
        
        // achievements ???
        CheckForAchievements(goal, pointsEarned);
        
        return pointsEarned;
    }

    private void CheckForLevelUp()
    {
        // 1000 points = 1 level
        int newLevel = ((_score / 1000) + 1);
        
        if (newLevel > _level)
        {
            // Level up !!!
            int levelsGained = newLevel - _level;
            _level = newLevel;
            
            // level up achievement
            _achievements.Add($"Leveled up to {LevelTitle} (Level {_level})!");
        }
    }

    private void CheckForAchievements(Goal goal, int pointsEarned)
    {
        if (goal is SimpleGoal && goal.IsComplete)
        {
            _achievements.Add($"Completed simple goal: {goal.Name}");
        }
        else if (goal is ChecklistGoal checklistGoal && checklistGoal.IsComplete)
        {
            _achievements.Add($"Completed checklist goal: {goal.Name}");
        }
        else if (goal is ProgressiveGoal progressiveGoal && progressiveGoal.IsComplete)
        {
            _achievements.Add($"Completed progressive goal: {goal.Name}");
        }

        if (_score >= 5000 && !_achievements.Contains("Reached 5,000 points!"))
        {
            _achievements.Add("Reached 5,000 points!");
        }
        if (_score >= 10000 && !_achievements.Contains("Reached 10,000 points!"))
        {
            _achievements.Add("Reached 10,000 points!");
        }
    }

    // Save goals and score to a file
    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine($"{_score},{_level}");

            outputFile.WriteLine(_achievements.Count);
            foreach (string achievement in _achievements)
            {
                outputFile.WriteLine(achievement);
            }

            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }
    }

    // Load goals and score from a file
    public void LoadFromFile(string filename)
    {
        _goals.Clear();
        _achievements.Clear();
        
        string[] lines = File.ReadAllLines(filename);

        string[] scoreLevel = lines[0].Split(',');
        _score = int.Parse(scoreLevel[0]);
        _level = int.Parse(scoreLevel[1]);
        
        int achievementCount = int.Parse(lines[1]);
        
        for (int i = 0; i < achievementCount; i++)
        {
            _achievements.Add(lines[2 + i]);
        }
        
        int startIndex = 2 + achievementCount;
        
        for (int i = startIndex; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] parts = line.Split(':');
            
            string goalType = parts[0];
            string goalData = parts[1];
            
            Goal goal = CreateGoal(goalType, goalData);
            if (goal != null)
            {
                _goals.Add(goal);
            }
        }
    }

    // create a goal from a string rep
    private Goal CreateGoal(string goalType, string goalData)
    {
        string[] parts = goalData.Split(',');
        
        switch (goalType)
        {
            case "SimpleGoal":
                return new SimpleGoal(
                    parts[0], // name
                    parts[1], // description
                    int.Parse(parts[2]), // points
                    bool.Parse(parts[3]) // isComplete
                );
                
            case "EternalGoal":
                return new EternalGoal(
                    parts[0], // name
                    parts[1], // description
                    int.Parse(parts[2]) // points
                );
                
            case "ChecklistGoal":
                return new ChecklistGoal(
                    parts[0], // name
                    parts[1], // description
                    int.Parse(parts[2]), // points
                    int.Parse(parts[3]), // target
                    int.Parse(parts[4]), // bonus
                    int.Parse(parts[5]) // amountCompleted
                );
                
            case "ProgressiveGoal":
                return new ProgressiveGoal(
                    parts[0], // name
                    parts[1], // description
                    int.Parse(parts[2]), // points
                    double.Parse(parts[3]), // targetValue
                    parts[4], // unit
                    int.Parse(parts[5]), // bonusPoints
                    double.Parse(parts[6]) // currentValue
                );
                
            case "NegativeGoal":
                return new NegativeGoal(
                    parts[0], // name
                    parts[1], // description
                    int.Parse(parts[2]), // points/penalty
                    int.Parse(parts[3]) // occurrences
                );
                
            default:
                return null;
        }
    }
}