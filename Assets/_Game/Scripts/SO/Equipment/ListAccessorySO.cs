using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ListAccessorySO : ScriptableObject {
    [SerializeField] private List<AccessorySO> accessories;

    public List<AccessorySO> GetAccessorySOs() {
        return accessories;
    }

    public AccessorySO GetByID(int id) {
        AccessorySO target = null;

        foreach (AccessorySO accessorySO in accessories) {
            if (accessorySO.IsMatchID(id)) {
                target = accessorySO;
                break;
            }
        }

        return target;
    }
}