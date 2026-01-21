using System.Collections;
using UnityEngine;
using Services.GameEventService;

namespace Components.Player.Scripts
{
    /// <summary>
    /// Handles the player's movement between lanes based on target positions.
    /// </summary>
    public class PlayerMoveController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float _maxSpeed = 1f;
        [SerializeField] private float _smoothTime = 0.1f;
        
        private const float THRESHOLD = 0.01f;

        private void Awake()
        {
            GameEventService.OnUpdateLaneTarget += HandleMove;
        }

        private void OnDestroy()
        {
            GameEventService.OnUpdateLaneTarget -= HandleMove;
        }
        
        private void HandleMove(Transform target)
        {
            StopAllCoroutines();
            StartCoroutine(MoveCoroutine(target));
        }

        private IEnumerator MoveCoroutine(Transform target)
        {
            GameEventService.OnPlayerMoving?.Invoke(true);
            var velocity = Vector3.zero;
            Vector3 targetPosition = new(target.position.x, target.position.y, transform.position.z);
            
            while (Vector3.Distance(transform.position, targetPosition) > THRESHOLD)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, _smoothTime, _maxSpeed);
                yield return null;
            }

            transform.position = targetPosition;
            GameEventService.OnPlayerMoving?.Invoke(false);
        }
    }
}
