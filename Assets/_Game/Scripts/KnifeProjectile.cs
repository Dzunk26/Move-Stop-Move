using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeProjectile : BaseProjectile {
    protected override void Fly() {
        TF.position += shootDir * shootSpeed * Time.deltaTime;
    }
}