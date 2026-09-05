using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeProjectile : BaseProjectile {
    [SerializeField] private float rotateSpeed = 100f;

    private float currentAngle;

    public override void OnInit(Vector3 shootDir, Character owner) {
        base.OnInit(shootDir, owner);
        currentAngle = 0f;
        TF.rotation = Quaternion.identity;
    }

    protected override void Fly() {
        TF.position += shootDir * shootSpeed * Time.deltaTime;

        currentAngle += rotateSpeed * Time.deltaTime;
        TF.rotation = Quaternion.Euler(0f, currentAngle, 0f);
    }
}
