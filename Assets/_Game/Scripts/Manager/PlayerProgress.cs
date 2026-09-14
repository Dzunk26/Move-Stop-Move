using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerProgress : Singleton<PlayerProgress>, IDataPersistence {
    [SerializeField] private Player player;

    [SerializeField] private ListHatSO hatDatas;
    [SerializeField] private ListWeaponSO weaponDatas;
    [SerializeField] private ListAccessorySO accessoryDatas;
    [SerializeField] private ListPantSO pantDatas;

    private int currentLevelID = 1;
    private int coin = 0;

    private int equippedHatID;
    private int equippedWeaponID;
    private int equippedAccessoryID;
    private int equippedPantID;

    private List<int> unlockedHatIDs;
    private List<int> unlockedWeaponIDs;
    private List<int> unlockedAccessoryIDs;
    private List<int> unlockedPantIDs;

    public void OnInit() {
        Hat currentHat = (Hat)hatDatas.GetByID(equippedHatID)?.GetEquipmentPrefab();
        Weapon currentWeapon = (Weapon)weaponDatas.GetByID(equippedWeaponID)?.GetEquipmentPrefab();
        Accessory currentAccessory = (Accessory)accessoryDatas.GetByID(equippedWeaponID)?.GetEquipmentPrefab();
        Pant currentPant = (Pant)pantDatas.GetByID(equippedPantID)?.GetEquipmentPrefab();

        player.OnEquipmentChanged(currentHat);
        player.OnEquipmentChanged(currentWeapon);
        player.OnEquipmentChanged(currentAccessory);
        player.OnEquipmentChanged(currentPant);
    }

    public void LoadData(GameData gameData) {
        this.currentLevelID = gameData.levelID;
        this.coin = gameData.coin;

        this.equippedHatID = gameData.equippedHatID;
        this.equippedWeaponID = gameData.equippedWeaponID;
        this.equippedAccessoryID = gameData.equippedAccessoryID;
        this.equippedPantID = gameData.equippedPantID;

        this.unlockedHatIDs = gameData.unlockedHatIDs;
        this.unlockedWeaponIDs = gameData.unlockedWeaponIDs;
        this.unlockedAccessoryIDs = gameData.unlockedAccessoryIDs;
        this.unlockedPantIDs = gameData.unlockedPantIDs;
    }

    public void SaveData(ref GameData gameData) {
        gameData.levelID = currentLevelID;
        gameData.coin = coin;

        gameData.equippedHatID = this.equippedHatID;
        gameData.equippedWeaponID = this.equippedWeaponID;
        gameData.equippedAccessoryID = this.equippedAccessoryID;
        gameData.equippedPantID = this.equippedPantID;

        gameData.unlockedHatIDs = this.unlockedHatIDs;
        gameData.unlockedWeaponIDs = this.unlockedWeaponIDs;
        gameData.unlockedAccessoryIDs = this.unlockedAccessoryIDs;
        gameData.unlockedPantIDs = this.unlockedPantIDs;
    }

    public void NextLevel() {
        currentLevelID++;
    }

    public int GetCurrentLevelID() {
        return currentLevelID;
    }

    public bool CanAfford(int price) {
        return coin >= price;
    }

    public void SpendCoins(int amount) {
        coin -= amount;
    }

    public void UnlockNewItem(EquipmentSO equipmentSO) {
        int equipmentID = equipmentSO.GetID();

        switch (equipmentSO) {
            case HatSO:
                unlockedHatIDs.Add(equipmentID);
                break;
            case WeaponSO:
                unlockedWeaponIDs.Add(equipmentID);
                break;
            case AccessorySO:
                unlockedAccessoryIDs.Add(equipmentID);
                break;
            case PantSO:
                unlockedPantIDs.Add(equipmentID);
                break;
        }
    }

    public bool IsUnlocked(EquipmentSO equipmentSO) {
        int equipmentID = equipmentSO.GetID();

        switch (equipmentSO) {
            default:
                return false;
            case HatSO:
                return unlockedHatIDs.Contains(equipmentID);
            case WeaponSO:
                return unlockedWeaponIDs.Contains(equipmentID);
            case AccessorySO:
                return unlockedAccessoryIDs.Contains(equipmentID);
            case PantSO:
                return unlockedPantIDs.Contains(equipmentID);
        }
    }

    public void Equip(EquipmentSO equipmentSO) {
        if (equipmentSO == null) return;

        int equipmentID = equipmentSO.GetID();

        switch (equipmentSO) {
            case HatSO:
                equippedHatID = equipmentID;
                break;
            case WeaponSO:
                equippedWeaponID = equipmentID;
                break;
            case AccessorySO:
                equippedAccessoryID = equipmentID;
                break;
            case PantSO:
                equippedPantID = equipmentID;
                break;
        }

        player.OnEquipmentChanged(equipmentSO.GetEquipmentPrefab());
    }

    public bool IsEquipped(EquipmentSO equipmentSO) {
        int equipmentID = equipmentSO.GetID();

        switch (equipmentSO) {
            default:
                return false;
            case HatSO:
                return equipmentID == equippedHatID;
            case WeaponSO:
                return equipmentID == equippedWeaponID;
            case AccessorySO:
                return equipmentID == equippedAccessoryID;
            case PantSO:
                return equipmentID == equippedPantID;
        }
    }
}