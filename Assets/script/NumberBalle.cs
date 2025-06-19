using UnityEngine;
using UnityEngine.UI;

public class NumberBalle : MonoBehaviour
{
    public static NumberBalle instance; // Ajoute cette ligne

    public int nombreBalle = 10; // Nombre de balles à afficher
    public Text Textvalue; 


 private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de NumberBalle dans la scène");
            return;
        }
        else
        {
            instance = this;
        }

        nombreBalle= PlayerPrefs.GetInt("ballesInventaire", 10);
    }

    
    void Start()
    {
        // Assurez-vous que Textvalue est assigné dans l'inspecteur ou via code
        if (Textvalue == null)
        {
            Textvalue = GetComponent<Text>();
        }
        Textvalue.text = nombreBalle.ToString(); // Affiche le nombre de balles 
    }
    



    public void AjoutBalle(int numberballes){
        nombreBalle += numberballes; // Ajoute une balle
        Textvalue.text = nombreBalle.ToString(); // Met à jour le texte affiché
    }

    public void RetireBalle(){
        nombreBalle -= 1; // Retire une balle
        Textvalue.text = nombreBalle.ToString(); // Met à jour le texte affiché
    }
}
