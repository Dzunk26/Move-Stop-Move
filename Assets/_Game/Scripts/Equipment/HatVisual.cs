using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatVisual : EquipmentVisual {
    public override void OnChangeEquipment(Equipment newEquipment) {
        UnequipOldEquiment(currentEquipment);

        EquipNewEquipment(newEquipment);
    }
}