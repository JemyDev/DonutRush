using Components.Data;
using UnityEngine;

public class LevelTracker
{
    private readonly LevelParametersData _levelParametersData;
    private int _currentLevel;
    
    public LevelTracker(LevelParametersData levelParametersData)
    {
        _levelParametersData = levelParametersData;
        _currentLevel = 1;
    }

    public LevelParametersInfo GetCurrentLevelParameters()
    {
        return CalculateLevelParameters();
    }

    public LevelParametersInfo AdvanceLevel()
    {
        _currentLevel++;

        return CalculateLevelParameters();
    }

    private LevelParametersInfo CalculateLevelParameters()
    {
        // Speed: linear growth per level
        var speed = _levelParametersData.BaseSpeed + (_currentLevel - 1) * _levelParametersData.SpeedIncrementPerLevel;
        speed = Mathf.Min(speed, _levelParametersData.MaxSpeed);

        // Max ingredients per order: starts at min, +1 every 3 levels, capped at max
        var maxIngredientsPerOrder = _levelParametersData.MinIngredientsPerOrder + (_currentLevel - 1) / 3;
        maxIngredientsPerOrder = Mathf.Min(maxIngredientsPerOrder, _levelParametersData.MaxIngredientsPerOrder);

        // Min ingredients per line: +1 every 4 levels
        var minIngredientsPerOrderLine = _levelParametersData.MinIngredientsPerOrderLine + (_currentLevel - 1) / 4;

        // Max ingredients per line: +1 every 2 levels
        var maxIngredientsPerOrderLine = _levelParametersData.MaxIngredientsPerOrderLine + (_currentLevel - 1) / 2;
        maxIngredientsPerOrderLine = Mathf.Min(maxIngredientsPerOrderLine, _levelParametersData.MaxIngredientsPerOrderLine);

        // Ensure min <= max
        minIngredientsPerOrderLine = Mathf.Min(minIngredientsPerOrderLine, maxIngredientsPerOrderLine);

        return new LevelParametersInfo(
            _currentLevel,
            speed,
            maxIngredientsPerOrder,
            minIngredientsPerOrderLine,
            maxIngredientsPerOrderLine
        );
    }
}