using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PowerUpSpawner : NetworkBehaviour {
    public GameObject[] powerupPrefabArray = new GameObject[2]; // variable for power up objects
    private GameObject powerupPrefab;
    private Vector3[] spawnPositions = new Vector3[4] { 
        new Vector3(40.8f, 70.0f, -281.8f),
        new Vector3(264, 70, -14),
        new Vector3(80, 70, 231),
        new Vector3(-79, 70, 60)
    };

    public override void OnStartServer()
    {
        Quaternion spawnRotation = Quaternion.Euler(45, 45, 45);
        for (int i = 0; i < 4; i++)
        {
            powerupPrefab = powerupPrefabArray[i % 2];
            GameObject powerup = (GameObject)Instantiate(powerupPrefab, spawnPositions[i], spawnRotation);
            NetworkServer.Spawn(powerup);
        }
    }
}
