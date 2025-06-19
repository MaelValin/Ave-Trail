using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class shootplayer : MonoBehaviour
{
    public GameObject bulletPrefab; // Le prefab de la balle
    public GameObject player; // Le joueur
    public float tirSpeed = 10f; // Vitesse du tir

    public float currentreload; // La valeur actuelle de la barre de rechargement
    public float maxreload = 100; // La valeur maximale de la barre de rechargement

    public reloadbar reloadbar; // La barre de rechargement
    public float reloadTime = 2f; // Temps total de rechargement en secondes
    public bool charger = true; // Booléen pour savoir si le joueur peut tirer
    public int degat = 10; // Dégâts infligés par la balle
    public AudioSource audioSource; // Source audio pour les effets sonores
    public AudioClip tirSound; // Son de tir
    public AudioClip lanceflamSound; // Son du lance-flammes
    


    public GameObject rayonPrefab; // Le prefab du rayon
    public Transform firePoint;    // Point de départ du rayon (devant le joueur)
    public float dureeRayon = 5f;
    public int degatsParSeconde = 10;

    private GameObject rayonActif;
    private bool lanceflamActif = false;

    public string typeBalle= "balle";

    public float longueurRayon = 5f; // à ajuster selon la taille de ton prefab
    public bool peutBouger = true;



    public static shootplayer instance; // Instance statique de la classe pour l'accès global
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de shootplayer dans la scène");
            return;
        }
        instance = this; // Assigne l'instance statique à cette instance
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
    }

    void Start()
    {
        currentreload = maxreload; // Initialise la barre de rechargement
        reloadbar.SetMaxReload((int)maxreload);
    }

    // Update is called once per frame
    void Update()
    {
    if(typeBalle == "lanceflam")
    {NumberBalle numberBalle = FindObjectOfType<NumberBalle>();
        if (Input.GetMouseButtonUp(0) && !lanceflamActif && charger == true && numberBalle.nombreBalle>0 && ManagerScene.instance.PartyIsPlay)
        {
            StartCoroutine(TirerLanceflammes());
            
        }

        // Suivi du rayon pendant qu'il est actif
        if (lanceflamActif && rayonActif != null)
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            mouseScreenPosition.z = Camera.main.nearClipPlane; // ou la distance entre la caméra et le plan du joueur, ex. 10f si nécessaire
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0f; // force le plan 2D

            Vector2 direction =  firePoint.position.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rayonActif.transform.position = firePoint.position + (Vector3)(direction * 1f); // 1f si ton rayon fait 2 unités
            rayonActif.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    else
    {
    NumberBalle numberBalle = FindObjectOfType<NumberBalle>();
        
        if (Input.GetMouseButtonUp(0)&& charger == true && numberBalle.nombreBalle>0 && ManagerScene.instance.PartyIsPlay)
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            Vector2 playerPosition = player.transform.position;
            shoot((Vector2)mouseWorldPosition, playerPosition); // Appelle la méthode de tir lorsque le clic gauche de la souris est pressé
            if (numberBalle != null)
    {
        currentreload = 0;
    reloadbar.SetReload((int)currentreload);
        numberBalle.RetireBalle();
    }
    else
    {
        Debug.LogWarning("Aucune instance de NumberBalle trouvée dans la scène.");
    }
            
        }

    }

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
         NumberBalle numberBalle = FindObjectOfType<NumberBalle>();

        if(numberBalle.nombreBalle>0)
        {
        float elapsed = 0f;
        charger = false;
        while (elapsed < reloadTime)
        {
            elapsed += Time.deltaTime;
            currentreload = Mathf.Lerp(0, maxreload, elapsed / reloadTime);
            reloadbar.SetReload((int)currentreload);
            yield return null;
        }
        currentreload = maxreload;
        reloadbar.SetReload((int)currentreload);
        }else{
            reloadbar.SetReload(0);
            charger = false;
        }
        
    }


    private void shoot(Vector2 mousePosition, Vector2 playerPosition)
    {   
        
        NumberBalle numberBalle = FindObjectOfType<NumberBalle>();
        // Calcule la direction du tir
        Vector2 direction = (mousePosition - playerPosition).normalized;
        //tourne la balle dans la direction du tir
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        audioSource.PlayOneShot(tirSound); // Joue le son de tir
        
        
        

        // Crée une nouvelle balle à la position du joueur
        GameObject bullet = Instantiate(bulletPrefab, playerPosition, Quaternion.Euler(new Vector3(0, 0, angle)));
        bullet.GetComponent<tirplayer>().degat = degat;
        bullet.GetComponent<tirplayer>().isPlayerBullet = true;
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
        // Ajoute une force à la balle pour la faire avancer
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * tirSpeed, ForceMode2D.Impulse);
        

        
        
        
    }
    

 void ActiverRayon()
    {
        lanceflamActif = true;
                


        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        Vector2 direction = (mouseWorldPosition - firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        rayonActif = Instantiate(rayonPrefab, firePoint.position + (Vector3)(direction * 6f), Quaternion.Euler(0, 0, angle));
    }

    void DesactiverRayon()
    {
        lanceflamActif = false;
        if (rayonActif != null)
            Destroy(rayonActif);
    }


   private IEnumerator TirerLanceflammes()
{
    lanceflamActif = true;
    peutBouger = false;
    NumberBalle numberBalle = FindObjectOfType<NumberBalle>();
    audioSource.PlayOneShot(lanceflamSound); // Joue le son de tir
    float timer = 0f;
    while (timer < 5f)
{
    Vector3 mouseScreenPosition = Input.mousePosition;
    Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    mouseWorldPosition.z = 0f;
    Vector2 direction = (mouseWorldPosition - firePoint.position).normalized;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    float distance = Vector2.Distance(firePoint.position, mouseWorldPosition);

    // Force une distance minimale de 5f
    float longueur = Mathf.Max(distance-20f, 15f);

    if (rayonActif == null)
    {
        rayonActif = Instantiate(rayonPrefab, firePoint.position, Quaternion.identity);
    }

     // Place le rayon sur le joueur
        rayonActif.transform.position = firePoint.position;
        // Oriente le rayon vers la souris
        rayonActif.transform.rotation = Quaternion.Euler(0, 0, angle);
        // Adapte la taille du rayon (supposons que l'échelle X contrôle la longueur)
        Vector3 scale = rayonActif.transform.localScale;
        scale.x = longueur*0.3f; // ou scale.y selon l'axe de ton prefab
        rayonActif.transform.localScale = scale;


    timer += Time.deltaTime;
    yield return null;
}

    

    if (rayonActif != null)
        Destroy(rayonActif);

    lanceflamActif = false;
    peutBouger = true;
    if (numberBalle != null)
    {
        currentreload = 0;
    reloadbar.SetReload((int)currentreload);
        numberBalle.RetireBalle();
    }
    else
    {
        Debug.LogWarning("Aucune instance de NumberBalle trouvée dans la scène.");
    }
}
}
    
    

