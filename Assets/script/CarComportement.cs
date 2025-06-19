using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.UI;

public class CarComportement : MonoBehaviour
{

    public float speed;

    public Transform[] waypoints;

    private Transform target;
    private int destPoint = 0;

    public SpriteRenderer graphic;

    public int damageOncollision= 30;
    public Animator animator;
    public GameObject car;

    

    public static CarComportement instance;

    public AudioClip roule;
    public AudioSource audioSource;

    public bool Carassitance = false; // Pour savoir si la voiture est assistée
    public bool carshoot = false; // Pour savoir si la voiture peut tirer

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de CarComportement dans la scène");
            return;
        }
        else
        {
            instance = this;
        }
        if (PlayerPrefs.GetString("garage_amelioration") == "1")
    Carassitance = true;
else
    Carassitance = false;
    }

    private bool isMoving = false;

void Start()
{
    target = waypoints[0];
    isMoving = false;
    animator.SetBool("roule", true);
    car.SetActive(false);
    }

void Update()
{
    if (GamePause.GameIsPaused)
        return;

    if (target != null && isMoving)
    {
        Vector3 moveDirection = target.position - transform.position;
        transform.position += moveDirection.normalized * speed * Time.deltaTime;
        if (!audioSource.isPlaying)
        {
            audioSource.clip = roule;
            audioSource.loop = true;
            audioSource.Play();
        }
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            // Arrêt au waypoint cible

            isMoving = false;
            animator.SetBool("roule", false);
            audioSource.Stop();
        
        }
    }else
    {
        audioSource.Stop();
    }

    if(Carassitance && carshoot)
    {
        shootcar.instance.shoot();

    }
}




public void CarArrived()
{
    if (waypoints.Length > 1)
    {
        destPoint = 1;
        target = waypoints[destPoint];
        isMoving = true;
        animator.SetBool("roule", true);
        carshoot = true; // La voiture peut tirer
        
    }
}

public void CarGo()
{
    if (waypoints.Length > 2)
    {
        destPoint = 2;
        target = waypoints[destPoint];
        isMoving = true;
        animator.SetBool("roule", true);
        carshoot = false;
        
    }
}

   

  
}
