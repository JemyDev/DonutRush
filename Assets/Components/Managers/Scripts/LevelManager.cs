using Components.Data;
using UnityEngine;

namespace Components.Managers
{
    public class LevelManager
    {
        private readonly LevelParametersData _levelParametersData;
        private int _currentLevel;
        
        private const float MAX_SPEED = 15f;
        private const float SPEED_INCREMENT_PER_LEVEL = 0.75f;
        private const int MAX_INGREDIENTS_PER_ORDER = 3;
        private const int MAX_INGREDIENTS_PER_ORDER_LINE = 8;
        
        public LevelManager(LevelParametersData levelParametersData)
        {
            _levelParametersData = levelParametersData;
        }

        public LevelParametersInfo AdvanceLevel()
        {
            _currentLevel++;

            return UpdateLevelParameters();
        }

        private LevelParametersInfo UpdateLevelParameters()
        {
            // Speed: linear growth per level
            var speed = _levelParametersData.Speed + (_currentLevel - 1) * SPEED_INCREMENT_PER_LEVEL;
            speed = Mathf.Min(speed, MAX_SPEED);

            // Max ingredients per order: +1 every 3 levels
            var maxIngredientsPerOrder = _levelParametersData.MaxIngredientsPerOrder + (_currentLevel - 1) / 3;
            maxIngredientsPerOrder = Mathf.Min(maxIngredientsPerOrder, MAX_INGREDIENTS_PER_ORDER);

            // Min ingredients per line: +1 every 4 levels
            var minIngredientsPerOrderLine = _levelParametersData.MinIngredientsPerOrderLine + (_currentLevel - 1) / 4;

            // Max ingredients per line: +1 every 2 levels
            var maxIngredientsPerOrderLine = _levelParametersData.MaxIngredientsPerOrderLine + (_currentLevel - 1) / 2;
            maxIngredientsPerOrderLine = Mathf.Min(maxIngredientsPerOrderLine, MAX_INGREDIENTS_PER_ORDER_LINE);

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
}