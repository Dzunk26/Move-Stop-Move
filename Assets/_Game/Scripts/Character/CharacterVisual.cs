using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimState {
    Idle,
    Run,
    Attack,
    Dead,
    DanceWin,
    DanceCharSkin
}

public class CharacterVisual : MonoBehaviour {
    public Transform TF {
        get {
            if (tf == null) {
                tf = transform;
            }

            return tf;
        }
    }

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject weaponVisual;
    [SerializeField] private List<EquipmentVisual> equipmentVisuals = new List<EquipmentVisual>();
    
    private AnimState currentAnimState;
    private string animName;
    private Transform tf;

    public void OnInit() {
        currentAnimState = AnimState.Idle;
    }

    public void OnIdle() {
        ChangeAnimState(AnimState.Idle);
    }

    public void OnRun() {
        ChangeAnimState(AnimState.Run);
    }

    public void OnAttack() {
        PlayAnim(AnimState.Attack);
    }

    public void OnWin() {
        ChangeAnimState(AnimState.DanceWin);
    }

    public void OnDead() {
        ChangeAnimState(AnimState.Dead);
    }

    public void OnHitted() {
        // spawn VFX
    }

    public void OnEquipmentChanged(Equipment newEquipment) {
        EquipmentVisual equipmentVisual = FindEquipmentVisualByType(newEquipment.GetEquipmentType());
        equipmentVisual.OnChangeEquipment(newEquipment);
    }

    public void ActiveWeaponVisual() {
        weaponVisual.SetActive(true);
    }

    public void DeactiveWeaponVisual() {
        weaponVisual.SetActive(false);
    }

    public void UpSize(float sizeScale) {
        Vector3 newScale = new Vector3(sizeScale, sizeScale, sizeScale);

        TF.localScale += newScale;
    }

    public void OnPause() {
        animator.enabled = false;
    }

    public void OnExitPause() {
        animator.enabled = true;
    }

    private EquipmentVisual FindEquipmentVisualByType(EquipmentType equipmentType) {
        foreach (EquipmentVisual equipmentVisual in equipmentVisuals) {
            if (equipmentVisual.IsMatchEquipmentType(equipmentType)) {
                return equipmentVisual;
            }
        }

        return null;
    }

    private void ChangeAnimState(AnimState animState) {
        if (currentAnimState != animState) {
            animName = Cache.GetAnimName(currentAnimState);

            animator.ResetTrigger(animName);

            currentAnimState = animState;
            animName = Cache.GetAnimName(currentAnimState);

            animator.SetTrigger(animName);
        }
    }

    private void PlayAnim(AnimState animState) {
        int hash = Cache.GetAnimHash(animState);
        animator.Play(hash, 0, 0f);
    }
}