using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryVisual : EquipmentVisual {
    public override void OnChangeEquipment(Equipment newEquipment) {
        UnequipOldEquiment(currentEquipment);

        EquipNewEquipment(newEquipment);
    }
}
