using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasLose : UICanvas {
    [SerializeField] private TextMeshProUGUI textRank;
    [SerializeField] private TextMeshProUGUI textKiller;
    [SerializeField] private Button continueButton;


    private void Awake() {
        continueButton.onClick.AddListener(() => {
            OnPressContinueButton();
        });
    }

    public override void SetUp() {
        base.SetUp();
        UpdateVisual();
    }

    private void UpdateVisual() {
        int rank = BotManager.Instance.GetRemainBotCount();
        textRank.text = "#" + rank;
        //text killer
    }

    private void OnPressContinueButton() {
        UIManager.Instance.CloseAllUI();

        int levelID = PlayerProgress.Instance.GetCurrentLevelID();

        GameManager.Instance.OnReloadLevel();
        GameManager.Instance.GameInMainMenu();

        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
}