using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentVisual : MonoBehaviour{
    public Transform TF {
        get {
            if (tf == null) {
                tf = transform;
            }
            return tf;
        }
    }

    [SerializeField] protected EquipmentType equipmentType;

    protected Equipment currentEquipment;
    private Transform tf;

    public abstract void OnChangeEquipment(Equipment newEquipment);

    public bool IsMatchEquipmentType(EquipmentType equipmentType) {
        return this.equipmentType == equipmentType;
    }

    protected void EquipNewEquipment(Equipment newEquipment) {
        if (newEquipment == null) return;

        currentEquipment = Instantiate(newEquipment, TF);
    }

    protected void UnequipOldEquiment(Equipment oldEquipment) {
        if (currentEquipment == null) return;

        Destroy(oldEquipment.gameObject);
        currentEquipment = null;
    }
}
