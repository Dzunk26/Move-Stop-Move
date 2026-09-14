using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ListWeaponSO : ScriptableObject {
    [SerializeField] private List<WeaponSO> weapons;

    public List<WeaponSO> GetWeaponSOs() {
        return weapons;
    }

    public WeaponSO GetByID(int id) {
        WeaponSO target = null;

        foreach (WeaponSO weaponSO in weapons) {
            if (weaponSO.IsMatchID(id)) {
                target = weaponSO;
                break;
            }
        }

        return target;
    }
}
