using UnityEngine;

public class ChunkController : MonoBehaviour
{
    [SerializeField] private Transform _endAnchor;
    [SerializeField] private DoorController _doorPrefab;
    
    public Transform EndAnchor => _endAnchor;
    public bool IsBehind => EndAnchor.position.z <= 0;
    public bool HasDoorPrefab => _doorPrefab != null;
}
