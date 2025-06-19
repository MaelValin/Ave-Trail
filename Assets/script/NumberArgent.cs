using UnityEngine;
using UnityEngine.UI;

public class NumberArgent : MonoBehaviour
{
    public static NumberArgent instance; // Ajoute cette ligne

    public int nombreArgent = 0; // Nombre de Argents à afficher
    public Text Textvalue; // Référence au composant Text pour afficher le nombre de Argents


 private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de NumberArgent dans la scène");
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
        Textvalue.text = nombreArgent.ToString(); // Affiche le nombre de Argents 
    }
    



    public void AjoutArgent(int numberArgent){
        nombreArgent += numberArgent; // Ajoute une Argent
        Textvalue.text = nombreArgent.ToString(); // Met à jour le texte affiché
    }

    public void RetireArgent(){
        nombreArgent -= 1; // Retire une Argent
        Textvalue.text = nombreArgent.ToString(); // Met à jour le texte affiché
    }
}
