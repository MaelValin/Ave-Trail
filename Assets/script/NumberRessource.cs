using UnityEngine;
using UnityEngine.UI;

public class NumberRessources : MonoBehaviour
{
    public static NumberRessources instance; // Ajoute cette ligne

    public int nombreRessources = 0; // Nombre de Ressourcess à afficher
    public Text Textvalue; 


 private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de NumberRessources dans la scène");
            return;
        }
        else
        {
            instance = this;
        }
    }

    
    void Start()
    {
        // Assurez-vous que Textvalue est assigné dans l'inspecteur ou via code
        if (Textvalue == null)
        {
            Textvalue = GetComponent<Text>();
        }
        Textvalue.text = nombreRessources.ToString(); // Affiche le nombre de Ressourcess 
    }
    



    public void AjoutRessources(int numberRessources){
        nombreRessources += numberRessources; // Ajoute une Ressources
        Textvalue.text = nombreRessources.ToString(); // Met à jour le texte affiché
    }

    public void RetireRessources(){
        nombreRessources -= 1; // Retire une Ressources
        Textvalue.text = nombreRessources.ToString(); // Met à jour le texte affiché
    }
}
