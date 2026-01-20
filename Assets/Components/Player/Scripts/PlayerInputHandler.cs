using UnityEngine;
using UnityEngine.InputSystem;

namespace Components.Player.Scripts
{
    /// <summary>
    /// Handles player input using the Input System and raises corresponding game events.
    /// </summary>
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private InputActionReference _moveActionReference;
        [SerializeField] private InputActionReference _jumpActionReference;
        
        private void OnEnable()
        {
            _moveActionReference.action.Enable();
            _moveActionReference.action.performed += OnMove;
            _jumpActionReference.action.Enable();
            _jumpActionReference.action.performed += OnJump;
        }
        
        private void OnDisable()
        {
            _moveActionReference.action.performed -= OnMove;
            _moveActionReference.action.Disable();
            _jumpActionReference.action.performed -= OnJump;
            _jumpActionReference.action.Disable();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            var moveInput = context.ReadValue<Vector2>();
            
            if (moveInput.x == 0)
                return;
            
            GameEventService.OnMoveInputPerformed?.Invoke(moveInput.x > 0 ? 1 : -1);
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            GameEventService.OnJumpInputPerformed?.Invoke();
        }
    }
}
