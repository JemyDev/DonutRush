using UnityEngine;

public class ChunkController : MonoBehaviour
{
    [SerializeField] private Transform _endAnchor;
    
    public Transform EndAnchor => _endAnchor;
    public bool IsBehind => EndAnchor.position.z <= 0;
}
