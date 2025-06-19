using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ennemieComportement_normal : MonoBehaviour
{

    public float movespeed;

    public int damageOncollision;
    public int life= 10;

    public GameObject player;


    public Rigidbody2D rb;

    public bool attackValid = true;

    public Animator animators;

    public SpriteRenderer spriteRenderer;

    public CapsuleCollider2D capsuleCollider;

    public AudioClip zombieattack;
    public AudioClip zombiedeath;
    public AudioSource audioSource;


    

    string typeBalle;

    public static ennemieComportement_normal instance; // Instance statique de la classe pour l'accès global
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ennemieComportement_normal dans la scène");
            return;
        }
        instance = this; // Assigne l'instance statique à cette instance
    }
    

    void start() {
        typeBalle = shootplayer.instance.typeBalle; // Type de balle, récupéré depuis l'instance de shootplayer
        
    }




    
    private Vector3 velocity = Vector3.zero;
    
    // Update is called once per frame
    void Update()
    {

    if (rb.bodyType == RigidbodyType2D.Dynamic)
    {
        Vector3 dir = player.transform.position - transform.position;
        dir.z = 0;
        Vector3 targetvelocity = dir.normalized * movespeed;
        rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetvelocity, ref velocity, 0.05f);    

    // Gestion du flip et des animations
if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
{
    // Mouvement horizontal dominant
    animators.SetBool("up", false);
    animators.SetBool("down", false);
    animators.SetBool("right", true);

    if (dir.x > 0)
    {
        // Droite
        spriteRenderer.flipX = false;
    }
    else
    {
        // Gauche
        spriteRenderer.flipX = true;
    }
}
else
{
    // Mouvement vertical dominant
    animators.SetBool("right", false);

    if (dir.y > 0)
    {
        // Haut
        animators.SetBool("up", true);
        animators.SetBool("down", false);
    }
    else
    {
        // Bas
        animators.SetBool("up", false);
        animators.SetBool("down", true);
    }
}

     

     }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        
        if(collision.transform.CompareTag("Player") && attackValid==true)
        {
           StartCoroutine(Attack(damageOncollision, collision)); 
        } 

        if(collision.transform.CompareTag("balle"))
        {
                tirplayer bulletScript = collision.transform.GetComponent<tirplayer>();

            if (bulletScript != null)
                {
                    life -= bulletScript.degat;
                    Debug.Log("Life after bullet hit: " + life);
                }
                if (life <= 0)
                {
                    ManagerScene.instance.AddKill();
                    StartCoroutine(death());
                }

                
            
        }

        

        

        if(collision.transform.CompareTag("car"))
        {
            CarComportement carScript = collision.transform.GetComponent<CarComportement>();
            if (carScript != null)
                {
                    life -= carScript.damageOncollision;
                }
                if (life <= 0)
                {
                    StartCoroutine(death());
                }
            
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
{
    if (collision.transform.CompareTag("Player") && attackValid)
    {
        StartCoroutine(Attack(damageOncollision, collision));
    }
}

private void OnTriggerStay2D(Collider2D collision)
{
    if (collision.CompareTag("balleexplosion")&&life>0)
    {
        
        life -= 15;
        if (life <= 0)
        {
            StartCoroutine(death());
        }
    }
    if(collision.CompareTag("rayonfeu")&&life>0)
    {
        
        life -= 5;
        if (life <= 0)
        {
            StartCoroutine(death());
        }
    }
}
    

    private IEnumerator Attack(int damageOncollision, Collision2D collision)
    {
        
                //animators.SetTrigger("attack");
            audioSource.PlayOneShot(zombieattack);
           PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
           playerHealth.TakeDamage(damageOncollision);

           attackValid = false;
        yield return new WaitForSeconds(2f);
        attackValid = true;
        
        
    }


    

    
    public IEnumerator death()
    {
        audioSource.Stop();
       yield return new WaitForSeconds(0.1f);
        audioSource.PlayOneShot(zombiedeath);
       animators.SetTrigger("death");
       rb.bodyType = RigidbodyType2D.Static;
    capsuleCollider.isTrigger = true;
    spriteRenderer.color = new Color(1f, 159f/255f, 135f/255f);
        
    yield return new WaitForSeconds(0.5f);
    float randomAngle = Random.Range(0f, 360f);
    rb.transform.Rotate(0, 0, randomAngle);


    yield return new WaitForSeconds(2f); // Attendre la durée de l'animation de mort
        spriteRenderer.sortingOrder = -1;

        yield return new WaitForSeconds(15f);
        Destroy(gameObject); // Détruire l'objet après la mort


        
    }

    

}
