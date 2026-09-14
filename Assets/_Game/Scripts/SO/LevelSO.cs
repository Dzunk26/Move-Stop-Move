using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelSO : ScriptableObject {
    [SerializeField] private int levelID;
    [SerializeField] private int totalBotAmount; // tong so luong bot cua level
    [SerializeField] private int maxActiveBot; // so luong bot toi da xuat hien tren map
    [SerializeField] private Level levelPrefab;

    public bool MatchLevelID(int levelID) {
        return this.levelID == levelID;
    }

    public int GetTotalBotAmount() {
        return totalBotAmount;
    }

    public int GetMaxActiveBot() {
        return maxActiveBot;
    }

    public Level GetLevelPrefab() {
        return levelPrefab;
    }
}