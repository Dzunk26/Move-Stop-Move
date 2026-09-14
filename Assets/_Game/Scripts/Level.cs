using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    [SerializeField] private List<Transform> spawnPoints;

    public List<Transform> GetListSpawnPoint() {
        return spawnPoints;
    }
}