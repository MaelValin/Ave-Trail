using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool isGamePlayed = true;
    public GameObject pauseMenuUI;

    public Image Button;
    public Sprite ButtonPlay;
    public Sprite ButtonPause;

    public static GamePause instance;

 private void Awake()
{
    if (instance != null)
    {
        Debug.LogWarning("Il y a plus d'une instance de GamePause dans la scène");
        return;
    }
    else
    {
        instance = this;
    }

    if (pauseMenuUI == null)
        Debug.LogError("pauseMenuUI n'est pas assigné dans l'inspecteur !");
}

    void Start()
    {
        
        isGamePlayed = true;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGamePlayed)
        {
            if (GameIsPaused)
            {
                
                Resume();
                
            }
            else
            {
                
                Pause();
                
            }
        }

        
        
    }

    public void Buttonpause()
    {
        if (GameIsPaused)
        {
            Resume();
            
        }
        else
        {
            Pause();
            
        }
    }


    public void Resume()
    {
        Button.sprite = ButtonPause;
        PlayerMovement.instance.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
    }

    public void Pause()
    {
        Button.sprite = ButtonPlay;
        PlayerMovement.instance.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        
    }


    public void RetryButton()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerHealth.instance.Respawn();
    }


     public void MenuButton()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

       SceneManager.LoadScene("Menu");
    }

    public void VillageButton()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

       SceneManager.LoadScene("village");
    }
}
