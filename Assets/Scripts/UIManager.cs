using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using System.Threading.Tasks;
// detecting mouse clicks: https://learn.unity.com/tutorial/onmousedown#

public class UIManager : MonoBehaviour {
    public GameObject startButton;
    public TMP_Dropdown dropdown;
    public GameController gameController;
    private bool isSearching;
    // Use this for initialization
    void Start() {
        isSearching = false;
    }

    // Update is called once per frame
    void Update() {
        // update initial state when a 
    }


    public void StartButtonClicked() {
        if (gameController && dropdown) {
            PatternType s = (PatternType)dropdown.value;
            if (s != PatternType.None) {
                // gameController.BeginSearch(s);
            } else {
                // dont do anything, maybe put a popup on screen
                Debug.Log("UIManager Error: Please select a pattern to use");
            }
        }
    }

    public void SetSearching(bool isSearching) {
        this.isSearching = isSearching;
    }

}
public enum PatternType {
    None = 0,
    Glider = 1,
    Oscillator = 2,
}
