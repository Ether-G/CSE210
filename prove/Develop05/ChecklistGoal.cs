using System;

// Derived class for checklist goals that must be completed a certain number of times
public class ChecklistGoal : Goal
{
    private int _target;
    private int _bonus;
    private int _amountCompleted;

    public ChecklistGoal(string name, string description, int points, int target, int bonus)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _amountCompleted = 0;
    }
    public ChecklistGoal(string name, string description, int points, int target, int bonus, int amountCompleted)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _amountCompleted = amountCompleted;
        _isComplete = _amountCompleted >= _target;
    }
    public int Target => _target;
    public int Bonus => _bonus;
    public int AmountCompleted => _amountCompleted;
    public override int RecordEvent()
    {
        _amountCompleted++;
        
        // Check if this completion finishes the goal
        if (_amountCompleted == _target)
        {
            _isComplete = true;
            return _points + _bonus; // Award points plus bonus
        }
        
        return _points; // Just award regular points
    }

    public override string GetDetailsString()
    {
        return $"{GetStatusString()} {Name} ({Description}) -- Currently completed: {_amountCompleted}/{_target}";
    }

    // override GetStringRepresentation method
    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{Name},{Description},{_points},{_target},{_bonus},{_amountCompleted}";
    }
}