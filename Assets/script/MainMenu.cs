using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject parametreMenu;

    public GameObject explication;
    public GameObject mission;
    public bool validstartMission = false;

    public GameObject vaccin;

    public bool validstart = false;

    public Text textInteractstart;
    public string difficultychoose;

    public GameObject CinematicFin;
    public Text TextIntectactCinematic;
    public GameObject TextCinematic;
    public GameObject TextFin;
    public Image Imagefin;
    private bool validfin = false;

    public static MainMenu instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de MainMenu dans la scène");
            return;
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
       Time.timeScale = 1f;
       Screen.fullScreen = true;
        if (mission != null){
          mission.SetActive(false);
        }
                
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && validstart)
        {
          StartCoroutine(confirmstart());
        }
         if(Input.GetKeyDown(KeyCode.E) && validstartMission)
        {
          StartCoroutine(confirmstartMission());
        }

        if(Input.GetKeyDown(KeyCode.E) && validfin)
        {
          StartCoroutine(Final());
        }
    }
    

   public void StartGameButton()
    {
        StartCoroutine(Loadstart());
        
        
    } 

    public void StartGameButtonMission(string difficulty)
    {
        StartCoroutine(LoadstartMission(difficulty));
        LoadAndSaveData.instance.SaveDataVillage();
        
        
    }

   

    public void SettingButton()
    {
        parametreMenu.SetActive(true);
        
    }

    public void CloseSettingButton()
    {
        parametreMenu.SetActive(false);
    }
    public void CreditButton()
    {
        SceneManager.LoadScene("credit");
    } 

    public void QuitGame()
    {
      //SaveDataVillage();
      
        Application.Quit();
    }

    public IEnumerator LoadstartScene(string levelToLoad)
  {
  
    yield return new WaitForSeconds(1f);
    SceneManager.LoadScene(levelToLoad);
    
  }

  public IEnumerator LoadstartMission(string difficulty)
  {
     difficultychoose = difficulty;
    yield return new WaitForSeconds(1f);
    fondnoir.instance.entrer();
    yield return new WaitForSeconds(0.1f);
    fondnoir.instance.sorti();
    explication.SetActive(true);
    yield return new WaitForSeconds(5f);
    validstartMission= true;
    StartCoroutine(BlinkTextInteract());
    
    
  }

  public IEnumerator confirmstartMission()
  {
    fondnoir.instance.entrer();
    yield return new WaitForSeconds(2f);
    SceneManager.LoadScene("Jeudetir");
  }



  public IEnumerator Loadstart()
  {
    yield return new WaitForSeconds(1f);
    fondnoir.instance.entrer();
    yield return new WaitForSeconds(0.1f);
    fondnoir.instance.sorti();
    explication.SetActive(true);
    yield return new WaitForSeconds(10f);
    validstart= true;
    StartCoroutine(BlinkTextInteract());
    
    
  }

  public IEnumerator confirmstart()
  {
    fondnoir.instance.entrer();
    yield return new WaitForSeconds(2f);
    SceneManager.LoadScene("village");
  }

  IEnumerator BlinkTextInteract()
    {
        float blinkDuration = 200f;
        float blinkInterval = 0.7f; // Durée entre chaque clignotement (plus grand = plus lent)
        float elapsed = 0f;
        bool isVisible = true;

        while (elapsed < blinkDuration)
        {
            if (textInteractstart != null)
                textInteractstart.gameObject.SetActive(isVisible);
            isVisible = !isVisible;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }
        textInteractstart.gameObject.SetActive(false);
    }







  public void openOngletMission()
  {
    if (mission != null){
          mission.SetActive(true);
        }
  }

  public void closeOngletMission()
  {
    if (mission != null){
          mission.SetActive(false);
        }
  }


  public void openOngletVaccin()
  {
      if (vaccin != null)
      
          vaccin.SetActive(true);
          
  }

  public void closeOngletVaccin()
  {
      if (vaccin != null)
          vaccin.SetActive(false);
  }


  public void openOngletCinematicFin()
  {
      if (CinematicFin != null)
      
          CinematicFin.SetActive(true);
          TextCinematic.SetActive(true);
          TextFin.SetActive(false);
          Imagefin.color = new Color(1f, 1f, 1f, 0f);
          StartCoroutine(openTextCinematic());
          
  }

  IEnumerator openTextCinematic()
  {
      float duration = 10f; // Durée totale du fondu (en secondes)
    float elapsed = 0f;
    Color startColor = new Color(1f, 1f, 1f, 0f);
    Color endColor = new Color(1f, 1f, 1f, 1f);

    while (elapsed < duration)
    {
        float t = elapsed / duration;
        Imagefin.color = Color.Lerp(startColor, endColor, t);
        elapsed += Time.deltaTime;
        yield return null;
    }
    Imagefin.color = endColor;
          StartCoroutine(BlinkTextInteractFin());
          validfin= true;
 
  }

  IEnumerator Final()
  {
    StopCoroutine(BlinkTextInteractFin());
    textInteractstart.gameObject.SetActive(false);
      TextFin.SetActive(true);
      TextCinematic.SetActive(false);
      LoadAndSaveData.instance.SaveDataVillage();
      yield return new WaitForSeconds(2f);
      SceneManager.LoadScene("credit");

  }


    IEnumerator BlinkTextInteractFin()
    {
        float blinkDuration = 200f;
        float blinkInterval = 0.7f; // Durée entre chaque clignotement (plus grand = plus lent)
        float elapsed = 0f;
        bool isVisible = true;

        while (elapsed < blinkDuration)
        {
            if (TextIntectactCinematic != null)
                TextIntectactCinematic.gameObject.SetActive(isVisible);
            isVisible = !isVisible;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }
        TextIntectactCinematic.gameObject.SetActive(false);
    }


  
}
