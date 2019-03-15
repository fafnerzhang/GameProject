using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    public Transform RespawnPoint;
    public Transform PlayerPrefab;

    void Start()
    {
        Instantiate(PlayerPrefab, RespawnPoint.position, RespawnPoint.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
