using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : UICanvas {
    [SerializeField] private Toggle toggleVibration;
    [SerializeField] private Toggle toggleMusic;
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonWeaponShop;
    [SerializeField] private Button buttonSkinShop;

    private void Awake() {
        toggleMusic.onValueChanged.AddListener(OnToggleVibrationChanged);

        toggleMusic.onValueChanged.AddListener(OnToggleMusicChanged);

        buttonPlay.onClick.AddListener(() => {
            OnPressButtonPlayGame();
        });

        buttonWeaponShop.onClick.AddListener(() => {
            OnPressButtonWeaponShop();
        });

        buttonSkinShop.onClick.AddListener(() => {
            OnPressButtonSkinShop();
        });
    }

    public override void SetUp() {
        base.SetUp();
        //toggleMusic.isOn = ;
        //toggleVibration.isOn = ;
    }

    private void OnToggleVibrationChanged(bool isOn) {

    }

    private void OnToggleMusicChanged(bool isOn) {

    }

    private void OnPressButtonPlayGame() {
        UIManager.Instance.CloseAllUI();
        UIManager.Instance.OpenUI<CanvasGameplay>();
        GameManager.Instance.PlayGame();
    }

    private void OnPressButtonWeaponShop() {
        UIManager.Instance.CloseAllUI();
        UIManager.Instance.OpenUI<CanvasWeaponShop>();
    }

    private void OnPressButtonSkinShop() {
        UIManager.Instance.CloseAllUI();
        UIManager.Instance.OpenUI<CanvasSkinShop>();
    }
}