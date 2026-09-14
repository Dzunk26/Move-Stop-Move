using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasVictory : UICanvas {
    [SerializeField] private Button nextButton;

    private void Awake() {
        nextButton.onClick.AddListener(() => {
            OnPressNextButton();
        });
    }

    private void OnPressNextButton() {
        UIManager.Instance.CloseAllUI();

        PlayerProgress.Instance.NextLevel();
        GameManager.Instance.OnLoadLevel();
        GameManager.Instance.GameInMainMenu();

        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
}