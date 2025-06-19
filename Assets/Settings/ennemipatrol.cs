using UnityEngine;

public class ennemipatrol : MonoBehaviour
{

    public float speed;

    public Transform[] waypoints;

    private Transform target;
    private int destPoint = 0;

    public SpriteRenderer graphic;

    public int damageOncollision= 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //si l'enemi est proche de la position, il fait demi tour
        if(Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            if (target.position.x > transform.position.x)
            {
                graphic.flipX = false;
            }
            else
            {
                graphic.flipX = true;
            }
             
            
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
           PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
           playerHealth.TakeDamage(damageOncollision);
        } 
    }

  
}
