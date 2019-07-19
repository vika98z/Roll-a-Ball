using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] _spawnPoints;

    public void SpawnPlayers(List<PlayerController> players)
    {
        int i = 0;
        
        foreach (var player in players)
            player.transform.position = _spawnPoints[i++].position;
    }

    public void SpawnPlayer(PlayerController player)
    {
        var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        player.transform.position = spawnPoint.position;
    }
}