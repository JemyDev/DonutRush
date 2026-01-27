namespace Components.Data
{
    public readonly struct LevelParametersInfo
    {
        public readonly int Level;
        public readonly float Speed;
        public readonly int MaxIngredientsPerOrder;
        public readonly int MinIngredientsPerOrderLine;
        public readonly int MaxIngredientsPerOrderLine;

        public LevelParametersInfo(
            int level,
            float speed,
            int maxIngredientsPerOrder,
            int minIngredientsPerOrderLine,
            int maxIngredientsPerOrderLine)
        {
            Level = level;
            Speed = speed;
            MaxIngredientsPerOrder = maxIngredientsPerOrder;
            MinIngredientsPerOrderLine = minIngredientsPerOrderLine;
            MaxIngredientsPerOrderLine = maxIngredientsPerOrderLine;
        }
    }
}
