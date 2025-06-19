using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class shootcar : MonoBehaviour
{
    public GameObject bulletPrefab; // Le prefab de la balle
    public GameObject car; // Le joueur
    public float tirSpeed = 15f; // Vitesse du tir

    public float currentreload; // La valeur actuelle de la barre de rechargement
    public float maxreload = 100; // La valeur maximale de la barre de rechargement

    public float reloadTime = 0.1f; // Temps total de rechargement en secondes
    public bool charger = true; // Booléen pour savoir si le joueur peut tirer
    public int degat = 10; // Dégâts infligés par la balle
    public AudioSource audioSource; // Source audio pour les effets sonores
    public AudioClip tirSound; // Son de tir


    



    public static shootcar instance; // Instance statique de la classe pour l'accès global
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de shootcar dans la scène");
            return;
        }
        instance = this; // Assigne l'instance statique à cette instance
    }

    void Start()
    {
        currentreload = maxreload; // Initialise la barre de rechargement
    }

    // Update is called once per frame
    void Update()
    {
    

    

        if(currentreload==0)
        {
            StartCoroutine(reloadbarcharge());
            charger = false;
            
        }

        if(currentreload == maxreload)
        {
            charger = true;
        }
        
    }

    // Coroutine pour recharger la barre de tir
    private IEnumerator reloadbarcharge()
    {
         
        float elapsed = 0f;
        charger = false;
        while (elapsed < reloadTime)
        {
            elapsed += Time.deltaTime;
            currentreload = Mathf.Lerp(0, maxreload, elapsed / reloadTime);
            yield return null;
        }
        currentreload = maxreload;
        
        
    }


    public void shoot()
    {
        if(charger){
            
            // Récupère tous les ennemis dans la scène (tag "enemie")
            GameObject[] ennemis = GameObject.FindGameObjectsWithTag("enemie");
            if (ennemis.Length == 0)
                return; // Aucun ennemi, ne rien faire

            // Choisit un ennemi au hasard
            GameObject cible = ennemis[Random.Range(0, ennemis.Length)];

            Vector2 playerPosition = car.transform.position; // ou player.transform.position selon ton script
            Vector2 ciblePosition = cible.transform.position;

            // Calcule la direction vers l'ennemi choisi
            Vector2 direction = (ciblePosition - playerPosition).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            audioSource.PlayOneShot(tirSound);

            // Crée la balle et la configure
            GameObject bullet = Instantiate(bulletPrefab, playerPosition, Quaternion.Euler(0, 0, angle));
            bullet.GetComponent<tirplayer>().degat = degat;
            currentreload=0;
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), car.GetComponent<Collider2D>());
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * tirSpeed, ForceMode2D.Impulse);
            Destroy(bullet, 2f); // Détruit la balle après 2 secondes
            
        }  

    }
}
    

 

    
    

