using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : GameUnit {


    public bool IsOutOfRange => Vector3.Distance(startPoint, TF.position) >= characterAttackRange;

    [SerializeField] protected float shootSpeed = 5f;
    [SerializeField] protected float stopTimerMax = 1f;
    [SerializeField] protected float delayStop = 0.05f;
    [SerializeField] protected Collider weaponCollider;

    protected Vector3 shootDir;
    protected Vector3 startPoint;
    protected bool isFlying;
    protected float stopTimer;
    private float characterAttackRange;
    private Character owner;

    private void Update() {

        if (isFlying) {
            Fly();
            if (IsOutOfRange) {
                OnDespawn();
            }
        }
        else {
            stopTimer += Time.deltaTime;
            if (stopTimer >= stopTimerMax) {
                OnDespawn();
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Constant.CHARACTER_TAG)) {
            Character character = Cache.GetCharacter(other);
            if (!IsOwner(character)) {
                OnDespawn();
            }
        }
        else if (other.CompareTag(Constant.OBSTACLE_TAG)) {
            Invoke(nameof(StopFly), delayStop); // delay de tao cam giac projectile va vao obstacle
            weaponCollider.enabled = false;
        }
    }

    public void OnInit(Vector3 shootDir, Character owner) {
        this.owner = owner;
        this.shootDir = shootDir;
        this.characterAttackRange = owner.CharacterStat.attackRange.GetBaseValue();
        //this.characterAttackRange = owner.CharacterStat.attackRange.GetValue();
        startPoint = TF.position;
        Shoot();
        stopTimer = 0f;
        weaponCollider.enabled = true;
    }

    public void OnDespawn() {
        isFlying = false;
        SimplePool.ReturnToPool(this);
    }

    public void Shoot() {
        isFlying = true;
    }

    protected abstract void Fly();

    private void StopFly() {
        isFlying = false;
    }

    public bool IsOwner(Character character) {
        return character == owner;
    }
}