using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] PlayerSpawner playerSpawner;

    [SerializeField, Min(1)] int startingLevel = 1;

    [SerializeField] UnityEvent<int> onLevelStarted;

    [SerializeField] UnityEvent onAllLevelsCompleted;

    public int CurrentLevel { get; private set; }
    public int MaxLevel { get; private set; }

    readonly List<LevelObstacle> obstacles = new();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (playerSpawner == null)
            playerSpawner = FindAnyObjectByType<PlayerSpawner>();

        obstacles.AddRange(FindObjectsByType<LevelObstacle>());

        MaxLevel = 1;
        foreach (LevelObstacle obstacle in obstacles)
            MaxLevel = Mathf.Max(MaxLevel, obstacle.Level);

        CurrentLevel = Mathf.Clamp(startingLevel, 1, MaxLevel);

        ApplyLevel();
    }

    void Start()
    {
        onLevelStarted?.Invoke(CurrentLevel);
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void CompleteLevel()
    {
        if (CurrentLevel >= MaxLevel)
            onAllLevelsCompleted?.Invoke();
        else
            CurrentLevel++;

        StartLevel();
    }

    public void StartLevel()
    {
        ObstacleReset.ResetAll();
        ApplyLevel();
        playerSpawner.ReplacePlayer();

        onLevelStarted?.Invoke(CurrentLevel);
    }

    void ApplyLevel()
    {
        foreach (LevelObstacle obstacle in obstacles)
        {
            if (obstacle == null) continue;

            bool active = obstacle.Level <= CurrentLevel;

            obstacle.gameObject.SetActive(false);
            if (active)
                obstacle.gameObject.SetActive(true);
        }
    }
}
