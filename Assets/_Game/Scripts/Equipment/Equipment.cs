using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType {
    Weapon,
    Hat,
    Pant,
    Accessory
}

public class Equipment : MonoBehaviour {
    [SerializeField] private EquipmentType equipmentType;
    [SerializeField] private float attackRangeModifier;
    [SerializeField] private float attackSpeedModifier;
    [SerializeField] private float moveSpeedModifier;
    [SerializeField] private int goldPercentModifier;
    [SerializeField] private int attackRangePercentModifier;
    [SerializeField] private int attackSpeedPercentModifier;
    [SerializeField] private int moveSpeedPercentModifier;

    public EquipmentType GetEquipmentType() {
        return equipmentType;
    }

    public float GetAttackRangeModifier() {
        return attackRangeModifier;
    }

    public float GetAttackSpeedModifier() {
        return attackSpeedModifier; 
    }

    public float GetMoveSpeedModifier() {
        return moveSpeedModifier;
    }

    public int GetGoldPercentModifier() {
        return goldPercentModifier;
    }

    public int GetAttackRangePercentModifier() {
        return attackRangePercentModifier;
    }

    public int GetAttackSpeedPercentModifier() {
        return attackSpeedPercentModifier;
    }

    public int GetMoveSpeedPercentModifier() {
        return moveSpeedPercentModifier;
    }
}