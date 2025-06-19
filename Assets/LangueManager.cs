using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;


public class LangueManager : MonoBehaviour
{
    //Menu
    public Text menuButtonStart;
    public Text menuButtonParametres;
    public Text menuExplication;
    public Text menuTextInteract;


    //Parametres
    public Text[] parametresTextResolution;
    public Text[] parametresTextFullScreen;
    public Text[] parametresTextVolumeMusic;
    public Text[] parametresTextVolumeEffect;
    public Text[] buttonQuitGame;
    public Text[] buttonClearDonnees;



    //village
    public Text villageTextMission;

    public Text villageTextInteract;
    public Text villageTextExplication;
    public Text villageTextControls;
    public Text villageTextControlsOr;
    public Text villageTextCinematicInteract;
    public Text villageTextCinematicExplication;
    public Text villageTextCinematicfin;

    public Text villagevaccinText;
    public Text villagevaccinTextInvestissement;
    public Text[] villagevaccinTextAchat;
    public Text villagevaccinTextDistribution;

    public Text villagemissionTextDifficulte1;
    public Text villagemissionTextDifficulte2;
    public Text villagemissionTextDifficulte3;
    public Text[] villageTextNonDisponible;

    public Text[] villageTextRetourVillage;

    public Text[] villageTextlevel;
    public Text[] villageTextAchat;
    public Text[] villageTextAmeliorationAucun;
    public Text[] villageTextlevelblock2;
    public Text[] villageTextlevelblock3;

    public Text villageTypieTextExplication;
    public Text villageTypieTextNamelevel;   

    public Text villageTypieTextAmelioration1titre;
    public Text villageTypieTextAmelioration1explication;   
    public Text villageTypieTextAmelioration2titre;
    public Text villageTypieTextAmelioration2explication;
    public Text villageTypieTextAmelioration3titre;
    public Text villageTypieTextAmelioration3explication;
    

    


    public Text villageCabaneTextExplication;
    public Text villageCabaneTextNamelevel;
    

    public Text villageCabaneTextAmelioration1titre;
    public Text villageCabaneTextAmelioration1explication;
    public Text villageCabaneTextAmelioration2titre;
    public Text villageCabaneTextAmelioration2explication;
    public Text villageCabaneTextAmelioration3titre;
    public Text villageCabaneTextAmelioration3explication;
    

    public Text villageLocalTextExplication;
    public Text villageLocalTextNamelevel;
    

    public Text villageLocalTextAmelioration1titre;
    public Text villageLocalTextAmelioration1explication;
    public Text villageLocalTextAmelioration2titre;
    public Text villageLocalTextAmelioration2explication;
    public Text villageLocalTextAmelioration3titre;
    public Text villageLocalTextAmelioration3explication;
    
    


    public Text villageMaisonTextExplication;
    public Text villageMaisonTextNamelevel;
    

    public Text villageMaisonTextAmelioration1titre;
    public Text villageMaisonTextAmelioration1explication;
    public Text villageMaisonTextAmelioration2titre;
    public Text villageMaisonTextAmelioration2explication;
    public Text villageMaisonTextAmelioration3titre;
    public Text villageMaisonTextAmelioration3explication;
    




    public Text villageGarageTextExplication;
    public Text villageGarageTextNamelevel;
    
    public Text villageGarageTextAmelioration1titre;

    public Text[] villageEntrer;
    public Text[] villageSortir;


    //Jeudetir

    public Text jeuTextPause;
    public Text jeuTextGameWinner;
    public Text jeuTextGameOver;
    public Text jeuTextGameOverExplication;
    


    public Text jeuTextparametres;
    public Text jeuTextretourJeu;
    public Text[] jeuTextRetourvillage;
    public Text[] jeuTextTime;
    public Text jeuTextInteract;


    public string langueFichier = "textjeuFrancais.json";
    
    private TextesJeu textes;
    public int indexLangue;

    public static LangueManager instance;


    void Awake()    
    {
        #if UNITY_WEBGL
            // Désactive le script si on est en WebGL
            this.enabled = false;
            return;
        #endif
        var parametreMenus=parametreMenu.instance;
        indexLangue = PlayerPrefs.GetInt("index", 0);
        if(parametreMenus !=null && parametreMenus.dropdownLangue != null)
        {
            parametreMenus.dropdownLangue.value = indexLangue;
        parametreMenus.dropdownLangue.RefreshShownValue();
        }
        
        adapteLangue(indexLangue);
        

        instance = this;
        ChargerLangue(langueFichier);
        InitializeTexts();
    }


    public void adapteLangue(int index){
         if (index == 0){
            ChangerLangue("TextjeuFrancais.json");
            PlayerPrefs.SetInt("index", 0);
            
        }
        else if (index == 1){
            ChangerLangue("TextjeuAnglais.json");
            PlayerPrefs.SetInt("index", 1);
            
        }
    }

    public void ChangerLangue(string nouveauFichier)
    {
        langueFichier = nouveauFichier;
        ChargerLangue(langueFichier);
        InitializeTexts();
    }

     void ChargerLangue(string fichier)
    {
        string path = Path.Combine(Application.streamingAssetsPath, fichier);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            textes = JsonUtility.FromJson<TextesJeu>(json);
        }
        else
        {
            Debug.LogError($"{fichier} non trouvé !");
            textes = new TextesJeu();
        }
    }

    string GetText(string key)
    {
        // Utilisation d'une réflexion simple pour accéder à la propriété
        var field = typeof(TextesJeu).GetField(key);
        if (field != null)
        {
            return field.GetValue(textes) as string;
        }
        return key;
    }

    void InitializeTexts()
    {
        if (SceneManager.GetActiveScene().name == "Menu"){
            menuButtonStart.text=GetText("menuButtonStart");
            menuButtonParametres.text = GetText("menuButtonParametres");
            menuExplication.text = GetText("menuExplication");
            menuTextInteract.text = GetText("menuTextInteract");
            buttonClearDonnees[0].text = GetText("buttonClearDonnees");
        }

        if (SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "village" || SceneManager.GetActiveScene().name == "Jeudetir"){
           
        parametresTextResolution[0].text = GetText("parametresTextResolution");
        parametresTextFullScreen[0].text = GetText("parametresTextFullScreen");
        parametresTextVolumeMusic[0].text = GetText("parametresTextVolumeMusic");
        parametresTextVolumeEffect[0].text = GetText("parametresTextVolumeEffect");
        buttonQuitGame[0].text = GetText("buttonQuitGame");
        }

        if (SceneManager.GetActiveScene().name == "village"){
        
            villageTextMission.text = GetText("villageTextMission");
            villageTextInteract.text = GetText("villageTextInteract");
            villageTextExplication.text = GetText("villageTextExplication");
            villageTextControls.text = GetText("villageTextControls");
            villageTextControlsOr.text = GetText("villageTextControlsOr");
            villageTextCinematicInteract.text = GetText("villageTextCinematicInteract");
            villageTextCinematicExplication.text = GetText("villageTextCinematicExplication");
            villageTextCinematicfin.text = GetText("villageTextCinematicfin");

            villagevaccinText.text = GetText("villagevaccinText");
            villagevaccinTextInvestissement.text = GetText("villagevaccinTextInvestissement");
            if (villagevaccinTextAchat != null && villagevaccinTextAchat.Length > 0)
                {
                string texte = GetText("villagevaccinTextAchat");
                for (int i = 0; i < villagevaccinTextAchat.Length; i++)
                {
                    villagevaccinTextAchat[i].text = texte;
                }
            }
            villagevaccinTextDistribution.text = GetText("villagevaccinTextDistribution");
            villagemissionTextDifficulte1.text = GetText("villagemissionTextDifficulte1");
            villagemissionTextDifficulte2.text = GetText("villagemissionTextDifficulte2");
            villagemissionTextDifficulte3.text = GetText("villagemissionTextDifficulte3");

            if (villageTextNonDisponible != null && villageTextNonDisponible.Length > 0)
            {
                string texte = GetText("villageTextNonDisponible");
                for (int i = 0; i < villageTextNonDisponible.Length; i++)
                {
                    villageTextNonDisponible[i].text = texte;
                }
            }
            

            if (villageTextRetourVillage != null && villageTextRetourVillage.Length > 0){
                string texte = GetText("villageTextRetourVillage");
                for (int i = 0; i < villageTextRetourVillage.Length; i++)
                {
                    villageTextRetourVillage[i].text = texte;
                }
            }

            if (villageTextlevel != null && villageTextlevel.Length > 0){
                string texte = GetText("villageTextlevel");
                for (int i = 0; i < villageTextlevel.Length; i++)
                {
                    villageTextlevel[i].text = texte;
                }
            }
            
            if (villageTextAchat != null && villageTextAchat.Length > 0)
            {
                string texte = GetText("villageTextAchat");
                for (int i = 0; i < villageTextAchat.Length; i++)
                {
                    villageTextAchat[i].text = texte;
                }
            }
            if (villageTextAmeliorationAucun != null && villageTextAmeliorationAucun.Length > 0){
                
                 string texte = GetText("villageTextAmeliorationAucun");
                for (int i = 0; i < villageTextAmeliorationAucun.Length; i++)
                {
                    villageTextAmeliorationAucun[i].text = texte;
                }
            }

            if (villageTextlevelblock2 != null && villageTextlevelblock2.Length > 0){
                string texte = GetText("villageTextlevelblock2");
                for (int i = 0; i < villageTextlevelblock2.Length; i++)
                {
                    villageTextlevelblock2[i].text = texte;
                }
            }
            if (villageTextlevelblock3 != null && villageTextlevelblock3.Length > 0){
                string texte = GetText("villageTextlevelblock3");
                for (int i = 0; i < villageTextlevelblock3.Length; i++)
                {
                    villageTextlevelblock3[i].text = texte;
                }
            }
                

            villageTypieTextExplication.text = GetText("villageTypieTextExplication");
            villageTypieTextNamelevel.text = GetText("villageTypieTextNamelevel");
            
            villageTypieTextAmelioration1titre.text = GetText("villageTypieTextAmelioration1titre");
            villageTypieTextAmelioration1explication.text = GetText("villageTypieTextAmelioration1explication");
            villageTypieTextAmelioration2titre.text = GetText("villageTypieTextAmelioration2titre");
            villageTypieTextAmelioration2explication.text = GetText("villageTypieTextAmelioration2explication");
            villageTypieTextAmelioration3titre.text = GetText("villageTypieTextAmelioration3titre");
            villageTypieTextAmelioration3explication.text = GetText("villageTypieTextAmelioration3explication");
            
            

            villageCabaneTextExplication.text = GetText("villageCabaneTextExplication");
            villageCabaneTextNamelevel.text = GetText("villageCabaneTextNamelevel");
            
            villageCabaneTextAmelioration1titre.text = GetText("villageCabaneTextAmelioration1titre");
            villageCabaneTextAmelioration1explication.text = GetText("villageCabaneTextAmelioration1explication");
            villageCabaneTextAmelioration2titre.text = GetText("villageCabaneTextAmelioration2titre");
            villageCabaneTextAmelioration2explication.text = GetText("villageCabaneTextAmelioration2explication");
            villageCabaneTextAmelioration3titre.text = GetText("villageCabaneTextAmelioration3titre");
            villageCabaneTextAmelioration3explication.text = GetText("villageCabaneTextAmelioration3explication");
            

            villageLocalTextExplication.text = GetText("villageLocalTextExplication");
            villageLocalTextNamelevel.text = GetText("villageLocalTextNamelevel");
            
            villageLocalTextAmelioration1titre.text = GetText("villageLocalTextAmelioration1titre");
            villageLocalTextAmelioration1explication.text = GetText("villageLocalTextAmelioration1explication");
            villageLocalTextAmelioration2titre.text = GetText("villageLocalTextAmelioration2titre");
            villageLocalTextAmelioration2explication.text = GetText("villageLocalTextAmelioration2explication");
            villageLocalTextAmelioration3titre.text = GetText("villageLocalTextAmelioration3titre");
            villageLocalTextAmelioration3explication.text = GetText("villageLocalTextAmelioration3explication");            

            villageMaisonTextExplication.text = GetText("villageMaisonTextExplication");
            villageMaisonTextNamelevel.text = GetText("villageMaisonTextNamelevel");
            
            villageMaisonTextAmelioration1titre.text = GetText("villageMaisonTextAmelioration1titre");
            villageMaisonTextAmelioration1explication.text = GetText("villageMaisonTextAmelioration1explication");
            villageMaisonTextAmelioration2titre.text = GetText("villageMaisonTextAmelioration2titre");
            villageMaisonTextAmelioration2explication.text = GetText("villageMaisonTextAmelioration2explication");
            villageMaisonTextAmelioration3titre.text = GetText("villageMaisonTextAmelioration3titre");
            villageMaisonTextAmelioration3explication.text = GetText("villageMaisonTextAmelioration3explication");
            

            villageGarageTextExplication.text = GetText("villageGarageTextExplication");
            villageGarageTextNamelevel.text = GetText("villageGarageTextNamelevel");
            
            villageGarageTextAmelioration1titre.text = GetText("villageGarageTextAmelioration1titre");

            if (villageSortir != null && villageSortir.Length > 0)
            {
                string texte = GetText("villageSortir");
                for (int i = 0; i < villageSortir.Length; i++)
                {
                    villageSortir[i].text = texte;
                }
            }

            if (villageEntrer != null && villageEntrer.Length > 0)
            {
                string texte = GetText("villageEntrer");
                for (int i = 0; i < villageEntrer.Length; i++)
                {
                    villageEntrer[i].text = texte;
                }
            }
            

        }

        if (SceneManager.GetActiveScene().name == "Jeudetir"){
            jeuTextPause.text = GetText("jeuTextPause");
            jeuTextGameWinner.text = GetText("jeuTextGameWinner");
            jeuTextGameOver.text = GetText("jeuTextGameOver");
            jeuTextGameOverExplication.text = GetText("jeuTextGameOverExplication");
            jeuTextparametres.text = GetText("jeuTextparametres");
            jeuTextretourJeu.text = GetText("jeuTextretourJeu");
            jeuTextInteract.text = GetText("jeuTextInteract");
            if (jeuTextRetourvillage != null && jeuTextRetourvillage.Length > 0)
                {
                string texte = GetText("jeuTextRetourvillage");
                for (int i = 0; i < jeuTextRetourvillage.Length; i++)
                {
                    jeuTextRetourvillage[i].text = texte;
                }
            }

            if (jeuTextTime != null && jeuTextTime.Length > 0)
                 {
                string texte = GetText("jeuTextTime");
                for (int i = 0; i < jeuTextTime.Length; i++)
                {
                    jeuTextTime[i].text = texte;
                }
            }

           

            
            
        }
    }

    [System.Serializable]
    public class TextesJeu
    {
        public string menuButtonStart;
        public string menuButtonParametres;
        public string menuExplication;
        public string menuTextInteract;
        public string parametresTextResolution;
        public string parametresTextFullScreen;
        public string parametresTextVolumeMusic;
        public string parametresTextVolumeEffect;
        public string buttonQuitGame;
        public string buttonClearDonnees;
        
        public string villageTextMission;
        public string villageTextInteract;
        public string villageTextExplication;
        public string villageTextControls;
        public string villageTextControlsOr;
        public string villageTextCinematicInteract; 
        public string villageTextCinematicExplication;
        public string villageTextCinematicfin;

        public string villagevaccinText;
        public string villagevaccinTextInvestissement;
        public string villagevaccinTextAchat;
        public string villagevaccinTextDistribution;
        public string villagemissionTextDifficulte1;    
        public string villagemissionTextDifficulte2;
        public string villagemissionTextDifficulte3;
        public string villageTextNonDisponible; // Pour les textes non disponibles, si nécessaire
        public string villageTextRetourVillage;

        public string villageTextlevel;
        public string villageTextAchat;
        public string villageTextAmeliorationAucun;
        public string villageTextlevelblock2;
        public string villageTextlevelblock3;


        public string villageTypieTextExplication;
        public string villageTypieTextNamelevel;
        public string villageTypieTextAmelioration1titre;
        public string villageTypieTextAmelioration1explication;
        public string villageTypieTextAmelioration2titre;
        public string villageTypieTextAmelioration2explication;
        public string villageTypieTextAmelioration3titre;
        public string villageTypieTextAmelioration3explication;
        

        public string villageCabaneTextExplication;
        public string villageCabaneTextNamelevel;
        

        public string villageCabaneTextAmelioration1titre;
        public string villageCabaneTextAmelioration1explication;
        public string villageCabaneTextAmelioration2titre;
        public string villageCabaneTextAmelioration2explication;
        public string villageCabaneTextAmelioration3titre;
        public string villageCabaneTextAmelioration3explication;
        

        public string villageLocalTextExplication;
        public string villageLocalTextNamelevel;
        public string villageLocalTextAmelioration1titre;
        public string villageLocalTextAmelioration1explication;
        public string villageLocalTextAmelioration2titre;
        public string villageLocalTextAmelioration2explication;
        public string villageLocalTextAmelioration3titre;
        public string villageLocalTextAmelioration3explication;

        


        public string villageMaisonTextExplication;
        public string villageMaisonTextNamelevel;
        public string villageMaisonTextAmelioration1titre;
        public string villageMaisonTextAmelioration1explication;
        public string villageMaisonTextAmelioration2titre;
        public string villageMaisonTextAmelioration2explication;
        public string villageMaisonTextAmelioration3titre;
        public string villageMaisonTextAmelioration3explication;




        public string villageGarageTextExplication;
        public string villageGarageTextNamelevel;
        
        public string villageGarageTextAmelioration1titre;

        
        public string villageSortir;
        public string villageEntrer; // Pour les textes d'entrée du village, si nécessaire


        //Jeudetir

        public string jeuTextPause;
        public string jeuTextGameWinner;
        public string jeuTextGameOver;
        public string jeuTextGameOverExplication;


        public string jeuTextparametres;
        public string jeuTextretourJeu;
        public string jeuTextRetourvillage;
        public string jeuTextTime;
        public string jeuTextInteract;
        

        public Dictionary<string, string> ToDictionary()
        {
            var dict = new Dictionary<string, string>();
            dict["menuButtonStart"]= menuButtonStart;
            dict["menuButtonParametres"] = menuButtonParametres;
            dict["menuExplication"] = menuExplication;
            dict["menuTextInteract"] = menuTextInteract;
            dict["parametresTextResolution"] = parametresTextResolution;
            dict["parametresTextFullScreen"] = parametresTextFullScreen;
            dict["parametresTextVolumeMusic"] = parametresTextVolumeMusic;
            dict["parametresTextVolumeEffect"] = parametresTextVolumeEffect;
            dict["buttonQuitGame"] = buttonQuitGame;
            dict["buttonClearDonnees"] = buttonClearDonnees;
            dict["villageTextMission"] = villageTextMission;
            dict["villageTextInteract"] = villageTextInteract;
            dict["villageTextExplication"] = villageTextExplication;
            dict["villageTextControls"] = villageTextControls;
            dict["villageTextControlsOr"] = villageTextControlsOr;
            dict["villageTextCinematicInteract"] = villageTextCinematicInteract;
            dict["villageTextCinematicExplication"] = villageTextCinematicExplication;
            dict["villageTextCinematicfin"] = villageTextCinematicfin;

            dict["villagevaccinText"] = villagevaccinText;
            dict["villagevaccinTextInvestissement"] = villagevaccinTextInvestissement;
            dict["villagevaccinTextAchat"] = villagevaccinTextAchat;
            dict["villagevaccinTextDistribution"] = villagevaccinTextDistribution;
            dict["villagemissionTextDifficulte1"] = villagemissionTextDifficulte1;
            dict["villagemissionTextDifficulte2"] = villagemissionTextDifficulte2;
            dict["villagemissionTextDifficulte3"] = villagemissionTextDifficulte3;
            dict["villageTextNonDisponible"] = villageTextNonDisponible;
            dict["villageTextRetourVillage"] = villageTextRetourVillage;

            dict["villageTextlevel"] = villageTextlevel;
            dict["villageTextAchat"] = villageTextAchat;
            dict["villageTextAmeliorationAucun"] = villageTextAmeliorationAucun;
            dict["villageTextlevelblock2"] = villageTextlevelblock2;
            dict["villageTextlevelblock3"] = villageTextlevelblock3;

            dict["villageTypieTextExplication"] = villageTypieTextExplication;
            dict["villageTypieTextNamelevel"] = villageTypieTextNamelevel;
            
            dict["villageTypieTextAmelioration1titre"] = villageTypieTextAmelioration1titre;
            dict["villageTypieTextAmelioration1explication"] = villageTypieTextAmelioration1explication;
            dict["villageTypieTextAmelioration2titre"] = villageTypieTextAmelioration2titre;
            dict["villageTypieTextAmelioration2explication"] = villageTypieTextAmelioration2explication;
            dict["villageTypieTextAmelioration3titre"] = villageTypieTextAmelioration3titre;
            dict["villageTypieTextAmelioration3explication"] = villageTypieTextAmelioration3explication;


            dict["villageCabaneTextExplication"] = villageCabaneTextExplication;
            dict["villageCabaneTextNamelevel"] = villageCabaneTextNamelevel;
            dict["villageCabaneTextAmelioration1titre"] = villageCabaneTextAmelioration1titre;
            dict["villageCabaneTextAmelioration1explication"] = villageCabaneTextAmelioration1explication;
            dict["villageCabaneTextAmelioration2titre"] = villageCabaneTextAmelioration2titre;
            dict["villageCabaneTextAmelioration2explication"] = villageCabaneTextAmelioration2explication;
            dict["villageCabaneTextAmelioration3titre"] = villageCabaneTextAmelioration3titre;
            dict["villageCabaneTextAmelioration3explication"] = villageCabaneTextAmelioration3explication;
            

            dict["villageLocalTextExplication"] = villageLocalTextExplication;
            dict["villageLocalTextNamelevel"] = villageLocalTextNamelevel;
            dict["villageLocalTextAmelioration1titre"] = villageLocalTextAmelioration1titre;
            dict["villageLocalTextAmelioration1explication"] = villageLocalTextAmelioration1explication;
            dict["villageLocalTextAmelioration2titre"] = villageLocalTextAmelioration2titre;
            dict["villageLocalTextAmelioration2explication"] = villageLocalTextAmelioration2explication;
            dict["villageLocalTextAmelioration3titre"] = villageLocalTextAmelioration3titre;
            dict["villageLocalTextAmelioration3explication"] = villageLocalTextAmelioration3explication;
            

            dict["villageMaisonTextExplication"] = villageMaisonTextExplication;
            dict["villageMaisonTextNamelevel"] = villageMaisonTextNamelevel;
            dict["villageMaisonTextAmelioration1titre"] = villageMaisonTextAmelioration1titre;
            dict["villageMaisonTextAmelioration1explication"] = villageMaisonTextAmelioration1explication;
            dict["villageMaisonTextAmelioration2titre"] = villageMaisonTextAmelioration2titre;
            dict["villageMaisonTextAmelioration2explication"] = villageMaisonTextAmelioration2explication;
            dict["villageMaisonTextAmelioration3titre"] = villageMaisonTextAmelioration3titre;
            dict["villageMaisonTextAmelioration3explication"] = villageMaisonTextAmelioration3explication;
            

            dict["villageGarageTextExplication"] = villageGarageTextExplication;
            dict["villageGarageTextNamelevel"] = villageGarageTextNamelevel;
            dict["villageGarageTextAmelioration1titre"] = villageGarageTextAmelioration1titre;

            dict["villageSortir"] = villageSortir;
            dict["villageEntrer"] = villageEntrer;

            dict["jeuTextPause"] = jeuTextPause;
            dict["jeuTextGameWinner"] = jeuTextGameWinner;
            dict["jeuTextGameOver"] = jeuTextGameOver;
            dict["jeuTextGameOverExplication"] = jeuTextGameOverExplication;
            dict["jeuTextparametres"] = jeuTextparametres;
            dict["jeuTextretourJeu"] = jeuTextretourJeu;
            dict["jeuTextRetourvillage"] = jeuTextRetourvillage;
            dict["jeuTextTime"] = jeuTextTime;
            dict["jeuTextInteract"] = jeuTextInteract;


            return dict;
        }
    }
}