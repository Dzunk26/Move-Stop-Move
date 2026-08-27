using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Character : GameUnit {
    public CharacterStat CharacterStat => characterStat;
    public CharacterEquipment CharacterEquipment => characterEquipment;
    public CharacterVisual CharacterVisual => characterVisual;

    [SerializeField] protected CharacterStat characterStat;
    [SerializeField] protected CharacterEquipment characterEquipment;
    [SerializeField] protected CharacterVisual characterVisual;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected float attackDelay = 0.3f;

    [SerializeField] private Weapon weapon;

    protected float size;
    protected int level;
    protected int point;
    protected List<Character> targets = new List<Character>();
    protected Character currentTarget;
    protected float attackTimer;
    protected bool isAttacking;

    private Coroutine attackCoroutine;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Constant.PROJECTILE_TAG)) {
            BaseProjectile projectile = Cache.GetProjectile(other);
            if (!projectile.IsOwner(this)) {
                Debug.Log(this.name + " is dead");
                OnHitted();
            }
        }
    }

    public virtual void OnInit() {
        isAttacking = false;
        characterVisual.OnInit();
        OnEquipmentChanged(weapon, null);
    }

    public virtual void OnDespawn() {

    }

    public void IncreasePoint(int point) {
        this.point += point;
    }

    public void OnDetectTarget(Character character) {
        if (!targets.Contains(character)) {
            targets.Add(character);
        }
    }

    public bool HasTarget() {
        return targets.Count > 0;
    }

    public void OnEquipmentChanged(Equipment newEquipment, Equipment oldEquipment) {
        characterStat.OnEquipmentChanged(newEquipment, oldEquipment);
        characterVisual.OnEquipmentChanged(newEquipment);
        characterEquipment.OnEquipmentChanged(newEquipment);
    }

    protected virtual void OnHitted() {
        characterVisual.OnHitted();
        Dead();
    }

    protected virtual void Dead() {
        characterVisual.OnDead();
    }

    protected virtual void BeginAttack() {
        if ((!HasTarget() && currentTarget == null) || isAttacking) return;

        isAttacking = true;
        RemoveInvalidTagets();
        currentTarget = GetClosestTarget();
        if (currentTarget == null) return;
        Vector3 attackDir = (currentTarget.TF.position - TF.position).normalized;
        TF.rotation = Quaternion.LookRotation(attackDir);
        characterVisual.OnAttack();

        attackCoroutine = StartCoroutine(IEAttack(attackDir));
    }

    protected void CancelAttack() {
        if (attackCoroutine != null) {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
            ResetCoolDownAttack();
        }
    }

    private IEnumerator IEAttack(Vector3 direction) {
        yield return new WaitForSeconds(attackDelay);
        Attack(direction);
    }

    protected void Attack(Vector3 direction) {
        characterVisual.DeactiveWeaponVisual();
        Weapon weapon = characterEquipment.GetCurrentWeapon();
        //if (weapon == null) return;

        float characterAttackRange = characterStat.attackRange.GetValue();

        weapon.Fire(firePoint.position, firePoint.rotation, direction, this);
    }

    protected void OnCoolDownAttack() {
        if (isAttacking) {
            attackTimer += Time.deltaTime;

            if (attackTimer > characterStat.attackSpeed.GetValue()) {
                ResetCoolDownAttack();
            }
        }
    }

    protected void ResetCoolDownAttack() {
        attackTimer = 0f;
        isAttacking = false;
        characterVisual.ActiveWeaponVisual();
    }

    protected Character GetClosestTarget() {
        if (targets.Count <= 0 || targets == null) return null;

        float minDistance = float.MaxValue;
        Character target = null;

        foreach (Character character in targets) {
            if (Vector3.Distance(character.TF.position, TF.position) < minDistance) {
                target = character;
                minDistance = Vector3.Distance(character.TF.position, TF.position);
            }
        }

        return target;
    }

    protected void RemoveInvalidTagets() {
        if (targets.Count == 0 || targets == null) return;

        float characterAttackRange = characterStat.attackRange.GetBaseValue();
        //float characterAttackRange = characterStat.attackRange.GetValue();
        for (int i = targets.Count - 1; i >= 0; i--) {
            if ((targets[i].TF.position - TF.position).sqrMagnitude > characterAttackRange * characterAttackRange) {
                targets.RemoveAt(i);
            }
        }
    }

    protected void Upsize() {

    }

    protected void LevelUp() {
        level++;
    }
}