using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class PlayerManger : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool gameStart;
    public GameObject TextStart;

    public static int numberOfCoins;
    public TextMeshProUGUI coinsText;
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        gameStart = false;
        numberOfCoins = 0;
    }

    
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        coinsText.text = "Coins: " + numberOfCoins; 
        if(SwipeManager.tap && SceneManager.GetActiveScene().name == "SampleScene")
        {
            gameStart = true;
            Destroy(TextStart);
        }
        if(SceneManager.GetActiveScene().name == "Level2")
        {
            gameStart = true;
            Destroy(TextStart);
        }
    }
}
