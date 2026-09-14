using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ListLevelSO : ScriptableObject {
    [SerializeField] private List<LevelSO> levelSOs;

    public LevelSO GetLevelSOByLevelID(int levelID) {
        LevelSO targetLevel = null;

        foreach (LevelSO levelSO in levelSOs) {
            if (levelSO.MatchLevelID(levelID)) {
                targetLevel = levelSO; 
                break;
            }
        }

        return targetLevel;
    }
}