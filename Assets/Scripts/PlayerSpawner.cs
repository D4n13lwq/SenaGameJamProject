using Unity.Cinemachine;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] Player playerPrefab;
    [SerializeField] Transform playerSpawnPoint;

    [SerializeField] Player currentPlayer;

    [SerializeField] CinemachineCamera cinemachineCamera;

    private void Start()
    {
        if (playerSpawnPoint == null)
            playerSpawnPoint = transform;

        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        currentPlayer = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        currentPlayer.Initialize();
        cinemachineCamera.Follow = currentPlayer.transform;
    }
}
