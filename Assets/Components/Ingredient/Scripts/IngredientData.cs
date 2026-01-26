using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "Data/IngredientData")]
public class IngredientData : ScriptableObject
{
    [SerializeField] private Color _color;
    [SerializeField] private int _score;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Material _material;
    
    public Color Color => _color;
    public int Score => _score;
    public string Name => _name;
    public Sprite Sprite => _sprite;
    public GameObject Prefab => _prefab;
    public Material Material => _material;
    
}
