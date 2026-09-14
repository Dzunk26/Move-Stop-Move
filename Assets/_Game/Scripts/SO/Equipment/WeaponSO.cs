using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum WeaponType {
    None,
    Arrow,
    Boomerang,
    CandyCane,
    SingleBitAxe,
    DoubleBitAxe,
    Hammer,
    Knife,
    Lollipop,
    Uzi
}

[CreateAssetMenu()]
public class WeaponSO : EquipmentSO {
    [SerializeField] private WeaponType weaponType;

    public WeaponType GetWeaponType() {
        return weaponType;
    }

    public override int GetID() {
        return (int)weaponType;
    }

    public bool IsMatchID(int id) {
        return (int)weaponType == id;
    }
}