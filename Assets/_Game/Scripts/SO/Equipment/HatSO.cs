using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum HatType {
    None = 0,
    Arrow = 1,
    Beard = 2,
    Cap = 3,
    CowBoy = 4,
    Ear = 5,
    HeadPhone = 6,
    PoliceHat = 7,
    StrawHat = 8
}

[CreateAssetMenu()]
public class HatSO : EquipmentSO {
    [SerializeField] private HatType hatType;

    public HatType GetHatType() {
        return hatType;
    }

    public override int GetID() {
        return (int)hatType;
    }

    public bool IsMatchID(int id) {
        return (int)hatType == id;
    }
}