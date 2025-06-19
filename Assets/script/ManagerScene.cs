using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManagerScene : MonoBehaviour
{
    public Sprite visage1;
    public Text[] time;
    public float maxTime = 30f;

    public float timer = 0f;
    private int lastSecond = 0;

    public static ManagerScene instance;

    public bool PartyIsPlay;

    public Text[] NumberKill;

    private int timetocar = 10;

    public Text textInteract;

    private bool CarisArrived = false;

    

    public Text[] NumberArgent;
    public Text[] NumberRessources;
    public Text[] NumberBalle;
    public Image[] visage;

    public Text[] difficulte;

     private Coroutine decreaseCoroutine;

// En haut de la classe
    public NumberArgent numberArgentScript;
    public NumberBalle numberBalleScript;
    public NumberRessources numberRessourcesScript;
    

    public int nombreArgent;
    public int nombreBalle;
    public int nombreBalleScore;
    public int nombreRessources;
    public int nombreRessourcesScore;
    public int nombreKill;
    public int nombreKillScore;
    public int timescore = 0;
    public int scoreFinal;

    public Text interacttext;






    private void Awake()   

    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ManagerScene dans la scène");
            return;
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1;
        timer = 0f;

        nombreKill = 0;
        foreach (Text t in NumberKill)
        {
            t.text = nombreKill.ToString();
        }

        foreach (Image v in visage)
        {
            v.sprite  = visage1;
        }

        fondnoir.instance.fondnoirActive = true;
        fondnoir.instance.sorti();

        if(ModManager.instance.Mod == "difficile"){
            interacttext.text = "Rejoindre la corde et sortez avec la touche E";
        }
        else{
            interacttext.text = "Rejoindre la voiture et rentrez avec la touche E";
        }

        if(ModManager.instance.Mod == "facile")
        {
            foreach (Text t in difficulte)
            {
                t.text = "Facile";
            }
        }
        else if(ModManager.instance.Mod == "moyen")
        {
            foreach (Text t in difficulte)
            {
                t.text = "Moyen";
        }
        }
        else if(ModManager.instance.Mod == "difficile")
        {
            foreach (Text t in difficulte)
            {
                t.text = "Difficile";
            }
        }
    }
    

    void Update()
    {

        if(Time.timeScale == 0)
        {
            PartyIsPlay = false;
        }else
        {
            PartyIsPlay = true;
        }


        
            timer += Time.deltaTime;
            int currentSecond = Mathf.FloorToInt(timer);
            if (currentSecond > lastSecond)
            {
                timescore += 10; // Ajoute 10 points chaque seconde écoulée
                lastSecond = currentSecond;
            }
            
        

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        string timerText = string.Format("{0}:{1:00}", minutes, seconds);
        foreach (Text t in time)
        {
            t.text = timerText;
        }



        // if(timer >= maxTime)
        // {
        //     GameWinnerManager.instance.OnPlayerWin();
        // }

        // Trigger CarArrived once when timer passes (maxTime - 15)
        if (timer >= (maxTime - timetocar) && timer - Time.deltaTime < (maxTime - timetocar))
        {
            
            if(ModManager.instance.Mod == "difficile"){
            CordeComportement.instance.Corde.SetActive(true); // Active le script CordeComportement
            CordeComportement.instance.CordeArrived();
            CarisArrived = true;
            }
            else{
                CarComportement.instance.car.SetActive(true); // Active le script CarComportement
            CarComportement.instance.CarArrived();
            CarisArrived = true;
            }

            Itemspawner.instance.UpdateSpawnItemsInterval(Itemspawner.instance.spawnInterval-1f);
                ennemieSpawnner.instance.UpdateSpawnInterval(ennemieSpawnner.instance.spawnInterval-1f);

                StartDecreaseSpawnInterval(); 

        }

        if (CarisArrived == true)
        {
            CarisArrived = false; // Pour éviter de lancer plusieurs fois la coroutine
            StartCoroutine(BlinkTextInteract());
        }



        

        nombreArgent = numberArgentScript.nombreArgent;
        nombreBalle = numberBalleScript.nombreBalle;
        nombreRessources = numberRessourcesScript.nombreRessources;
        nombreBalleScore = (nombreBalle*10);
        nombreRessourcesScore = (nombreRessources*100);
        nombreKillScore = (nombreKill*10);
        scoreFinal = ((nombreKill*10)+(nombreBalle*10)+(nombreArgent)+(nombreRessources*100)+timescore);

        foreach (Text t in NumberArgent)
        {
            t.text = nombreArgent.ToString();
        }
        foreach (Text t in NumberRessources)
        {
            t.text = nombreRessources.ToString();
        }

        foreach (Text t in NumberBalle)
        {
            t.text = nombreBalle.ToString();
        }

        foreach (Image v in visage)
        {
            v.sprite  = ChangementVisage.instance.visagechoose;
        }

        



    }

   

public void StartDecreaseSpawnInterval()
{
    if (decreaseCoroutine == null)
        decreaseCoroutine = StartCoroutine(DecreaseSpawnInterval());
}

private IEnumerator DecreaseSpawnInterval()
{
    while (Itemspawner.instance.spawnInterval > 3f)
    {
        yield return new WaitForSeconds(4f); // Attend 1 seconde
        ennemieSpawnner.instance.UpdateSpawnInterval(ennemieSpawnner.instance.spawnInterval-0.2f);
    }
}

    IEnumerator BlinkTextInteract()
    {
        float blinkDuration = 200f;
        float blinkInterval = 0.7f; // Durée entre chaque clignotement (plus grand = plus lent)
        float elapsed = 0f;
        bool isVisible = true;

        while (elapsed < blinkDuration)
        {
            textInteract.gameObject.SetActive(isVisible);
            isVisible = !isVisible;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }
        textInteract.gameObject.SetActive(false);
    }


    public void AddKill()
    {
        nombreKill++;
        foreach (Text t in NumberKill)
        {
            t.text = nombreKill.ToString();
        }
    }
}