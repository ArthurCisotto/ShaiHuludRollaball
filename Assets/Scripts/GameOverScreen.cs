using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI secondsSurvivedUI;

    public void Setup(int secondsSurvived)
    {
        gameObject.SetActive(true);
        secondsSurvivedUI.text = secondsSurvived.ToString() + " seconds survived";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("MiniGame");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
