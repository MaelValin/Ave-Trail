using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameWinnerManager : MonoBehaviour
{

    public GameObject gameWinnerUI;

    public static GameWinnerManager instance;

    private void Awake()
    { 
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameWinnerManager dans la scène");
            return;
        }
        else
        {
            instance = this;
        }
    }
    
  public void OnPlayerWin()
    {
        GamePause.isGamePlayed = false;
        StartCoroutine(OnPlayerWinCoroutine());
    }

    IEnumerator OnPlayerWinCoroutine()
    {
        fondnoir.instance.entrer();
        PlayerMovement.instance.enabled = false;
        
        yield return new WaitForSeconds(2f);
        gameWinnerUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerHealth.instance.Respawn();
        Time.timeScale = 1f;
        gameWinnerUI.SetActive(false);
    }

    public void VillageButton()
    {
        
       Time.timeScale = 1f;
        LoadAndSaveData.instance.SaveDataGame();
        SceneManager.LoadScene("village");
       
    }



    public void QuitButton()
    {
        Application.Quit();
    }
}
