using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasLoading : UICanvas {
    [SerializeField] private Image imageFill;
    [SerializeField] private TextMeshProUGUI textFill;
    [SerializeField] private float loadingTimerMax = 3f;

    private bool isStartLoading = false;
    private float loadingTimer = 0f;

    private void Update() {
        if (isStartLoading) {
            loadingTimer += Time.deltaTime;
            if (loadingTimer <= loadingTimerMax) {
                float loadingAmount = loadingTimer / loadingTimerMax;
                imageFill.fillAmount = loadingAmount;
                textFill.SetText((loadingAmount * 100).ToString("F0") + "%");
            }
            else {
                GameManager.Instance.GameInMainMenu();
                UIManager.Instance.OpenUI<CanvasMainMenu>();
                Close(0);
                isStartLoading = false;
            }
        }
    }

    public override void SetUp() {
        base.SetUp();

        isStartLoading = true;
        loadingTimer = 0f;
        imageFill.fillAmount = 0f;
    }
}