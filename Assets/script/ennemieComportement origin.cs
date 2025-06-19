using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ennemieComportement : MonoBehaviour
{

    public float movespeed;

    public int damageOncollision;
    public int life= 10;

    public GameObject player;


    public Rigidbody2D rb;

    public bool attackValid = true;

    public Animator animators;

    public SpriteRenderer spriteRenderer;

    public CircleCollider2D circleCollider ;

    




    
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
    

    private IEnumerator Attack(int damageOncollision, Collision2D collision)
    {
        
                animators.SetTrigger("attack");
            
           PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
           playerHealth.TakeDamage(damageOncollision);

           attackValid = false;
        yield return new WaitForSeconds(2f);
        attackValid = true;
        
        
    }


    

    
    private IEnumerator death()
    {
        
       animators.SetTrigger("death");
        
    

    rb.bodyType = RigidbodyType2D.Static;
    circleCollider.isTrigger = true;
    spriteRenderer.color = new Color(1f, 0.772f, 0.239f);

    yield return new WaitForSeconds(3f); // Attendre la durée de l'animation de mort
        spriteRenderer.sortingOrder = -1;
        
    }

    

}
