using System;

// Base class for all goals
public abstract class Goal
{
    private string _name;
    private string _description;
    protected int _points;
    protected bool _isComplete;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
        _isComplete = false;
    }

    public string Name => _name;
    public string Description => _description;
    public int Points => _points;
    public bool IsComplete => _isComplete;

    // abstract methods to be implemented by derived classes
    public abstract int RecordEvent();
    public abstract string GetDetailsString();
    public abstract string GetStringRepresentation();

    // virtual method that can be overridden
    public virtual string GetStatusString()
    {
        return _isComplete ? "[X]" : "[ ]";
    }
}