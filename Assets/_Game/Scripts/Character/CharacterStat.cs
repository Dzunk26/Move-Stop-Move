using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour {
    public Stat attackRange;
    public Stat attackSpeed;
    public Stat moveSpeed;
    public Stat bonusGold;

    public void OnEquipmentChanged(Equipment newEquipment, Equipment oldEquipment) {
        if (newEquipment != null) {
            AddModifiers(newEquipment);
        }

        if (oldEquipment != null) {
            RemoveModifiers(oldEquipment);
        }
    }

    private void AddModifiers(Equipment equipment) {
        attackRange.AddFlatModifier(equipment.GetAttackRangeModifier());
        attackRange.AddPercentModifier(equipment.GetAttackRangePercentModifier() / 100);

        attackSpeed.AddFlatModifier(equipment.GetAttackSpeedModifier());
        attackSpeed.AddPercentModifier(equipment.GetAttackSpeedPercentModifier() / 100);

        moveSpeed.AddFlatModifier(equipment.GetMoveSpeedModifier());
        moveSpeed.AddPercentModifier(equipment.GetMoveSpeedPercentModifier() / 100);

        bonusGold.AddPercentModifier(equipment.GetGoldPercentModifier() / 100);
    }

    private void RemoveModifiers(Equipment equipment) {
        attackRange.RemoveFlatModifier(equipment.GetAttackRangeModifier());
        attackRange.RemovePercentModifier(equipment.GetAttackRangePercentModifier() / 100);

        attackSpeed.RemoveFlatModifier(equipment.GetAttackSpeedModifier());
        attackSpeed.RemovePercentModifier(equipment.GetAttackSpeedPercentModifier() / 100);

        moveSpeed.RemoveFlatModifier(equipment.GetMoveSpeedModifier());
        moveSpeed.RemovePercentModifier(equipment.GetMoveSpeedPercentModifier() / 100);

        bonusGold.RemovePercentModifier(equipment.GetGoldPercentModifier() / 100);
    }
}