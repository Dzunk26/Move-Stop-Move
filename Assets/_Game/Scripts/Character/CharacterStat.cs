using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour {
    private Stat attackRange;
    private Stat attackSpeed;
    private Stat moveSpeed;
    private Stat bonusGold;

    public void OnEquipmentChanged(Equipment newEquipment, Equipment oldEquipment) {
        if (newEquipment != null) {
            float baseAttackRange = this.attackRange.GetBaseValue(); // lay base attackRange cua Character
            float bonusAttackRange = baseAttackRange + newEquipment.GetAttackRangePercentModifier() / 100 * baseAttackRange; // tinh gia tri bonus attackRange tu Equipment cong them
            this.attackRange.AddModifier(newEquipment.GetAttackRangeModifier());
            this.attackRange.AddModifier(bonusAttackRange);

            float baseAttackSpeed = this.attackSpeed.GetBaseValue(); // lay base attackSpeed cua Character
            float bonusAttackSpeed = baseAttackSpeed + newEquipment.GetAttackSpeedPercentModifier() / 100 * baseAttackSpeed; // tinh gia tri bonus attackSpeed tu Equipment cong them
            this.attackSpeed.AddModifier(newEquipment.GetAttackSpeedModifier());
            this.attackSpeed.AddModifier(bonusAttackSpeed);

            float baseMoveSpeed = this.moveSpeed.GetBaseValue(); // lay base moveSpeed cua Character
            float bonusMoveSpeed = baseMoveSpeed + newEquipment.GetMoveSpeedPercentModifier() / 100 * baseMoveSpeed; // tinh gia tri bonus moveSpeed tu Equipment cong them
            this.moveSpeed.AddModifier(newEquipment.GetAttackSpeedModifier());
            this.moveSpeed.AddModifier(bonusMoveSpeed);

            float bonusGold= newEquipment.GetGoldPercentModifier() / 100; // tinh gia tri bonus gold tu Equipment cong them
            this.bonusGold.AddModifier(bonusGold);
        }

        if (oldEquipment != null) {
            float baseAttackRange = this.attackRange.GetBaseValue(); // lay base attackRange cua Character
            float bonusAttackRange = baseAttackRange + newEquipment.GetAttackRangePercentModifier() / 100 * baseAttackRange; // tinh gia tri bonus attackRange tu Equipment cong them
            this.attackRange.RemoveModifier(newEquipment.GetAttackRangeModifier());
            this.attackRange.RemoveModifier(bonusAttackRange);

            float baseAttackSpeed = this.attackSpeed.GetBaseValue(); // lay base attackSpeed cua Character
            float bonusAttackSpeed = baseAttackSpeed + newEquipment.GetAttackSpeedPercentModifier() / 100 * baseAttackSpeed; // tinh gia tri bonus attackSpeed tu Equipment cong them
            this.attackSpeed.RemoveModifier(newEquipment.GetAttackSpeedModifier());
            this.attackSpeed.RemoveModifier(bonusAttackSpeed);

            float baseMoveSpeed = this.moveSpeed.GetBaseValue(); // lay base moveSpeed cua Character
            float bonusMoveSpeed = baseMoveSpeed + newEquipment.GetMoveSpeedPercentModifier() / 100 * baseMoveSpeed; // tinh gia tri bonus moveSpeed tu Equipment cong them
            this.moveSpeed.RemoveModifier(newEquipment.GetAttackSpeedModifier());
            this.moveSpeed.RemoveModifier(bonusMoveSpeed);

            float bonusGold = newEquipment.GetGoldPercentModifier() / 100; // tinh gia tri bonus gold tu Equipment cong them
            this.bonusGold.RemoveModifier(bonusGold);
        }
    }
}