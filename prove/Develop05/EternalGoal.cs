using System;

// Derived class for eternal goals that are never complete
public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    // Override RecordEvent method
    public override int RecordEvent()
    {
        // Eternal goals are never complete but always give points
        return _points;
    }

    // ooverride GetDetailsString method
    public override string GetDetailsString()
    {
        return $"{GetStatusString()} {Name} ({Description})";
    }

    // override GetStatusString method
    public override string GetStatusString()
    {
        // Eternal goals always show as incomplete
        return "[ ]";
    }

    // override GetStringRepresentation method
    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{Name},{Description},{_points}";
    }
}