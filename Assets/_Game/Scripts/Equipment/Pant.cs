using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pant : Equipment {
    [SerializeField] private Material material;

    public Material GetMaterial() {
        return material;
    }
}
