using UnityEngine;
using UnityEngine.UI;

public class ChangementVisage : MonoBehaviour
{
    public Image visage;

    

    public Sprite visage1;
    public Sprite visage2;
    public Sprite visage3;
    public Sprite visage4;
    public Sprite visage5;

    public Sprite visagechoose;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

  
    public static ChangementVisage instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ChangementVisage dans la scène");
            return;
        }
        else
        {
            instance = this;
        }
    }

    void start()
    {
        visage.sprite = visage1;
        visagechoose = visage1;
    }

    void Update()
    {
        Changementvisage();
    }

    public void Changementvisage(){

        if (playerHealth.currenthealth <= 0)
        {
            visagechoose = visage5;
        }
        else if (playerHealth.currenthealth <= (playerHealth.maxhealth*0.25))
        {
            visagechoose = visage4;
        }
        else if (playerHealth.currenthealth <= (playerHealth.maxhealth*0.5))
        {
            visagechoose = visage3;
        }
        else if (playerHealth.currenthealth <= (playerHealth.maxhealth*0.75))
        {
            visagechoose = visage2;
        }
        else if (playerHealth.currenthealth <= (playerHealth.maxhealth))
        {
            visagechoose = visage1;
        }

        visage.sprite = visagechoose;
    }
}
