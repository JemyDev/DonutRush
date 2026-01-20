using System;
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
    [SerializeField] private InputActionReference _jumpActionReference;

    [Header("Jump Parameters")]
    [SerializeField] private float _jumpDuration = 1f;
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private AnimationCurve _fallCurve;

    [Header("Movement Parameters")]
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private Transform[] _moveTargets;

    [Header("Components")]
    [SerializeField] private Animator _animator;

    private int _currentLaneIndex = 1;
    private bool _isMoving = false;
    private bool _isJumping = false;
    private float _currentPositionY;
    private const float THRESHOLD = 0.01f;
    private const string JUMP_PARAMETER = "IsJumping";
    private const string GROUNDED_PARAMETER = "Grounded";

    private void Start()
    {
        _currentPositionY = transform.position.y;
        GameEventService.OnGameOver += HandleGameOver;
    }

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

    private void OnDestroy()
    {
        GameEventService.OnGameOver -= HandleGameOver;
    }
    
    private void HandleGameOver()
    {
        _isMoving = false;
        _isJumping = false;
        _animator.enabled = false;
        StopAllCoroutines();
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

    private void OnJump(InputAction.CallbackContext context)
    {
        if (_isJumping)
            return;

        StartCoroutine(JumpCoroutine());
    }

    private IEnumerator MoveCoroutine(Transform target)
    {
        _isMoving = true;
        var velocity = Vector3.zero;
        Vector3 targetPosition = new(target.position.x, target.position.y + _currentPositionY, transform.position.z);

        while (Vector3.Distance(transform.position, targetPosition) > THRESHOLD)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, _moveSpeed);
            yield return null;
        }

        transform.position = targetPosition;
        _isMoving = false;
    }

    private IEnumerator JumpCoroutine()
    {
        _isJumping = true;
        _animator.SetBool(JUMP_PARAMETER, true);

        var halfJumpDuration = _jumpDuration / 2;

        var jumpTimer = 0f;

        while (jumpTimer < halfJumpDuration)
        {
            jumpTimer += Time.deltaTime;
            var normalizedTime = Mathf.Clamp01(jumpTimer / halfJumpDuration);
            var targetHeight = _jumpCurve.Evaluate(normalizedTime) * _jumpHeight;

            var targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);
            transform.position = targetPosition;

            yield return null;
        }
        
        _animator.SetBool(JUMP_PARAMETER, false);

        jumpTimer = 0f;

        while (jumpTimer < halfJumpDuration)
        {
            jumpTimer += Time.deltaTime;
            var normalizedTime = Mathf.Clamp01(jumpTimer / halfJumpDuration);
            
            var targetHeight = _fallCurve.Evaluate(normalizedTime) * _jumpHeight;
            var targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);
            transform.position = targetPosition;

            yield return null;
        }
        
        _animator.SetTrigger(GROUNDED_PARAMETER);
        _isJumping = false;
    }
}
