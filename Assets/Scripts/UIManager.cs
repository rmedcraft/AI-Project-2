using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.UI;
// detecting mouse clicks: https://learn.unity.com/tutorial/onmousedown#

public class UIManager : MonoBehaviour {
    public GameObject startButton;
    TextMeshProUGUI pauseText;
    // public TMP_Dropdown dropdown;
    public GameController gameController;

    public TMP_Dropdown presetDropdown;

    // Use this for initialization
    void Start() {
        pauseText = startButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void StartButtonClicked() {
        if (gameController) {
            gameController.SetPaused(!gameController.GetPaused());
            pauseText.text = gameController.GetPaused() ? "Play" : "Pause";
        } else {
            Debug.Log("UIManager Error: GameController does not exist");
        }
    }

    public void ClearButtonClicked() {
        if (gameController && gameController.GetPaused()) {
            gameController.Clear();
        }
    }

    public void ApplyPreset() {

    }
}

enum Presets {
    None = 0,
    Glider = 1,
    Oscillator = 2
}
