using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject GameplayPanel;
    public GameObject GameOverPanel;

    public Text CoinText;
    public int Coins;

    public string StageSuivant;

    private void Awake()
    {
        Application.targetFrameRate = 120;
        Application.runInBackground = true;
    }
    void Start()
    {
        GameplayPanel.gameObject.SetActive(true);
        GameOverPanel.gameObject.SetActive(false);

        CoinText.text = "x0";
    }

   
    public void UpdateCoin()
    {
        CoinText.text = "x" + Coins;
    }

    public void GameOver()
    {
        GameplayPanel.gameObject.SetActive(false);
        GameOverPanel.gameObject.SetActive(true);
    }

    public void RECOMMERCER()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  
}
