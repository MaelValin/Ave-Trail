using UnityEngine;

public class ModManager : MonoBehaviour
{


    public string Mod;

    public static ModManager instance;

   

    private void Awake(){

        if(instance != null){
            Debug.LogWarning("Il y a plus d'une instance de ModManager dans la scène");
            return;
        }
        instance = this;

        // int randomMod = Random.Range(0, 3); // 0, 1, or 2
        // switch (randomMod)
        // {
        //     case 0:
        //         Mod = "facile";
        //         break;
        //     case 1:
        //         Mod = "moyen";
        //         break;
        //     case 2:
        //         Mod = "difficile";
        //         break;
        // }
        Mod = Mod; // For testing purposes, set a default mod

        Debug.Log("Mod choisi: " + Mod);

        //pv  PlayerHealth.instance.maxhealth = 100;
        //recharge shootplayer.instance.reloadTime = 0.5f;
        //vitesse PlayerMovement.instance.movespeed = 5f;
        
        //balle shootplayer.instance.typeBalle;
        //voiture CarComponent.instance.assistance = true;


        if(PlayerPrefs.GetString("typie_amelioration") == "0"){
            PlayerMovement.instance.movespeed = 5f;
        }
        else if(PlayerPrefs.GetString("typie_amelioration") == "1"){
            PlayerMovement.instance.movespeed = 7f;
        }
        else if(PlayerPrefs.GetString("typie_amelioration") == "2"){
            PlayerMovement.instance.movespeed = 9f;
        }
        else if(PlayerPrefs.GetString("typie_amelioration") == "3"){
            PlayerMovement.instance.movespeed = 11f;
        }


        if(PlayerPrefs.GetString("cabane_amelioration") == "0"){
            shootplayer.instance.typeBalle="balle";
        }
        else if(PlayerPrefs.GetString("cabane_amelioration") == "1"){
            shootplayer.instance.typeBalle="explosif";
        }
        else if(PlayerPrefs.GetString("cabane_amelioration") == "2"){
            shootplayer.instance.typeBalle="ricochet";
        }
        else if(PlayerPrefs.GetString("cabane_amelioration") == "3"){
            shootplayer.instance.typeBalle="lanceflam";
        }


        if(PlayerPrefs.GetString("local_amelioration") == "0"){
            PlayerHealth.instance.maxhealth = 100;
        }
        else if(PlayerPrefs.GetString("local_amelioration") == "1"){
            PlayerHealth.instance.maxhealth = 150;
        }
        else if(PlayerPrefs.GetString("local_amelioration") == "2"){
            PlayerHealth.instance.maxhealth = 250;
        }
        else if(PlayerPrefs.GetString("local_amelioration") == "3"){
            PlayerHealth.instance.maxhealth = 400;
        }

        if(PlayerPrefs.GetString("maison_amelioration") == "0"){
            shootplayer.instance.reloadTime = 2f;
        }
        else if(PlayerPrefs.GetString("maison_amelioration") == "1"){
            shootplayer.instance.reloadTime = 1.5f;
        }
        else if(PlayerPrefs.GetString("maison_amelioration") == "2"){
            shootplayer.instance.reloadTime = 1f;
        }
        else if(PlayerPrefs.GetString("maison_amelioration") == "3"){
            shootplayer.instance.reloadTime = 0.75f;
        }

         if(PlayerPrefs.GetString("garage_amelioration") == "0"){
            CarComportement.instance.Carassitance = false;
        }
        else if(PlayerPrefs.GetString("garage_amelioration") == "1"){
            CarComportement.instance.Carassitance = true;
        }
        
        
    }

    

}
