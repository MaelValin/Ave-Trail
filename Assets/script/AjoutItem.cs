using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AjoutItem : MonoBehaviour
{

    public int valueItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player")&& (CompareTag("balle legere")||CompareTag("balle lourde")))
        {
            NumberBalle numberBalle = FindObjectOfType<NumberBalle>();
        if (numberBalle != null)
        {
            numberBalle.AjoutBalle(valueItem); // Retire une balle du nombre de balles
        }
        else
        {
            Debug.LogWarning("Aucune instance de NumberBalle trouvée dans la scène.");
    }
            Destroy(gameObject);
            Itemspawner itemSpawner = FindObjectOfType<Itemspawner>();
          itemSpawner.removeItem();
        }


        if (collision.CompareTag("Player")&& CompareTag("ressource legere"))
        {
            NumberRessources numberRessources = FindObjectOfType<NumberRessources>();
        if (numberRessources != null)
        {
            numberRessources.AjoutRessources(valueItem); // Retire une Ressource du nombre de Ressources
        }
        else
        {
            Debug.LogWarning("Aucune instance de NumberRessource trouvée dans la scène.");
    }
            Destroy(gameObject);
            Itemspawner itemSpawner = FindObjectOfType<Itemspawner>();
          itemSpawner.removeItem();
        }


        if (collision.CompareTag("Player")&& CompareTag("argent"))
        {
            NumberArgent numberArgent = FindObjectOfType<NumberArgent>();
        if (numberArgent != null)
        {
            numberArgent.AjoutArgent(valueItem); // Retire une Ressource du nombre de Argent
        }
        else
        {
            Debug.LogWarning("Aucune instance de NumberRessource trouvée dans la scène.");
    }
            Destroy(gameObject);
            Itemspawner itemSpawner = FindObjectOfType<Itemspawner>();
          itemSpawner.removeItem();
        }

    if (collision.CompareTag("decor"))
    {
        // Décale l’item de 2 unités dans une direction aléatoire autour du décor
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 offset = new Vector3(randomDirection.x, randomDirection.y, 0) * 2f;
        transform.position = collision.transform.position + offset;
    }

    }





    
}

