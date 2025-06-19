using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadAndSaveData : MonoBehaviour
{
    public static LoadAndSaveData instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de LoadAndSaveData dans la scène");
            return;
        }
        else
        {
            instance = this;
        }

        

        if (SceneManager.GetActiveScene().name == "Jeudetir")
        {
            LoadDataGame();
        }
    }

    void Start()
    {        
        if (SceneManager.GetActiveScene().name == "village")
        {
            LoadDataVillage();
            
        }
    }

    // Méthode pour sauvegarder les données
    public void SaveDataGame()
    {
        PlayerPrefs.SetInt("argent", NumberArgent.instance.nombreArgent);
        PlayerPrefs.SetInt("nourriture", NumberRessources.instance.nombreRessources);
        PlayerPrefs.SetInt("ballesInventaire", NumberBalle.instance.nombreBalle);
        
    }

     public void SaveDataVillage()
    {
    if (Inventaire.instance != null)
    {
        PlayerPrefs.SetInt("argentInventaire", (int)Inventaire.instance.ArgentQuantity);
        PlayerPrefs.SetInt("nourritureInventaire", (int)Inventaire.instance.NourritureQuantity);
    }
    

    if (VaccinManager.instance != null)
    {
        PlayerPrefs.SetInt("currentArgent", VaccinManager.instance.currentargent);
        PlayerPrefs.SetInt("currentNourriture", VaccinManager.instance.currentnourriture);
    }
    
    

    if (MainMenu.instance != null)
    {
        PlayerPrefs.SetString("difficulty", MainMenu.instance.difficultychoose);
    }
   PlayerPrefs.SetInt("argent", 0);
PlayerPrefs.SetInt("nourriture", 0);

// Sauvegarder tous les bâtiments
foreach (BatimentAmelioration bat in FindObjectsOfType<BatimentAmelioration>())
{
    bat.SaveToBatimentData();
}
    
}

    // Méthode pour charger les données
    
    public void LoadDataVillage()
{

    
    if (Inventaire.instance != null)
    {
        // Additionne les gains de mission et l'inventaire du village
        int totalArgent = PlayerPrefs.GetInt("argent", 0) + PlayerPrefs.GetInt("argentInventaire", 0);
        int totalNourriture = PlayerPrefs.GetInt("nourriture", 0) + PlayerPrefs.GetInt("nourritureInventaire", 0);
        

        Inventaire.instance.ArgentQuantity = totalArgent;
        Inventaire.instance.NourritureQuantity = totalNourriture;
        Inventaire.instance.BalleQuantity = PlayerPrefs.GetInt("ballesInventaire", 10);

        // Mets à jour l'inventaire du village avec le nouveau total
        PlayerPrefs.SetInt("argentInventaire", totalArgent);
        PlayerPrefs.SetInt("nourritureInventaire", totalNourriture);

        // Remets à zéro les gains de mission
        PlayerPrefs.SetInt("argent", 0);
        PlayerPrefs.SetInt("nourriture", 0);

    }


    
        foreach (BatimentAmelioration bat in FindObjectsOfType<BatimentAmelioration>())
{
    bat.LoadBatiment();
}

    
    
}

    public void LoadDataVaccin(){
        
    
        VaccinManager.instance.currentargent = PlayerPrefs.GetInt("currentArgent", 0);
        VaccinManager.instance.currentnourriture = PlayerPrefs.GetInt("currentNourriture", 0);
            VaccinManager.instance.RefreshBarres();

    
    }
    

    public void LoadDataGame()
    {
        NumberBalle.instance.nombreBalle= PlayerPrefs.GetInt("ballesInventaire", 0);
        ModManager.instance.Mod = PlayerPrefs.GetString("difficulty", "facile");
    }
}
