using UnityEngine;

namespace Components.Data
{
    [CreateAssetMenu(fileName = "LevelParameters", menuName = "Data/LevelParameters")]
    public class LevelParametersData : ScriptableObject
    {
        [Header("Health parameter")]
        [SerializeField] private int _playerLife = 3;
        
        [Header("Speed parameters")]
        [SerializeField] private float _baseSpeed;
        [SerializeField] private float _speedIncrementPerLevel = 0.75f;
        [SerializeField] private float _maxSpeed = 15f;
        
        [Header("Order parameters")]
        [SerializeField] private int _minIngredientsPerOrder = 1;
        [SerializeField] private int _maxIngredientsPerOrder = 1;
        [SerializeField] private int _minIngredientsPerOrderLine = 1;
        [SerializeField] private int _maxIngredientsPerOrderLine = 5;
        [SerializeField] private float _orderTimeLimit = 60f;
        
        
        public int PlayerLife => _playerLife;
        public float BaseSpeed => _baseSpeed;
        public float SpeedIncrementPerLevel => _speedIncrementPerLevel;
        public float MaxSpeed => _maxSpeed;
        public int MinIngredientsPerOrder => _minIngredientsPerOrder;
        public int MaxIngredientsPerOrder => _maxIngredientsPerOrder;
        public int MinIngredientsPerOrderLine => _minIngredientsPerOrderLine;
        public int MaxIngredientsPerOrderLine => _maxIngredientsPerOrderLine;
        public float OrderTimeLimit => _orderTimeLimit;
    }
}