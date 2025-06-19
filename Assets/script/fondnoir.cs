using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class fondnoir : MonoBehaviour
{
    public Image spriteFondNoir;
    public bool fondnoirActive = false;

    public static fondnoir instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de fondnoir dans la scène");
            return;
        }
        else
        {
            instance = this;
        }
    }

    
    void Update()
    {
        if (fondnoirActive)
        {
            spriteFondNoir.color = new Color(0, 0, 0, 1);
        }
        else
        {
            spriteFondNoir.color = new Color(0, 0, 0, 0);
        }
    }


    public void entrer()
    {
        
        spriteFondNoir.color = new Color(0, 0, 0, 0);
        StartCoroutine(entrerCoroutine());
    }

    IEnumerator entrerCoroutine()
    {
        float time = 0f;
        float duration = 1f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Clamp01(time / duration);
            spriteFondNoir.color = new Color(0, 0, 0, alpha);
            fondnoirActive = true;
            yield return null;
        }
    }

    public void sorti()
    {
        spriteFondNoir.color = new Color(0, 0, 0, 1);
        StartCoroutine(sortiCoroutine());
        
    }

    IEnumerator sortiCoroutine()
    {
        float time = 0f;
        float duration =1f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (time / duration));
            spriteFondNoir.color = new Color(0, 0, 0, alpha);
            fondnoirActive = false;
            yield return null;
        }
    }
}
