using UnityEngine;

public class pointeurScript : MonoBehaviour
{

    public SpriteRenderer[] spriteRenderer;
    public GameObject player;
    public GameObject pointeur;
    public GameObject pointeur2;
    public GameObject pointeur3;
    private shootplayer shootScript;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootScript = FindObjectOfType<shootplayer>(); // ou assigne-le via l’inspecteur si besoin
        foreach (SpriteRenderer sr in spriteRenderer)
            sr.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButton(0)&& shootScript != null && shootScript.charger)
        {
            foreach (SpriteRenderer sr in spriteRenderer)
            sr.enabled = true;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(pointeur.transform.position).z;
            Vector3 playerPosition = player.transform.position;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
            mouseWorldPos.z = playerPosition.z;

            pointeur.transform.position = mouseWorldPos;

            // Position à 3/4 entre le joueur et la souris
            Vector3 posTroisQuart = playerPosition + (mouseWorldPos - playerPosition) * 0.625f;
            pointeur2.transform.position = posTroisQuart;

            Vector3 posDeuxQuart = playerPosition + (mouseWorldPos - playerPosition) * 0.25f;
            pointeur3.transform.position = posDeuxQuart;
        }
        else{
            foreach (SpriteRenderer sr in spriteRenderer)
            sr.enabled = false;            
            pointeur.transform.position = player.transform.position; // Set the position of the pointer to the player's position
        }
        
    }
}
