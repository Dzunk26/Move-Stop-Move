using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour {
    [SerializeField] private GameObject onObject;
    [SerializeField] private GameObject offObject;
    [SerializeField] private Toggle toggle;

    private void Awake() {
        toggle.onValueChanged.AddListener(OnToggleChanged);
        OnToggleChanged(toggle.isOn);
    }

    private void OnToggleChanged(bool isOn) {
        onObject.SetActive(isOn);
        offObject.SetActive(!isOn);
    }
}