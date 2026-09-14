using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PantType {
    None,
    America,
    Batman,
    Leopard,
    Onion,
    Pokemon,
    PolkaDots,
    Rainbow,
    Skull,
    Violet
}

[CreateAssetMenu()]
public class PantSO : EquipmentSO {
    [SerializeField] private PantType pantType;

    public PantType GetPantType() {
        return pantType;
    }

    public override int GetID() {
        return (int)pantType;
    }

    public bool IsMatchID(int id) {
        return (int)pantType == id;
    }
}
