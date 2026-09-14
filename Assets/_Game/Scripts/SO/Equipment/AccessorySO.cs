using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum AccessoryType {
    None,
    InfinityShield,
    NormalShield
}

[CreateAssetMenu()]
public class AccessorySO : EquipmentSO {
    [SerializeField] private AccessoryType accessoryType;

    public AccessoryType GetAccessoryType() {
        return accessoryType;
    }

    public override int GetID() {
        return (int)accessoryType;
    }

    public bool IsMatchID(int id) {
        return (int)accessoryType == id;
    }
}