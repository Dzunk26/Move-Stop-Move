using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ListHatSO : ScriptableObject {
    [SerializeField] private List<HatSO> hats;

    public List<HatSO> GetHatSOs() {
        return hats;
    }

    public HatSO GetByID(int id) {
        HatSO target = null;

        foreach (HatSO hatSO in hats) {
            if (hatSO.IsMatchID(id)) {
                target = hatSO;
                break;
            }
        }

        return target;
    }
}