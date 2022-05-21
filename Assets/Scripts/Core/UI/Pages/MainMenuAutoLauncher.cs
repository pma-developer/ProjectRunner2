using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Core.UI.Pages;

public class MainMenuAutoLauncher : MonoBehaviour
{
    private UIManager _uiManager;

    [Inject]
    private void Init(UIManager uiManager) {
        _uiManager = uiManager;
    }

    public void Start() {
        _uiManager.OpenPage<MainMenuPage>();
    }
}
