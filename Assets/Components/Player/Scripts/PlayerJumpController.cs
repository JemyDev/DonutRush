using System.Collections;
using UnityEngine;

namespace Components.Player.Scripts
{
    /// <summary>
    /// Handles the player's jump mechanics, including jump height, duration, and animation.
    /// </summary>
    public class PlayerJumpController : MonoBehaviour
    {
        [Header("Jump Settings")]
        [SerializeField] private float _jumpDuration = 1f;
        [SerializeField] private float _jumpHeight = 2f;
        [SerializeField] private AnimationCurve _jumpCurve;
        [SerializeField] private AnimationCurve _fallCurve;
        
        [Header("Components")]
        [SerializeField] private Animator _animator;
        
        private bool _isJumping;
        
        private const string JUMP_PARAMETER = "IsJumping";
        private const string GROUNDED_PARAMETER = "Grounded";

        private void Awake()
        {
            GameEventService.OnJumpInputPerformed += HandleJump;
        }
        
        private void OnDestroy()
        {
            GameEventService.OnJumpInputPerformed -= HandleJump;
        }

        private void HandleJump()
        {
            if (!_isJumping)
                StartCoroutine(JumpCoroutine());
        }

        private IEnumerator JumpCoroutine()
        {
            _isJumping = true;
            _animator.SetBool(JUMP_PARAMETER, true);

            var jumpCoroutine = AnimateJumpCoroutine(_jumpCurve);
            yield return jumpCoroutine;

            _animator.SetBool(JUMP_PARAMETER, false);

            var fallJumpCoroutine = AnimateJumpCoroutine(_fallCurve);
            yield return fallJumpCoroutine;
            
            _animator.SetTrigger(GROUNDED_PARAMETER);
            _isJumping = false;
        }

        private IEnumerator AnimateJumpCoroutine(AnimationCurve animationCurve)
        {
            var timer = 0f;
            var halfJumpDuration = _jumpDuration / 2f;

            while (timer < halfJumpDuration)
            {
                timer += Time.deltaTime;
                var normalizedTime = Mathf.Clamp01(timer / halfJumpDuration);
                var targetHeight = animationCurve.Evaluate(normalizedTime) * _jumpHeight;

                var targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);
                transform.position = targetPosition;

                yield return null;
            }
        }
    }
}
