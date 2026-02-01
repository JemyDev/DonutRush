using System.Collections;
using Services.GameEventService;
using UnityEngine;

/// <summary>
/// Controls the behavior of obstacles that fall and raise periodically.
/// </summary>
public class ObstacleController : MonoBehaviour
{
    [Header("Obstacle Settings")]
    [SerializeField] private float _fallDuration = 0.5f;
    [SerializeField] private float _delayBetweenFalls = 0.5f;
    [SerializeField] private Vector2 _initialDelayRange = new Vector2(0f, 1f);
    [SerializeField] private AnimationCurve _fallCurve;
    [SerializeField] private AnimationCurve _raiseCurve;
    
    [Header("Prefabs")]
    [SerializeField] private GameObject[] _obstaclePrefabs;
    
    private bool _isFalling;
    private bool _isInitialized;
    private Vector3 _initialRotation;

    private void Start()
    {
        if (_obstaclePrefabs.Length == 0)
        {
            Debug.LogWarning("No obstacle prefabs assigned!");
            return;
        }
        
        _initialRotation = transform.rotation.eulerAngles;
        var randomIndex = Random.Range(0, _obstaclePrefabs.Length);
        Instantiate(_obstaclePrefabs[randomIndex], transform);

        StartCoroutine(InitialDelayCoroutine());
    }

    private void Update()
    {
        if (!_isInitialized || _isFalling) return;
        StartCoroutine(FallCoroutine());
    }

    private IEnumerator InitialDelayCoroutine()
    {
        var randomDelay = Random.Range(_initialDelayRange.x, _initialDelayRange.y);
        yield return new WaitForSeconds(randomDelay);
        _isInitialized = true;
    }

    private IEnumerator FallCoroutine()
    {
        _isFalling = true;

        var fallCoroutine = AnimateFallCoroutine(_fallCurve);
        yield return fallCoroutine;
        
        var raiseCoroutine = AnimateFallCoroutine(_raiseCurve);
        yield return raiseCoroutine;
        
        yield return new WaitForSeconds(_delayBetweenFalls);
        
        _isFalling = false;
    }

    private IEnumerator AnimateFallCoroutine(AnimationCurve animationCurve)
    {
        var timer = 0f;

        var halfFallDuration = _fallDuration / 2f;

        while (timer < halfFallDuration)
        {
            timer += Time.deltaTime;
            var normalizedTime = Mathf.Clamp01(timer / halfFallDuration);
            var curveValue = animationCurve.Evaluate(normalizedTime);
            var targetRotation = Quaternion.Euler(_initialRotation.x, _initialRotation.y, curveValue * 90f);
            transform.rotation = targetRotation;

            yield return null;
        }
    }
}
