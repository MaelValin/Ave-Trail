using UnityEngine;
using UnityEngine.UI;

public class Inventaire : MonoBehaviour
{
    public float NourritureQuantity= 500;
    public float ArgentQuantity= 500;
    public float BalleQuantity= 500;

    public Text NourritureText;
    public Text ArgentText;
    public Text BalleText;

    public static Inventaire instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventaire dans la scène");
            return;
        }
        else
        {
            instance = this;
        }
    }


    void Start()
    {
        // Initialisation des quantités d'inventaire
        NourritureText.text =  NourritureQuantity.ToString();
        ArgentText.text =  ArgentQuantity.ToString();
        BalleText.text =  BalleQuantity.ToString();
    }   

    void Update()
    {
         NourritureText.text =  NourritureQuantity.ToString();
        ArgentText.text =  ArgentQuantity.ToString();
        BalleText.text =  BalleQuantity.ToString();
    }


    public void AddNourriture(float amount)
    {
        NourritureQuantity += amount;
    }

    public void AddArgent(float amount)
    {
        ArgentQuantity += amount;
    }

    public void AddBalle(float amount)
    {
        BalleQuantity += amount;
    }

    public void RemoveNourriture(float amount)
    {
        NourritureQuantity -= amount;
        if (NourritureQuantity < 0) NourritureQuantity = 0; // Empêche les quantités négatives
    }

    public void RemoveArgent(float amount)
    {
        ArgentQuantity -= amount;
        if (ArgentQuantity < 0) ArgentQuantity = 0; // Empêche les quantités négatives
    }

    public void RemoveBalle(float amount)
    {
        BalleQuantity -= amount;
        if (BalleQuantity < 0) BalleQuantity = 0; // Empêche les quantités négatives
    }
}
