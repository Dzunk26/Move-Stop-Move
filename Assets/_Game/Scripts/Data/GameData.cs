using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {
    public int levelID;
    public int coin;
    public bool VibrationToggle;
    public bool MusicToggle;

    public int equippedHatID;
    public int equippedWeaponID;
    public int equippedAccessoryID;
    public int equippedPantID;

    public List<int> unlockedHatIDs;
    public List<int> unlockedWeaponIDs;
    public List<int> unlockedAccessoryIDs;
    public List<int> unlockedPantIDs;

    public GameData() {
        levelID = 1;
        coin = 1;
        VibrationToggle = true;
        MusicToggle = true;

        equippedHatID = (int) Constant.DEAFAULT_HAT_TYPE;
        equippedWeaponID = (int) Constant.DEAFAULT_WEAPON_TYPE;
        equippedAccessoryID = (int) Constant.DEAFAULT_ACCESSORY_TYPE;
        equippedPantID = (int)Constant.DEAFAULT_PANT_TYPE;

        unlockedHatIDs = new List<int>();
        unlockedWeaponIDs = new List<int>();
        unlockedAccessoryIDs = new List<int>();
        unlockedPantIDs = new List<int>();
    }
}