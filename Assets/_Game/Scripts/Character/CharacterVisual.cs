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
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject weaponVisual;
    [SerializeField] private List<EquipmentVisual> equipmentVisuals = new List<EquipmentVisual>();
    
    private AnimState currentAnimState;
    private string animName;

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
        ChangeAnimState(AnimState.Attack);
        
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
}