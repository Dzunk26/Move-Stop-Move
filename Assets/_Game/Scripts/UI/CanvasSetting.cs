using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSetting : UICanvas {
    [SerializeField] private Toggle toggleVibration;
    [SerializeField] private Toggle toggleMusic;
    [SerializeField] private Button buttonHome;
    [SerializeField] private Button buttonContinue;

    private void Awake() {
        toggleVibration.onValueChanged.AddListener(OnToggleVibrationChanged);

        toggleMusic.onValueChanged.AddListener(OnToggleMusicChanged);

        buttonHome.onClick.AddListener(() => {
            OnPressHomeButton();
        });

        buttonContinue.onClick.AddListener(() => {
            OnPressContinueButton();
        });
    }

    public override void SetUp() {
        base.SetUp();
        //musicSlider.value = MusicManager.Instance.GetVolume();
        //soundSlider.value = SoundManager.Instance.GetVolume();
    }

    private void OnToggleVibrationChanged(bool isOn) {

    }

    private void OnToggleMusicChanged(bool isOn) {

    }

    private void OnPressHomeButton() {
        UIManager.Instance.CloseAllUI();

        GameManager.Instance.OnReloadLevel();
        GameManager.Instance.GameInMainMenu();

        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }

    private void OnPressContinueButton() {
        Close(0);
        GameManager.Instance.PlayGame();
    }
}