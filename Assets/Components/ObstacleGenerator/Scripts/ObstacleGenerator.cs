using UnityEngine;
using System.Collections.Generic;
using Components.Data;
using Components.SODatabase;
using Services.GameEventService;
using Services.SaveService;

/// <summary>
/// Generate infinite random chunks and make them translate
/// </summary>
public class ObstacleGenerator : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _translationSpeed = 1f;
    [SerializeField] private int _activeChunksCount = 5;
    [SerializeField] private int _behindChunksCount = 2;
    [SerializeField] private bool _preventSameChunkGeneration = true;
    
    [Header("Prefabs")]
    [SerializeField] private ChunkController[] _chunkPrefabs;

    private readonly List<ChunkController> _activeChunks = new();
    private ChunkController LastChunk => _activeChunks[^1];
    private int _lastChunkIndex;
    private bool _enabled;

    private void Start()
    {
        var levelIndex = 1;
        if (SaveService.TryLoad(out SaveData saveData))
        {
            levelIndex = saveData.LevelIndex;
        }
        var parameters = ScriptableObjectDatabase.Get<LevelParametersData>("Level" + levelIndex);
        _translationSpeed = parameters.Speed;
        
        AddBaseChunk();
        GameEventService.OnGameState += HandleGameState;
        GameEventService.OnLevelChanged += HandleLevelChanged;
    }
    
    private void OnDestroy()
    {
        GameEventService.OnGameState -= HandleGameState;
        GameEventService.OnLevelChanged -= HandleLevelChanged;
    }

    private void Update()
    {
        if (!_enabled)
            return;
        
        foreach (var activeChunk in _activeChunks)
        {
            activeChunk.transform.Translate(_translationSpeed * Time.deltaTime * Vector3.back);
        }

        UpdateChunks();
    }

    private void HandleGameState(bool enterState)
    {
        _enabled = enterState;
    }
    
    private void AddBaseChunk()
    {
        for (var i = 0; i < _activeChunksCount; i++)
        {
            if (i == 0)
            {
                AddNewChunk(Vector3.zero);
                continue;
            }
            
            AddNewChunk(LastChunk.EndAnchor.position);
        }
    }

    private void AddNewChunk(Vector3 position)
    {
        var newChunkIndex = Random.Range(0, _chunkPrefabs.Length);

        if (_preventSameChunkGeneration)
        {
            for (var i = 0; i < 10; i++)
            {
                if (newChunkIndex == _lastChunkIndex)
                {
                    newChunkIndex = Random.Range(0, _chunkPrefabs.Length);
                }
            }

            _lastChunkIndex = newChunkIndex;
        }

        var newChunk = Instantiate(_chunkPrefabs[newChunkIndex], transform);
        newChunk.transform.position = position;
        _activeChunks.Add(newChunk);

        if (newChunk.HasDoorPrefab)
        {
            TriggerDoorEvent();
        }
    }

    private void UpdateChunks()
    {
        List<ChunkController> behindChunks = new();

        foreach (var activeChunk in _activeChunks)
        {
            if (activeChunk.IsBehind)
            {
                behindChunks.Add(activeChunk);
            }
        }

        if (behindChunks.Count >= _behindChunksCount)
        {
            var chunksToRemoveCount = behindChunks.Count - _behindChunksCount;

            for (var i = 0; i < chunksToRemoveCount; i++)
            {
                var chunkToRemove = behindChunks[i];
                _activeChunks.Remove(chunkToRemove);
                Destroy(chunkToRemove.gameObject);
            }
        }

        var missingChunkCount = _activeChunksCount - _activeChunks.Count;

        for (var i = 0; i < missingChunkCount; i++)
        {
            AddNewChunk(LastChunk.EndAnchor.position);
        }
    }

    private static void TriggerDoorEvent()
    {
        GameEventService.OnDoorInstantiated?.Invoke();
    }
    
    private void HandleLevelChanged(LevelParametersData newParameters)
    {
        _translationSpeed = newParameters.Speed;
    }
}
