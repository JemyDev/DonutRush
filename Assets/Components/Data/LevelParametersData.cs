using UnityEngine;

namespace Components.Data
{
    [CreateAssetMenu(fileName = "LevelParameters", menuName = "Data/LevelParameters")]
    public class LevelParametersData : ScriptableObject
    {
        [SerializeField] private int _playerLife = 3;
        [SerializeField] private float _speed;
        [SerializeField] private int _maxIngredientsPerOrder = 1;
        [SerializeField] private int _minIngredientsPerOrderLine = 1;
        [SerializeField] private int _maxIngredientsPerOrderLine = 5;
        
        public int PlayerLife => _playerLife;
        public float Speed => _speed;
        public int MaxIngredientsPerOrder => _maxIngredientsPerOrder;
        public int MinIngredientsPerOrderLine => _minIngredientsPerOrderLine;
        public int MaxIngredientsPerOrderLine => _maxIngredientsPerOrderLine;
    }
}