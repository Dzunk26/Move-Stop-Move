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
    [SerializeField] protected EquipmentType equipmentType;
    [SerializeField] protected float attackRangeModifier;
    [SerializeField] protected float attackSpeedModifier;
    [SerializeField] protected float moveSpeedModifier;
    [SerializeField] protected int goldPercentModifier;
    [SerializeField] protected int attackRangePercentModifier;
    [SerializeField] protected int attackSpeedPercentModifier;
    [SerializeField] protected int moveSpeedPercentModifier;

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