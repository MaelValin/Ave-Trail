using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameOverManager : MonoBehaviour
{

    public GameObject gameOverUI;

    public static GameOverManager instance;

    private void Awake()
    { 
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la scène");
            return;
        }
        else
        {
            instance = this;
        }
    }
    
  public void OnPlayerDeath()
    {
        GamePause.isGamePlayed = false;
       StartCoroutine(OnPlayerverCoroutine());
    }

    IEnumerator OnPlayerverCoroutine()
    {
        yield return new WaitForSeconds(2f);
        fondnoir.instance.entrer();
        PlayerMovement.instance.enabled = false;


        yield return new WaitForSeconds(2f);
        gameOverUI.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0f;
        
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerHealth.instance.Respawn();
        Time.timeScale = 1f;
        gameOverUI.SetActive(false);
    }

    public void VillageButton()
    {
        
       Time.timeScale = 1f;
        SceneManager.LoadScene("village");
        
    }

   
    public void QuitButton()
    {
        Application.Quit();
    }
}
