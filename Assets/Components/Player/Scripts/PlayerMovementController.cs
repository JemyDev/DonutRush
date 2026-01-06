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
    [SerializeField] private float _moveDuration = 0.5f;
    [SerializeField] private Transform[] _moveTargets;

    private int _currentLaneIndex = 1;
    private bool _isMoving = false;
    private float xVelocity = 0.0f;

    private void OnEnable()
    {
        _moveActionReference.action.performed += OnMove;
    }

    private void OnDisable()
    {
        _moveActionReference.action.performed -= OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

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
        float moveTimer = 0f;

        while (moveTimer < _moveDuration)
        {
            moveTimer += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(moveTimer / _moveDuration);
            float newHorizontalPosition = Mathf.SmoothDamp(transform.position.x, target.position.x, ref xVelocity, normalizedTime);
            
            transform.position = new Vector3(newHorizontalPosition, transform.position.y, transform.position.z);

            yield return null;
        }

        _isMoving = false;
    }
}
