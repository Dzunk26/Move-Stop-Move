using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotManager : MonoBehaviour {
    [SerializeField] private Transform container;
    [SerializeField] private ItemSlot slotItemPrefab;

    private List<ItemSlot> activeSlots = new List<ItemSlot>();

    public void UpdateVisual<T>(List<T> equipmentSOs) where T : EquipmentSO{
        foreach (ItemSlot slot in activeSlots) {
            if (slot != null) {
                Destroy(slot.gameObject);
            }
        }
        activeSlots.Clear();

        foreach (T equipmentSO in equipmentSOs) {
            ItemSlot itemSlot = Instantiate(slotItemPrefab, container);
            bool isLocked = !PlayerProgress.Instance.IsUnlocked(equipmentSO);
            itemSlot.Setup(equipmentSO, isLocked);
            activeSlots.Add(itemSlot);
        }

        if (activeSlots != null && activeSlots.Count > 0) {
            activeSlots[0].onClick.Invoke();
        }
    }
}