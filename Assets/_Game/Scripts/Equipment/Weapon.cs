using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment {
    [SerializeField] private BaseProjectile  projectilePrefab;

    public void Fire(Vector3 position, Quaternion rotation, Vector3 shootDir, Character owner) {
        BaseProjectile projectile = SimplePool.GetFromPool<BaseProjectile>(projectilePrefab.PoolType, position, rotation);

        projectile.OnInit(shootDir, owner);
    }
}