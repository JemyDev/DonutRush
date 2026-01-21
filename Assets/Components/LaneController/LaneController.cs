using UnityEngine;
using Services.GameEventService;

namespace Components.LaneController
{
    /// <summary>
    /// Controller responsible for managing lane changes based on player input.
    /// </summary>
    public class LaneController : MonoBehaviour
    {
        [SerializeField] private Transform[] _lanes;

        private int _currentLaneIndex = 1;
        private bool _isPlayerMoving;

        private Transform CurrentTarget => _lanes[_currentLaneIndex];

        private void Awake()
        {
            GameEventService.OnMoveInputPerformed += ChangeLane;
            GameEventService.OnPlayerMoving += HandlePlayerMoving;
        }
        
        private void OnDestroy()
        {
            GameEventService.OnMoveInputPerformed -= ChangeLane;
            GameEventService.OnPlayerMoving -= HandlePlayerMoving;
        }
        
        // Here we need to track if the player is currently moving
        private void HandlePlayerMoving(bool isMoving)
        {
            _isPlayerMoving = isMoving;
        }

        private void ChangeLane(int direction)
        {
            // Prevent lane change if the player is currently moving
            if (_isPlayerMoving) return;
            
            var newLaneIndex = _currentLaneIndex + direction;
            if (newLaneIndex < 0 || newLaneIndex >= _lanes.Length)
                return;

            _currentLaneIndex = newLaneIndex;
            GameEventService.OnUpdateLaneTarget?.Invoke(CurrentTarget);
        }
    }
}