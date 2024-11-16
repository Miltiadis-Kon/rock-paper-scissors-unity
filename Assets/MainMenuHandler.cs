using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    public string GameScene= "RPS";

    public Button playButton;
    public Button guideButton;

    public Button settingsButton;

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(GameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        playButton.onClick.AddListener(StartGame);
        guideButton.onClick.AddListener(ShowHideGuide);
        settingsButton.onClick.AddListener(ShowHideSettings);
    }

    public Transform guide;
    public Transform settings;

    public void ShowHideGuide()
    {
        guide.gameObject.SetActive(!guide.gameObject.activeSelf);
        settings.gameObject.SetActive(false);
    }

    public void ShowHideSettings()
    {
        settings.gameObject.SetActive(!settings.gameObject.activeSelf);
        guide.gameObject.SetActive(false);
    }

}
