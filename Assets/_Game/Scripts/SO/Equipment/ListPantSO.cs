using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ListPantSO : ScriptableObject {
    [SerializeField] private List<PantSO> pants;

    public List<PantSO> GetPantSOs() {
        return pants;
    }

    public PantSO GetByID(int id) {
        PantSO target = null;

        foreach (PantSO pantSO in pants) {
            if (pantSO.IsMatchID(id)) {
                target = pantSO;
                break;
            }
        }

        return target;
    }
}