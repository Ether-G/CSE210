using System;

// Derived class for simple goals that can be completed once
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    // loading from file
    public SimpleGoal(string name, string description, int points, bool isComplete)
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    // Override RecordEvent method
    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return _points;
        }
        return 0;
    }

    // Override GetDetailsString method
    public override string GetDetailsString()
    {
        return $"{GetStatusString()} {Name} ({Description})";
    }

    // Override GetStringRepresentation method
    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{Name},{Description},{_points},{_isComplete}";
    }
}