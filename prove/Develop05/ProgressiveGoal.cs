using System;

// Creative extension: A goal that tracks progress towards a larger achievement
public class ProgressiveGoal : Goal
{
    private double _currentValue;
    private double _targetValue;
    private string _unit;
    private int _bonusPoints;
    public ProgressiveGoal(string name, string description, int points, double targetValue, 
                           string unit, int bonusPoints)
        : base(name, description, points)
    {
        _currentValue = 0;
        _targetValue = targetValue;
        _unit = unit;
        _bonusPoints = bonusPoints;
    }
    public ProgressiveGoal(string name, string description, int points, double targetValue, 
                           string unit, int bonusPoints, double currentValue)
        : base(name, description, points)
    {
        _currentValue = currentValue;
        _targetValue = targetValue;
        _unit = unit;
        _bonusPoints = bonusPoints;
        _isComplete = _currentValue >= _targetValue;
    }

    // partial progress
    public int RecordProgress(double amount)
    {
        if (_isComplete) return 0;

        double previousValue = _currentValue;
        _currentValue += amount;
        
        // Check if this progress completes the goal
        if (_currentValue >= _targetValue && previousValue < _targetValue)
        {
            _isComplete = true;
            return _points + _bonusPoints;
        }
        
        // points proportional to the progress made
        int earnedPoints = (int)(_points * (amount / _targetValue));
        return earnedPoints;
    }

    // Override RecordEvent method
    public override int RecordEvent()
    {
        // default incr
        return RecordProgress(1);
    }

    // Override GetDetailsString method
    public override string GetDetailsString()
    {
        double percentComplete = Math.Min((_currentValue / _targetValue) * 100, 100);
        return $"{GetStatusString()} {Name} ({Description}) -- Progress: {_currentValue:F1}/{_targetValue:F1} {_unit} ({percentComplete:F1}%)";
    }

    // override GetStringRepresentation method
    public override string GetStringRepresentation()
    {
        return $"ProgressiveGoal:{Name},{Description},{_points},{_targetValue},{_unit},{_bonusPoints},{_currentValue}";
    }
}