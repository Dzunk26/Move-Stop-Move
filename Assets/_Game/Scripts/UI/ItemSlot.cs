using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : Button, ISelectHandler, IDeselectHandler {
    [SerializeField] private Image Corner;
    [SerializeField] private Image equipmentIcon;
    [SerializeField] private GameObject lockState;
    [SerializeField] private IItemSlotListener itemSlotListener;

    private EquipmentSO equipmentSO;

    public void Setup(EquipmentSO equipmentSO, bool isLocked) {
        this.equipmentSO = equipmentSO;

        equipmentIcon.sprite = equipmentSO.GetEquipmentIcon();
        SetLockState(isLocked);
        SetCorner(false);

        onClick.RemoveAllListeners();
        onClick.AddListener(OnClick);
    }

    private void OnClick() {
        itemSlotListener.OnItemSlotSelected(equipmentSO);
    }

    public override void OnSelect(BaseEventData eventData) {
        base.OnSelect(eventData);
        SetCorner(true);
    }

    public override void OnDeselect(BaseEventData eventData) {
        base.OnDeselect(eventData);
        SetCorner(false);
    }

    private void SetLockState(bool isLocked) {
        lockState.SetActive(isLocked);
    }

    private void SetCorner(bool isChosen) {
        Corner.enabled = isChosen;
    }
}