using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSkinShop : UICanvas, IItemSlotListener {
    [SerializeField] private Button buttonCancel;
    [SerializeField] private Button buttonShopHats;
    [SerializeField] private Button buttonShopPants;
    [SerializeField] private Button buttonShopAccessories;
    [SerializeField] private CustomButton buttonBuy;
    [SerializeField] private CustomButton buttonEquip;
    [SerializeField] private ItemSlotManager itemSlotManager;
    [SerializeField] private TextMeshProUGUI textStatDescription;

    [SerializeField] private ListHatSO hats;
    [SerializeField] private ListPantSO pants;
    [SerializeField] private ListAccessorySO accessories;

    private EquipmentSO currentSelectedEquipment;

    private void Awake() {
        buttonCancel.onClick.AddListener(() => {
            OnPressButtonCancel();
        });

        buttonShopHats.onClick.AddListener(() => {
            OnPressButtonShopHats();
        });

        buttonShopPants.onClick.AddListener(() => {
            OnPressButtonShopPants();
        });

        buttonShopAccessories.onClick.AddListener(() => {
            OnPressButtonAccessories();
        });

        buttonBuy.onClick.AddListener(() => {
            OnPressButtonBuy();
        });

        buttonEquip.onClick.AddListener(() => {
            OnPressButtonEquip();
        });
    }

    public override void SetUp() {
        base.SetUp();
        buttonShopHats.onClick.Invoke();
    }

    public void OnItemSlotSelected(EquipmentSO equipmentSO) {
        currentSelectedEquipment = equipmentSO;
        RefreshButtonState();
    }

    private void OnPressButtonCancel() {
        UIManager.Instance.CloseAllUI();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }

    private void OnPressButtonShopHats() {
        itemSlotManager.UpdateVisual(hats.GetHatSOs());
    }

    private void OnPressButtonShopPants() {
        itemSlotManager.UpdateVisual(pants.GetPantSOs());
    }

    private void OnPressButtonAccessories() {
        itemSlotManager.UpdateVisual(accessories.GetAccessorySOs());
    }

    private void OnPressButtonBuy() {
        if (currentSelectedEquipment == null) return;

        int goldRequire = currentSelectedEquipment.GetGoldRequire();

        if (!PlayerProgress.Instance.CanAfford(goldRequire)) return;

        PlayerProgress.Instance.SpendCoins(goldRequire);
        PlayerProgress.Instance.UnlockNewItem(currentSelectedEquipment);
        RefreshButtonState();

        DataManager.Instance.SaveGame();
    }

    private void OnPressButtonEquip() {
        if (currentSelectedEquipment == null) return;

        PlayerProgress.Instance.Equip(currentSelectedEquipment);

        DataManager.Instance.SaveGame();
    }

    private void RefreshButtonState() {
        if (currentSelectedEquipment == null) return;

        bool isUnlocked = PlayerProgress.Instance.IsUnlocked(currentSelectedEquipment);
        bool isEquipped = PlayerProgress.Instance.IsEquipped(currentSelectedEquipment);

        buttonBuy.gameObject.SetActive(!isUnlocked);
        buttonEquip.gameObject.SetActive(isUnlocked);

        if (!isUnlocked) {
            buttonBuy.SetText(currentSelectedEquipment.GetGoldRequire().ToString());
        }
        else {
            buttonEquip.SetText(isEquipped ? Constant.EQUIP_TEXT : Constant.NONEQUIP_TEXT);
        }

        textStatDescription.text = currentSelectedEquipment.GetStatDescription();
    }
}