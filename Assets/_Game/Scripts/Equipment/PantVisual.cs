using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantVisual : EquipmentVisual {
    [SerializeField] private Renderer pantRenderer;

    public override void OnChangeEquipment(Equipment newEquipment) {
        currentEquipment = newEquipment;
        Pant newPant = (Pant)newEquipment;

        pantRenderer.material = newPant.GetMaterial();
    }
}