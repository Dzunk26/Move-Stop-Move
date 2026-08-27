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
        Instantiate(newEquipment, TF);
    }

    protected void UnequipOldEquiment(Equipment oldEquipment) {
        Destroy(oldEquipment.gameObject);
    }
}
