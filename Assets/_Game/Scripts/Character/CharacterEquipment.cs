using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour {
    [SerializeField] private Character character;

    [SerializeField] private Equipment[] currentEquipments;

    public void OnInit() {
        int numSlots = System.Enum.GetNames(typeof(EquipmentType)).Length;
        currentEquipments = new Equipment[numSlots];
    }

    public void OnDespawn() {

    }

    public void OnEquipmentChanged(Equipment newEquipment) {
        int slotIndex = (int)newEquipment.GetEquipmentType();

        Equipment oldEquipment = null;

        if (currentEquipments[slotIndex] != null) {
            oldEquipment = currentEquipments[slotIndex];
        }

        //characterStat.OnEquipmentChanged(newEquipment, oldEquipment);

        currentEquipments[slotIndex] = newEquipment;
    }

    public void Unequip(int slotIndex) {
        if (currentEquipments[slotIndex] != null) {
            Equipment oldEquipment = currentEquipments[slotIndex];
            currentEquipments[slotIndex] = null;

            //characterStat.OnEquipmentChanged(null, oldEquipment);
        }
    }

    public Equipment GetEquipmentByType(EquipmentType equipmentType) {
        int slotIndex = (int)equipmentType;

        Equipment oldEquipment = null;

        if (currentEquipments[slotIndex] != null) {
            oldEquipment = currentEquipments[slotIndex];
        }

        return oldEquipment;
    }
}