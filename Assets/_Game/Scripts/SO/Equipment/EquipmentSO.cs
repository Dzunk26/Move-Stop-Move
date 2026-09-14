using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentSO : ScriptableObject {
    [SerializeField] protected string equipmentName;
    [SerializeField] protected int goldRequire;
    [SerializeField] protected Sprite equipmentIcon;
    [SerializeField] protected Equipment equipmentPrefab;
    [SerializeField] protected string statDescription;

    public abstract int GetID();
    
    public string GetEquipmentName() {
        return equipmentName;
    }

    public int GetGoldRequire() {
        return goldRequire;
    }

    public Sprite GetEquipmentIcon() {
        return equipmentIcon;
    }

    public Equipment GetEquipmentPrefab() {
        return equipmentPrefab;
    }

    public string GetStatDescription() {
        return statDescription;
    }
}