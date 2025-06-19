using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class tirplayer : MonoBehaviour
{
    public float tirSpeed = 10f; // Vitesse du tir
    public float tirLifeTime = 2f; // Durée de vie du tir
    public GameObject tirPrefab; // Préfabriqué du tir
    public BoxCollider2D BoxCollider;
    public int degat = 10; // Dégâts infligés par la balle
    public int richochetCount = 0; // Nombre de ricochets
    string typeBalle;
    public bool isPlayerBullet = false;

    

    public GameObject balleexplosionPrefab; // Préfabriqué de l'explosion de la balle




    void Start()
    {
        StartCoroutine(Despawn());
        typeBalle = shootplayer.instance.typeBalle; // Type de balle, récupéré depuis l'instance de shootplayer
    }
    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    if(isPlayerBullet){

         if(typeBalle== "balle")
        {
            if(collision.transform.CompareTag("enemie"))
                {
                    Destroy(gameObject);
                }

                if(collision.transform.CompareTag("decor"))
                {
                    Destroy(gameObject);
                }
        }

        if(typeBalle == "explosif" )
        {
            if(collision.transform.CompareTag("enemie") || collision.transform.CompareTag("decor"))
            {
               
                    GameObject zone = Instantiate(balleexplosionPrefab, transform.position, Quaternion.identity);
                        

                    Destroy(zone, 0.2f);
                    Destroy(gameObject);

                
            }
        }



        if(typeBalle == "ricochet" )
        {
            
            if(collision.transform.CompareTag("enemie"))
            {
                

                richochetCount++;
                
            }

            if(collision.transform.CompareTag("decor"))
            {
                richochetCount++;
                
            }

            if(richochetCount >= 3)
            {
                Destroy(gameObject);
                
            }
            else
            {
                // Logique de ricochet, par exemple, changer la direction du tir
                Vector2 ricochetDirection = Vector2.Reflect(transform.up, collision.contacts[0].normal);
                transform.up = ricochetDirection;
                GetComponent<Rigidbody2D>().linearVelocity = ricochetDirection * (tirSpeed-2);
                
            }

            
        }


        if(typeBalle== "lanceflam" )
        {
            StartCoroutine(DespawnFlame());
        }

        // if(collision.transform.CompareTag("Player"))
        // {
        //     BoxCollider.isTrigger = false;
        // }
        // else
        // {
        //     BoxCollider.isTrigger = true;
        // }
    }
    else{
         
            if(collision.transform.CompareTag("enemie"))
                {
                    Destroy(gameObject);
                }

                if(collision.transform.CompareTag("decor"))
                {
                    Destroy(gameObject);
                }
        
    }

    }


    IEnumerator Despawn(){
        yield return new WaitForSeconds(tirLifeTime);
        Destroy(gameObject);
    }

     IEnumerator DespawnFlame(){
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

   

}
