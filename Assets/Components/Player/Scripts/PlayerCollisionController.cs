using UnityEngine;

namespace Components.Player.Scripts
{
    /// <summary>
    /// Handles collision on Player using an overlap box
    /// </summary>
    public class PlayerCollisionController : MonoBehaviour
    {
        [Header("Box Parameters")]
        [SerializeField] private Vector3 _boxCenter;
        [SerializeField] private Vector3 _boxHalfSize;

        [Header("Invulnerability Parameters")] [SerializeField]
        private bool _hasHitWall;

        [SerializeField] private bool _hasHitDoor;

        private readonly Collider[] _hitResults = new Collider[1];
        private const string WALL_TAG = "Wall";
        private const string DOOR_TAG = "Door";

        private void Update()
        {
            var animatedBoxHalfSize = Vector3.Scale(_boxHalfSize, transform.localScale);
            var animatedCenter = transform.TransformPoint(_boxCenter);
            var hitCount = Physics.OverlapBoxNonAlloc(animatedCenter, animatedBoxHalfSize, _hitResults);

            if (hitCount > 0)
            {
                for (var i = 0; i < hitCount; i++)
                {
                    if (!_hitResults[i])
                        continue;

                    if (_hitResults[i].CompareTag(WALL_TAG) && !_hasHitWall)
                    {
                        _hasHitWall = true;
                        GameEventService.OnPlayerCollision?.Invoke();
                    }
                    else if (_hitResults[i].CompareTag(DOOR_TAG) && !_hasHitDoor)
                    {
                        _hasHitDoor = true;
                        GameEventService.OnPlayerTriggerDoorPassed?.Invoke(_hitResults[i]);
                    }
                }
            }
            else if (hitCount == 0)
            {
                _hasHitWall = false;
                _hasHitDoor = false;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            var scale = transform.lossyScale;
            Gizmos.DrawWireCube(transform.position + _boxCenter, _boxHalfSize * scale.x);
        }
    }
}