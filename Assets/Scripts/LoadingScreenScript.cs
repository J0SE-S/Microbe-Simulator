using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenScript : MonoBehaviour {
    public static LoadingScreenScript Instance;
    public string sceneToLoad;

    void Start() {
        Instance = this;
        DontDestroyOnLoad(this);
        if (SceneManager.GetActiveScene().name == "LoadingScreen") {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    void OnSceneLoad() {
        if (SceneManager.GetActiveScene().name == "LoadingScreen") {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
