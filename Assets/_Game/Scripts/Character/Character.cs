using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Character : GameUnit { 
    [SerializeField] protected CharacterStat characterStat;
    [SerializeField] protected CharacterEquipment characterEquipment;
    [SerializeField] protected CharacterVisual characterVisual;
    [SerializeField] protected CharacterDetectTrigger characterDetectTrigger;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected float attackDelay = 0.3f;
    [SerializeField] protected float attackRangeOffset = 0.75f;
    [SerializeField] protected ListLevelCharacterConfigSO listLevelCharacterSO;

    [SerializeField] private Weapon weapon; // for testing

    protected int level;
    protected int point;
    protected List<Character> targets = new List<Character>();
    protected Character currentTarget;
    protected float attackTimer;
    protected bool isAttacking;
    protected float worldAttackRange;
    protected float detectTriggerRange;
    protected LevelCharacterConfigSO currentLevelCharacterSO;

    private Coroutine attackCoroutine;

    public virtual void OnInit() {
        isAttacking = false;
        level = 1;
        currentLevelCharacterSO = listLevelCharacterSO.GetLevelGrowthByLevelID(level);
        characterVisual.OnInit();
        OnEquipmentChanged(weapon);
        OnAttackRangeChanged();
    }

    public virtual void OnDespawn() {

    }


    public virtual void OnHitted() {
        characterVisual.OnHitted();
        Dead();
    }
    public void IncreasePoint(int point) {
        this.point += point;
        if (CanLevelUp(point, currentLevelCharacterSO.GetRequiredPointLevelUp())) {
            LevelUp();
        }
    }

    public void OnDetectTarget(Character character) {
        if (!targets.Contains(character)) {
            targets.Add(character);
        }
    }

    public int GetPointReward() {
        return currentLevelCharacterSO.GetPointReward();
    }

    public bool HasTarget() {
        return targets.Count > 0;
    }

    public float GetWorldAttackRange() {
        return worldAttackRange;
    }

    public void OnEquipmentChanged(Equipment newEquipment) {
        Equipment oldEquipment = characterEquipment.GetEquipmentByType(newEquipment.GetEquipmentType());

        characterStat.OnEquipmentChanged(newEquipment, oldEquipment);
        characterVisual.OnEquipmentChanged(newEquipment);
        characterEquipment.OnEquipmentChanged(newEquipment);
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

    protected virtual void OnAttackRangeChanged() {
        worldAttackRange = characterStat.GetWorldAttackRange();
        detectTriggerRange = worldAttackRange - attackRangeOffset;

        characterDetectTrigger.SetRange(detectTriggerRange);
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
        Weapon weapon = (Weapon)characterEquipment.GetEquipmentByType(EquipmentType.Weapon);
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

        float characterAttackRange = characterStat.GetWorldAttackRange();
        for (int i = targets.Count - 1; i >= 0; i--) {
            if ((targets[i].TF.position - TF.position).sqrMagnitude > characterAttackRange * characterAttackRange) {
                targets.RemoveAt(i);
            }
        }
    }

    protected void Upsize(LevelCharacterConfigSO levelGrowthSO) {
        if (levelGrowthSO == null) return;

        characterVisual.UpSize(levelGrowthSO.GetScaleModifier()); // upsize visual
        characterStat.OnLevelUp(levelGrowthSO); // add stat modifier

        OnAttackRangeChanged(); // update change
    }

    protected void LevelUp() {
        level++;
        currentLevelCharacterSO = listLevelCharacterSO.GetLevelGrowthByLevelID(level);
        Upsize(currentLevelCharacterSO);
    }

    private bool CanLevelUp(int point, int requiredPoint) {
        return point >= requiredPoint;
    }
}