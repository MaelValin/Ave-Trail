using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed;
    

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;


    private Vector3 velocity = Vector3.zero;

    private float Horizontalmouvement;
    private float Verticalmouvement;

    private Vector2 screenBounds;
    
    public static PlayerMovement instance;

    public CapsuleCollider2D playercollider;
    public bool collisionDetection = false;
    
    public Sprite spriteDroite;
    public Sprite spriteGauche;
    public Sprite spriteHaut;
    public Sprite spriteBas;

    public Sprite spriteHautDroite;
    public Sprite spriteHautGauche;
    public Sprite spriteBasDroite;
    public Sprite spriteBasGauche;
    public bool carcollider = false;

    public bool click = false;
    

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovement dans la scène");
            return;
        }
        else
        {
            instance = this;
        }
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
    }

private void Start()
    {
        // Calculer les limites de l'écran en coordonnées du monde
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.transform.position.z));
    }

    void FixedUpdate()
    {
        MovePlayer(Horizontalmouvement, Verticalmouvement);
       
    }

    void Update()
    {
        if(carcollider == true && Input.GetKey(KeyCode.E))
        {
            StartCoroutine(winner());
        }
        

        if (!shootplayer.instance.peutBouger)
    return; // ou return avant tout déplacement

// Bloque le mouvement
        Horizontalmouvement = 0;
        Verticalmouvement = 0;
    if (Input.GetMouseButton(0))
        {
        animator.enabled = false;
        click = true;
        Horizontalmouvement = 0;
        Verticalmouvement = 0;
        // Calcul de la direction souris
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPos - transform.position;

        // Choix du sprite/direction selon l'angle
        // Détermination de la direction pour choisir le sprite approprié
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle >= -22.5f && angle < 22.5f)
        {
            
            spriteRenderer.sprite = spriteDroite;
        }
        else if (angle >= 22.5f && angle < 67.5f)
        {
            // Haut droite
            spriteRenderer.sprite = spriteHautDroite;
        }
        else if (angle >= 67.5f && angle < 112.5f)
        {
            // Haut
            spriteRenderer.sprite = spriteHaut;
        }
        else if (angle >= 112.5f && angle < 157.5f)
        {
            // Haut gauche
            spriteRenderer.sprite = spriteHautGauche;
        }
        else if (angle >= 157.5f || angle < -157.5f)
        {
            
            spriteRenderer.sprite = spriteGauche;
        }
        else if (angle >= -157.5f && angle < -112.5f)
        {
            // Bas gauche
            spriteRenderer.sprite = spriteBasGauche;
        }
        else if (angle >= -112.5f && angle < -67.5f)
        {
            // Bas
            spriteRenderer.sprite = spriteBas;
        }
        else if (angle >= -67.5f && angle < -22.5f)
        {
            // Bas droite
            spriteRenderer.sprite = spriteBasDroite;
        }

        if(PlayerHealth.instance.currenthealth<=0){
        click = false;
        animator.enabled = true;
        return;
    }



            
           
        }else{

            animator.enabled = true;
            click = false;
        

        if (Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.D))
        {
            
            Horizontalmouvement = movespeed;
            animator.SetBool("right", true);
            animator.SetBool("left", false);
        }
        else if (Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A))
        {
            Horizontalmouvement = -movespeed;
            animator.SetBool("right", false);
            animator.SetBool("left", true);
        }
        else
        {
            animator.SetBool("right", false);
            animator.SetBool("left", false);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            Verticalmouvement = movespeed;
            animator.SetBool("up", true);
            animator.SetBool("down", false);
        }
        else if (Input.GetKey(KeyCode.DownArrow)|| Input.GetKey(KeyCode.S))
        {
            Verticalmouvement = -movespeed;
            animator.SetBool("up", false);
            animator.SetBool("down", true);
        }
        else
        {
            animator.SetBool("up", false);
            animator.SetBool("down", false);
        }

        }

        

        
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionDetection = true;

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Carcollider"))
        {
            carcollider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Carcollider"))
        {
            carcollider = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionDetection = false;
    }

    void MovePlayer(float _Horizontalmouvement, float _Verticalmouvement)
    {
        
             Vector3 targetvelocity = new Vector2(0, Verticalmouvement);
            rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetvelocity, ref velocity, .05f);

            Vector3 targetvelocity2 = new Vector2(Horizontalmouvement, 0);
            rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetvelocity2, ref velocity, .05f);

         Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBounds.x, screenBounds.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenBounds.y, screenBounds.y);
        transform.position = clampedPosition;
    }


    


IEnumerator winner()
{
    playercollider.enabled = false;
    spriteRenderer.enabled = false;
    ManagerScene.instance.textInteract.gameObject.SetActive(false);
    yield return new WaitForSeconds(1f);
    if(ModManager.instance.Mod == "difficile"){
    CordeComportement.instance.CordeGo();
    }
    else{
    CarComportement.instance.CarGo();
    }
    
    
    yield return new WaitForSeconds(5f);
    GameWinnerManager.instance.OnPlayerWin();
}
   
}
