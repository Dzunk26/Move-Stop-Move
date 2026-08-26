using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeProjectile : BaseProjectile {
    [SerializeField] private float rotateSpeed = 100f;

    protected override void Fly() {
        TF.position += shootDir * shootSpeed * Time.deltaTime;
        TF.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
}
