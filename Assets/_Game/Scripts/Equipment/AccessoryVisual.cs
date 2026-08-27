using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryVisual : EquipmentVisual {
    public override void OnChangeEquipment(Equipment newEquipment) {
        if (currentEquipment != null) {
            UnequipOldEquiment(currentEquipment);
        }

        currentEquipment = newEquipment;
        EquipNewEquipment(newEquipment);
    }
}
