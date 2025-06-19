using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class CordeComportement : MonoBehaviour
{

    public float speed;

    

    private Transform target;
    private int destPoint = 0;

    public SpriteRenderer graphic;

    public int damageOncollision= 30;
    public Animator animator;
    public GameObject Corde;

    public BoxCollider2D collider2d;

    

    public static CordeComportement instance;

    //public AudioClip roule;
    //public AudioSource audioSource;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de CordeComportement dans la scène");
            return;
        }
        else
        {
            instance = this;
        }
    }

    private bool isMoving = false;

void Start()
{
   
    isMoving = false;
    
    Corde.SetActive(false);
    collider2d.enabled = false;
    }

void Update()
{
    if (GamePause.GameIsPaused)
        return;

    if (target != null && isMoving)
    {
        Vector3 moveDirection = target.position - transform.position;
        transform.position += moveDirection.normalized * speed * Time.deltaTime;
        /*if (!audioSource.isPlaying)
        {
            audioSource.clip = roule;
            audioSource.loop = true;
            audioSource.Play();
        }*/
       
    }else
    {
        //audioSource.Stop();
    }
}

    public void CordeArrived()
    {
        StartCoroutine(WaitForCorde());
    }

    IEnumerator WaitForCorde()
    {
        
        Corde.SetActive(true);
    animator.SetTrigger("arrive");
    yield return new WaitForSeconds(2f);
    collider2d.enabled = true;
    }

    public void CordeGo()
    {
        
        animator.SetTrigger("go");
    
    }

   

  
}
