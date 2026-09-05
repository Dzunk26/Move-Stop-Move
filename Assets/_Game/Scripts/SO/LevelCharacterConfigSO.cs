using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelCharacterConfigSO : ScriptableObject {
    [SerializeField] private int levelID;
    [SerializeField] private int requiredPointLevelUp;
    [SerializeField] private float scaleModifier;
    [SerializeField] private float attackRangeModifier;
    [SerializeField] private int pointReward;

    public bool MatchLevelID(int levelID) {
        return this.levelID == levelID;
    }

    public int GetRequiredPointLevelUp() {
        return this.requiredPointLevelUp;
    }

    public float GetScaleModifier() {
        return scaleModifier;
    }

    public float GetAttackRangeModifier() {
        return attackRangeModifier; 
    }

    public int GetPointReward() {
        return pointReward;
    }
}