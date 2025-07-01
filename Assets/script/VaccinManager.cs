using UnityEngine;
using UnityEngine.UI;

public class VaccinManager : MonoBehaviour
{

    public GameObject four1;
    public GameObject four2;
    public GameObject four3;
    public GameObject four4;
    public GameObject four5;

    public int maxnourriture = 1000;
    public int currentnourriture;

    public int maxargent = 1000;
    public int currentargent;

    public barrenourriture barrenourriture;
    public barreargent barreargent;

    public Button buttonnourriture;
    public Button buttonargent;

    public GameObject buttonnourritureobject;
    public GameObject buttonargentobject;

    public Text pricenourriture;
    public Text priceargent;

    public int prixnourriture = 100;
    public int prixargent = 100;

    public GameObject buttonfin;
    public GameObject investissement;

    public static VaccinManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de VaccinManager dans la scène");
            return;
        }
        else
        {
            instance = this;
        }

        if(PlayerPrefs.GetString("four") == "four1")
        {
            four1.SetActive(true);
            four2.SetActive(false);
            four3.SetActive(false);
            four4.SetActive(false);
            four5.SetActive(false);
        }
        else if (PlayerPrefs.GetString("four") == "four2")
        {
            four1.SetActive(false);
            four2.SetActive(true);
            four3.SetActive(false);
            four4.SetActive(false);
            four5.SetActive(false);
        }
        else if (PlayerPrefs.GetString("four") == "four3")
        {
            four1.SetActive(false);
            four2.SetActive(false);
            four3.SetActive(true);
            four4.SetActive(false);
            four5.SetActive(false);
        }
        else if (PlayerPrefs.GetString("four") == "four4")
        {
            four1.SetActive(false);
            four2.SetActive(false);
            four3.SetActive(false);
            four4.SetActive(true);
            four5.SetActive(false);
        }
        else if (PlayerPrefs.GetString("four") == "four5")
        {
            four1.SetActive(false);
            four2.SetActive(false);
            four3.SetActive(false);
            four4.SetActive(false);
            four5.SetActive(true);
        }

        
    }


    void Start()
{
    LoadAndSaveData.instance.LoadDataVaccin();
    barrenourriture.SetMaxBarrenourriture(maxnourriture);
    barreargent.SetMaxbarreargent(maxargent);

    pricenourriture.text = prixnourriture.ToString();
    priceargent.text = prixargent.ToString();
}

    public void RefreshBarres()
{
    barrenourriture.SetBarrenourriture(currentnourriture);
    barreargent.Setbarreargent(currentargent);
}

    void Update()
    {

        
        RefreshBarres();

        if (prixnourriture <= Inventaire.instance.NourritureQuantity)
            {
                buttonnourriture.interactable = true;
            }
            else
            {
                buttonnourriture.interactable = false;
            }

        if (prixargent <= Inventaire.instance.ArgentQuantity)
            {
                buttonargent.interactable = true;
            }
            else
            {
                buttonargent.interactable = false;
            }

        if (currentnourriture >= maxnourriture && currentargent >= maxargent){
            buttonfin.SetActive(true);
            investissement.SetActive(false);
        }
        else{
            buttonfin.SetActive(false);
            investissement.SetActive(true);
        }

        if(currentnourriture >= maxnourriture)
        {
            buttonnourritureobject.SetActive(false);
        }
        else
        {
            buttonnourritureobject.SetActive(true);
        }

        if(currentargent >= maxargent)
        {
            buttonargentobject.SetActive(false);
        }
        else
        {
            buttonargentobject.SetActive(true);
        }

    }

    

    public void fourverified(){
        

        if(currentnourriture > maxnourriture * 4f/5f && currentargent > maxargent * 4f/5f)
    {
        Destroy(four4);
        four5.SetActive(true);
        PlayerPrefs.SetString("four", "four5");
    }
    else if(currentnourriture > maxnourriture * 3f/5f && currentargent > maxargent * 3f/5f)
    {
        
        Destroy(four3);
        four4.SetActive(true);
        PlayerPrefs.SetString("four", "four4");
    }
    else if(currentnourriture > maxnourriture * 2f/5f && currentargent > maxargent * 2f/5f)
    {
        
        Destroy(four2);
        four3.SetActive(true);
        PlayerPrefs.SetString("four", "four3");
    }
    else if(currentnourriture > maxnourriture * 1f/5f && currentargent > maxargent * 1f/5f)
    {
        
        Destroy(four1);
        four2.SetActive(true);
        PlayerPrefs.SetString("four", "four2");
    }
    else{
        PlayerPrefs.SetString("four", "four1");
    }
        
    }

     public void AjoutNourriture()
    {
        currentnourriture += prixnourriture;
        if(currentnourriture>maxnourriture)
        {
            currentnourriture = maxnourriture;
        }
        barrenourriture.SetBarrenourriture(currentnourriture);

        Inventaire.instance.RemoveNourriture(prixnourriture);
        fourverified(); 
    }

     public void Ajoutargent()
    {
        currentargent += prixargent;
        if(currentargent>maxargent)
        {
            currentargent = maxargent;
        }
        barreargent.Setbarreargent(currentargent);

        Inventaire.instance.RemoveArgent(prixargent);
        fourverified();
    }

}
