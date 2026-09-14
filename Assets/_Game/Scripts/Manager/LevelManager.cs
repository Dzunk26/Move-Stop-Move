using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager> {
    public Transform TF {
        get {
            if (tf == null) {
                tf = transform;
            }

            return tf;
        }
    }

    [SerializeField] private ListLevelSO listLevelSO;

    private LevelSO currentLevelSO;
    private Level currentLevel;
    private Transform tf;

    private void Awake() {
        OnInit();
    }

    public void OnInit() {
        
    }

    public void OnDespawn() {

    }

    public void LoadLevel(int levelID) {
        currentLevelSO = listLevelSO.GetLevelSOByLevelID(levelID);

        if (currentLevelSO == null) {
            return;
        }

        UnLoadLevel();
        currentLevel = Instantiate(currentLevelSO.GetLevelPrefab(), TF);
    }

    public void UnLoadLevel() {
        SimplePool.CollectAllActiveUnitsInAllPools();
        if (currentLevel != null && currentLevelSO != null) {
            Destroy(currentLevel.gameObject);
            currentLevel = null;
            currentLevelSO = null;
        }
    }

    public void ResetCurrentLevel() {
        int currentLevelID = PlayerProgress.Instance.GetCurrentLevelID();
        LoadLevel(currentLevelID);
    }
    
    public Level GetCurrentLevel() {
        return currentLevel;
    }

    public int GetCurrentTotalBotAmount() {
        return currentLevelSO.GetTotalBotAmount();
    }

    public int GetCurrentMaxActiveBot() {
        return currentLevelSO.GetMaxActiveBot();
    }
}