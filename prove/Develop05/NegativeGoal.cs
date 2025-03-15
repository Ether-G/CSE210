using System;

// Creative extension: A goal that deducts points when a bad habit is recorded
// This helps with breaking bad habits by providing a penalty... should you be honest about it (:
public class NegativeGoal : Goal
{
    private int _penalty;
    private int _occurrences;

    public NegativeGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _penalty = points; // same value for penalty
        _occurrences = 0;
    }

    public NegativeGoal(string name, string description, int points, int occurrences)
        : base(name, description, points)
    {
        _penalty = points;
        _occurrences = occurrences;
    }

    public int Occurrences => _occurrences;
    public int Penalty => _penalty;

    // override RecordEvent method
    public override int RecordEvent()
    {
        _occurrences++;
        // return negative
        return -_penalty;
    }

    // override GetDetailsString method
    public override string GetDetailsString()
    {
        return $"{GetStatusString()} {Name} ({Description}) -- Occurred {_occurrences} times (-{_penalty} points each time)";
    }

    // override GetStatusString method
    public override string GetStatusString()
    {
        // Negative goals always show as incomplete - never want to "complete" them.
        return "[!]";
    }

    // override GetStringRepresentation method
    public override string GetStringRepresentation()
    {
        return $"NegativeGoal:{Name},{Description},{_penalty},{_occurrences}";
    }
}