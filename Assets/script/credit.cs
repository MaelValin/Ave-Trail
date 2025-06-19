using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class credit : MonoBehaviour
{

    public Animator fondnoir;
    public void LoadMainMenu()
    {
        StartCoroutine(LoadstartScene("Menu"));
    }

    void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                LoadMainMenu();
            }
        }

         
    public void MenuButton()
    {

       
       StartCoroutine(LoadstartScene("Menu"));
    }


    public IEnumerator LoadstartScene(string levelToLoad)
  {
    
    fondnoir.SetTrigger("sorti");
    
    yield return new WaitForSeconds(1f);
    SceneManager.LoadScene(levelToLoad);
    
    
  }
    
}
