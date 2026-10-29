using Unity.Cinemachine;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] Player playerPrefab;
    [SerializeField] Transform playerSpawnPoint;

    [SerializeField] Player currentPlayer;

    [SerializeField] CinemachineCamera cinemachineCamera;

    [SerializeField] Transform entranceDoor;

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

        currentPlayer.EntranceDoor = entranceDoor;
        if (entranceDoor.GetComponentInChildren<Animator>() != null)
            entranceDoor.GetComponentInChildren<Animator>().SetTrigger("Open");

        cinemachineCamera.Follow = currentPlayer.transform;
    }

    public void ReplacePlayer()
    {
        if (currentPlayer != null)
            Destroy(currentPlayer.gameObject);

        SpawnPlayer();
    }

    private void OnDrawGizmos()
    {
        if (entranceDoor == null) return;
        Transform spawn = playerSpawnPoint != null ? playerSpawnPoint : transform;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(entranceDoor.position, spawn.position);
        Gizmos.DrawWireSphere(entranceDoor.position, 0.15f);
    }
}
