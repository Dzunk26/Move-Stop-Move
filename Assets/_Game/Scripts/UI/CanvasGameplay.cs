using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameplay : UICanvas {
    [SerializeField] private Button settingButton;
    [SerializeField] private GameObject panelGuide;
    [SerializeField] private TextMeshProUGUI textBotCount;

    private void Awake() {
        settingButton.onClick.AddListener(() => {
            OnPressButtonSetting();
        });
    }

    private void Start() {
        GameInput.Instance.OnFirstTourch += GameInput_OnFirstTourch;
    }

    private void GameInput_OnFirstTourch(object sender, System.EventArgs e) {
        TurnOffGuide();
    }

    private void Update() {
        UpdateVisual();
    }

    public override void SetUp() {
        base.SetUp();
        UpdateVisual();
        TurnOnGuide();
    }

    private void UpdateVisual() {
        int remainBotCount = BotManager.Instance.GetRemainBotCount();
        textBotCount.text = "Alive: " + remainBotCount;
    }

    private void TurnOffGuide() {
        panelGuide.SetActive(false);
    }

    private void TurnOnGuide() {
        panelGuide.SetActive(true);
    }

    private void OnPressButtonSetting() {
        GameManager.Instance.PauseGame();
    }
}
