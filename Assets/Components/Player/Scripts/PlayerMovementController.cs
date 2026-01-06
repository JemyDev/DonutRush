using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handle player's movements by listening to new Input System inputs and update player's position accordingly.
/// </summary>
public class PlayerMovementController : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputActionReference _moveActionReference;

    [Header("Movement Parameters")]
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private Transform[] _moveTargets;

    private int _currentLaneIndex = 1;
    private bool _isMoving = false;
    private const float THRESHOLD = 0.01f;

    private void OnEnable()
    {
        _moveActionReference.action.Enable();
        _moveActionReference.action.performed += OnMove;
    }

    private void OnDisable()
    {
        _moveActionReference.action.performed -= OnMove;
        _moveActionReference.action.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        var moveInput = context.ReadValue<Vector2>();

        if (moveInput.x > 0)
        {
            // Here we avoid running multiple Coroutine if the player is performing a movement
            if (_isMoving)
                return;

            // And we are checking if we are not outside our target array then we ensure our player stays in the lanes we defined
            if (_currentLaneIndex >= _moveTargets.Length - 1)
                return;

            _currentLaneIndex++;
            StartCoroutine(MoveCoroutine(_moveTargets[_currentLaneIndex]));
        }
        else if (moveInput.x < 0)
        {
            if (_isMoving)
                return;

            if (_currentLaneIndex <= 0)
                return;

            _currentLaneIndex--;
            StartCoroutine(MoveCoroutine(_moveTargets[_currentLaneIndex]));
        }
    }

    private IEnumerator MoveCoroutine(Transform target)
    {
        _isMoving = true;
        var velocity = Vector3.zero;
        Vector3 targetPosition = new(target.position.x, transform.position.y, transform.position.z);

        while (Vector3.Distance(transform.position, targetPosition) > THRESHOLD)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, _moveSpeed);
            yield return null;
        }

        transform.position = targetPosition;
        _isMoving = false;
    }
}
