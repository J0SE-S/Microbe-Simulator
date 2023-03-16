using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour {
    // Start is called before the first frame update
    public void PlayGame() {
        //SceneManager.LoadScene("GameScene");
    }

    // Update is called once per frame
    public void OpenSettingsMenu() {
    }

    public void QuitGame() {
        Application.Quit(0);
    }
}
