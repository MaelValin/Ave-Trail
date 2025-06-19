using UnityEngine;

public class regagnelive : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();

        if (collision.CompareTag("Player") && playerHealth.currenthealth < playerHealth.maxhealth)
        {
           playerHealth.Heal(25);
          Itemspawner itemSpawner = FindObjectOfType<Itemspawner>();
          itemSpawner.removeItem();
            Destroy(gameObject);
        }
    }
}



    
   

