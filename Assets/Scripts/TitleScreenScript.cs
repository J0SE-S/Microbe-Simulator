using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour {
    public GameObject MainMenuCanvas;
    public GameObject SettingsCanvas;

    public void PlayGame() {
        //SceneManager.LoadScene("GameScene");
    }

    // Update is called once per frame
    public void OpenSettingsMenu() {
        MainMenuCanvas.SetActive(false);
        SettingsCanvas.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit(0);
    }

    public void BackToMainMenu() {
        MainMenuCanvas.SetActive(true);
        SettingsCanvas.SetActive(false);
    }
}