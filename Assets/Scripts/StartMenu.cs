using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void OnPlayButton() {
        SceneManager.LoadScene("Game");
    }

    public void OnQuitButton() {
        Application.Quit();
    }
}
