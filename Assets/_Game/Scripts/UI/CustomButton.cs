using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : Button {
    [SerializeField] private TextMeshProUGUI textButton;

    public void SetText(string content) {
        textButton.text = content;
    }
}