using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ListLevelCharacterConfigSO : ScriptableObject {
    [SerializeField] private List<LevelCharacterConfigSO> levelCharacterSOs;

    public LevelCharacterConfigSO GetLevelGrowthByLevelID(int levelID) {
        LevelCharacterConfigSO levelTarget = null;

        foreach (LevelCharacterConfigSO levelCharacter in levelCharacterSOs) {
            if (levelCharacter.MatchLevelID(levelID)) {
                levelTarget = levelCharacter;
            }
        }

        return levelTarget;
    }
}