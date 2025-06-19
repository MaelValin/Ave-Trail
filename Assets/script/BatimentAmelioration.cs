using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class BatimentAmelioration : MonoBehaviour
{
    public Sprite niveau1;
    public Sprite niveau2;
    public Sprite niveau3;

    public Image[] batimentSprites;

    public GameObject OngletBatiment;
    public GameObject OngletAmelioration;

    public string niveau = "Niveau 0";
    public string ameliorationchoisi= "0";

    public GameObject buttonAchat;
    public GameObject ObjetAmelioration;

    public Text textniveau;
    public Text textFuturNiveau;
    public GameObject buttonAmelioration;

    public float priceAchat;
    public float priceAmelioration;
    public float priceAmelioration2;

    public Text textPriceAchat;
    public Text textPriceAmelioration;

    public Text priceamelioration1text;
    public Text priceamelioration2text;
    public Text priceamelioration3text;

    public string priceamelioration1;
    public string priceamelioration2;
    public string priceamelioration3;

    public Toggle Amelioration1;
    public Toggle Amelioration2;
    public Toggle Amelioration3;
    public Toggle aucun;

    
    public GameObject fondbloquerAmelioration2;
    public GameObject fondbloquerAmelioration3;



    public GameObject batiment;
    public Button batimentButton;
    public string batimentID; // À renseigner dans l'inspecteur Unity (ex: "maison1", "maison2", "usineA", etc.)
    


    //public static BatimentAmelioration instance;

    // private void Awake()
    // {
    //     if (instance != null)
    //     {
    //         Debug.LogWarning("Il y a plus d'une instance de BatimentAmelioration dans la scène");
    //         return;
    //     }
    //     else
    //     {
    //         instance = this;
    //     }
    // }

    

    void Start()
    {

        
        OngletBatiment.SetActive(false);
        OngletAmelioration.SetActive(false);
        ObjetAmelioration.SetActive(false);
        buttonAchat.SetActive(true);
        textniveau.text = "1";
        textFuturNiveau.text = "2";
        textPriceAchat.text = priceAchat.ToString();
        textPriceAmelioration.text = priceAmelioration.ToString();
        ColorBlock cb = batimentButton.colors;
        cb.normalColor = new Color(0.349f, 0.349f, 0.349f, 1f);
        batimentButton.colors = cb;
        foreach (Image sprite in batimentSprites)
        {
            sprite.sprite = niveau1;
        }

        priceamelioration1text.text = priceamelioration1;
        priceamelioration2text.text = priceamelioration2;
        priceamelioration3text.text = priceamelioration3;

        aucun.isOn = true; // Assurez-vous qu'aucun est sélectionné par défaut
        // Ajoute ceci pour initialiser la couleur au démarrage
        //colortoggle();

        // Ajoute l'écouteur sur chaque toggle
        Amelioration1.onValueChanged.AddListener(delegate { colortoggle(); });
       Amelioration1.onValueChanged.AddListener(OnAmelioration1Selected);
        Amelioration2.onValueChanged.AddListener(delegate { colortoggle(); });
        Amelioration2.onValueChanged.AddListener(OnAmelioration2Selected);
        Amelioration3.onValueChanged.AddListener(delegate { colortoggle(); });
        Amelioration3.onValueChanged.AddListener(OnAmelioration3Selected);
        aucun.onValueChanged.AddListener(delegate { colortoggle(); });
       aucun.onValueChanged.AddListener(OnAucunSelected);
        
    UpdateAmeliorationToggles();
    
    LoadBatiment();
        
    }

    void Update()
    {
        // Met à jour les boutons d'amélioration en fonction des ressources disponibles
        UpdateAmeliorationButton();
    }

   
   
    
    public void OpenOngletBatiment()
    {
        OngletBatiment.SetActive(true);
        
    }

    public void OpenOngletAmelioration()
    {
        OngletAmelioration.SetActive(true);
    }

    public void RetourBatiment()
    {
        
        OngletAmelioration.SetActive(false);
        
    }

    public void RetourVillage()
    {
        OngletBatiment.SetActive(false);
        OngletAmelioration.SetActive(false);
    }


    public void AchatBatiment()
    {
        if(Inventaire.instance.NourritureQuantity >= priceAchat)
        {
            buttonAchat.SetActive(false);
        ObjetAmelioration.SetActive(true);
        Inventaire.instance.RemoveNourriture(priceAchat);
        UpdateAmeliorationButton();

        ColorBlock cb = batimentButton.colors;
        cb.normalColor = new Color(0.89f, 0.89f, 0.89f, 1f); // E4E4E4
        batimentButton.colors = cb;
        niveau = "Niveau 1";
        }
        else{
            return;
        }
        

    }

    public void Amelioration()
    {
        if (niveau == "Niveau 1"&& Inventaire.instance.NourritureQuantity >= priceAmelioration)
        {
            foreach (Image sprite in batimentSprites)
        {
            sprite.sprite = niveau2;
        }
            textniveau.text = "2";
            textFuturNiveau.text = "3";
            textPriceAmelioration.text = priceAmelioration2.ToString();
            Inventaire.instance.RemoveNourriture(priceAmelioration);
            niveau = "Niveau 2";
            Amelioration2.interactable = true;
            fondbloquerAmelioration2.SetActive(false);
            UpdateAmeliorationButton();
            
        }

        else if (niveau == "Niveau 2"&& Inventaire.instance.NourritureQuantity >= priceAmelioration2)
        {
            foreach (Image sprite in batimentSprites)
        {
            sprite.sprite = niveau3;
        }
            textniveau.text = "3";
            textFuturNiveau.text = "3";
            buttonAmelioration.SetActive(false);
            Inventaire.instance.RemoveNourriture(priceAmelioration2);
            niveau = "Niveau 3";
            Amelioration3.interactable = true;
            fondbloquerAmelioration3.SetActive(false);
            UpdateAmeliorationButton();
        }
        else if (niveau == "Niveau 3")
        {
           return; // Ne rien faire si déjà au niveau 3
        }

        
    }

    void UpdateAmeliorationButton()
    {
        float Ressource = Inventaire.instance.NourritureQuantity;

        buttonAchat.GetComponent<Button>().interactable = Ressource >= priceAchat;

        // Vérifie le niveau pour choisir le bon prix d'amélioration
        if (niveau == "Niveau 1")
            buttonAmelioration.GetComponent<Button>().interactable = Ressource >= priceAmelioration;
        else if (niveau == "Niveau 2")
            buttonAmelioration.GetComponent<Button>().interactable = Ressource >= priceAmelioration2;
        
    }

    void UpdateAmeliorationToggles()
    {
        float argent = Inventaire.instance.ArgentQuantity;

        // Amelioration1
        if (!Amelioration1.isOn )
            Amelioration1.interactable = argent >= float.Parse(priceamelioration1);
        else
            Amelioration1.interactable = true;

        // Amelioration2
        if (!Amelioration2.isOn)
            Amelioration2.interactable = argent >= float.Parse(priceamelioration2);
        else
            Amelioration2.interactable = true;

        // Amelioration3
        if (!Amelioration3.isOn)
            Amelioration3.interactable = argent >= float.Parse(priceamelioration3);
        else
            Amelioration3.interactable = true;
    }

    
    private int lastSelected = -1;

        private void OnAmelioration1Selected(bool isOn)
        {
            if (isOn)
            {
                if (lastSelected != 0)
                {
                    Inventaire.instance.RemoveArgent(float.Parse(priceamelioration1));
                    lastSelected = 0;
                    UpdateAmeliorationToggles();
                    ameliorationchoisi = "1"; // Met à jour l'amélioration choisie
                }
            }
        }

        private void OnAmelioration2Selected(bool isOn)
        {
            if (isOn)
            {
                if (lastSelected != 1)
                {
                    Inventaire.instance.RemoveArgent(float.Parse(priceamelioration2));
                    lastSelected = 1;
                    UpdateAmeliorationToggles();
                    ameliorationchoisi = "2"; // Met à jour l'amélioration choisie
                }
            }
        }

        private void OnAmelioration3Selected(bool isOn)
        {
            if (isOn)
            {
                if (lastSelected != 2)
                {
                    Inventaire.instance.RemoveArgent(float.Parse(priceamelioration3));
                    lastSelected = 2;
                    UpdateAmeliorationToggles();
                    ameliorationchoisi = "3"; // Met à jour l'amélioration choisie
                }
            }
        }

        private void OnAucunSelected(bool isOn)
        {
            if (isOn)
            {
                lastSelected = -1;
                UpdateAmeliorationToggles();
                ameliorationchoisi = "0"; // Réinitialise l'amélioration choisie
            }
        }
   

    

        private void colortoggle()
        {
             Color selected = new Color(0.353f, 0.561f, 0.173f, 0.364f);
    Color normal = new Color(1f, 1f, 1f, 0f);

    ColorBlock cb1 = Amelioration1.colors;
    cb1.normalColor = Amelioration1.isOn ? selected : normal;
    cb1.selectedColor = Amelioration1.isOn ? selected : normal;
    Amelioration1.colors = cb1;

    ColorBlock cb2 = Amelioration2.colors;
    cb2.normalColor = Amelioration2.isOn ? selected : normal;
    cb2.selectedColor = Amelioration2.isOn ? selected : normal;
    Amelioration2.colors = cb2;

    ColorBlock cb3 = Amelioration3.colors;
    cb3.normalColor = Amelioration3.isOn ? selected : normal;
    cb3.selectedColor = Amelioration3.isOn ? selected : normal;
    Amelioration3.colors = cb3;

    ColorBlock cbaucun = aucun.colors;
    cbaucun.normalColor = aucun.isOn ? selected : normal;
    cbaucun.selectedColor = aucun.isOn ? selected : normal;
    aucun.colors = cbaucun;

        }


        private void OnDestroy()
{
    
    Amelioration1.onValueChanged.RemoveListener(OnAmelioration1Selected);

    
    Amelioration2.onValueChanged.RemoveListener(OnAmelioration2Selected);

    
    Amelioration3.onValueChanged.RemoveListener(OnAmelioration3Selected);

    
    aucun.onValueChanged.RemoveListener(OnAucunSelected);
}

    public void SaveToBatimentData()
    {
        PlayerPrefs.SetString(batimentID + "_niveau", niveau);
        PlayerPrefs.SetString(batimentID + "_amelioration", ameliorationchoisi);
    
        Debug.Log("Données du bâtiment sauvegardées pour " + batimentID + ": Niveau " + niveau + ", Amélioration " + ameliorationchoisi);

    }

    public void LoadBatiment()
    {
        niveau = PlayerPrefs.GetString(batimentID + "_niveau", "Niveau 0");
    ameliorationchoisi = PlayerPrefs.GetString(batimentID + "_amelioration", "0");
    AppliquerNiveau();
    AppliquerAmelioration();
        }

    


 public void AppliquerAmelioration(){
        if(ameliorationchoisi == "0")
        {
            aucun.isOn = true;
            Amelioration1.isOn = false;
            Amelioration2.isOn = false;
            Amelioration3.isOn = false;
        }
        else if(ameliorationchoisi == "1")
        {
            aucun.isOn = false;
            Amelioration1.isOn = true;
            Amelioration2.isOn = false;
            Amelioration3.isOn = false;
        }
        else if(ameliorationchoisi == "2")
        {
            aucun.isOn = false;
            Amelioration1.isOn = false;
            Amelioration2.isOn = true;
            Amelioration3.isOn = false;
        }
        else if(ameliorationchoisi == "3")
        {
            aucun.isOn = false;
            Amelioration1.isOn = false;
            Amelioration2.isOn = false;
            Amelioration3.isOn = true;
        }
        
    }

    public void AppliquerNiveau()
{
    if(niveau == "Niveau 0")
    {
        
        textPriceAchat.text = priceAchat.ToString();
        textPriceAmelioration.text = priceAmelioration.ToString();
        foreach (Image sprite in batimentSprites)
        {
            sprite.sprite = niveau1;
        }
        ColorBlock cb = batimentButton.colors;
        cb.normalColor = new Color(0.349f, 0.349f, 0.349f, 1f);
        batimentButton.colors = cb;
        buttonAchat.SetActive(true);
        ObjetAmelioration.SetActive(false);
        
    }
    

    if(niveau == "Niveau 1")
    {
        textniveau.text = "1";
        textFuturNiveau.text = "2";
        textPriceAchat.text = priceAchat.ToString();
        textPriceAmelioration.text = priceAmelioration.ToString();
        foreach (Image sprite in batimentSprites)
        {
            sprite.sprite = niveau1;
        }

            Amelioration2.interactable = true;
            fondbloquerAmelioration2.SetActive(false);
            
            ColorBlock cb = batimentButton.colors;
        cb.normalColor = new Color(0.89f, 0.89f, 0.89f, 1f); // E4E4E4
        batimentButton.colors = cb;
        buttonAchat.SetActive(false);
        ObjetAmelioration.SetActive(true);
    }
    else if(niveau == "Niveau 2")
    {
        textniveau.text = "2";
        textFuturNiveau.text = "3";
        textPriceAchat.text = priceAchat.ToString();
        textPriceAmelioration.text = priceAmelioration2.ToString();
        foreach (Image sprite in batimentSprites)
        {
            sprite.sprite = niveau2;
        }

            Amelioration3.interactable = true;
            fondbloquerAmelioration3.SetActive(false);
            
            ColorBlock cb = batimentButton.colors;
        cb.normalColor = new Color(0.89f, 0.89f, 0.89f, 1f); // E4E4E4
        batimentButton.colors = cb;
        buttonAchat.SetActive(false);
        ObjetAmelioration.SetActive(true);
        Amelioration2.interactable = true;
            fondbloquerAmelioration2.SetActive(false);
    }
    else if(niveau == "Niveau 3")
    {
        textniveau.text = "3";
        textFuturNiveau.text = "3";
        buttonAmelioration.SetActive(false);
        foreach (Image sprite in batimentSprites)
        {
            sprite.sprite = niveau3;
        }
        ColorBlock cb = batimentButton.colors;
        cb.normalColor = new Color(0.89f, 0.89f, 0.89f, 1f); // E4E4E4
        batimentButton.colors = cb;
        buttonAchat.SetActive(false);
        ObjetAmelioration.SetActive(true);

        Amelioration2.interactable = true;
            fondbloquerAmelioration2.SetActive(false);

            Amelioration3.interactable = true;
            fondbloquerAmelioration3.SetActive(false);
    }
       
}

    
    
}
