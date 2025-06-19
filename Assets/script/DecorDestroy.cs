using UnityEngine;
using System.Collections;

public class DecorDestroy : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer spriteRenderer2;
    public Sprite detruit;
    public BoxCollider2D boxCollider;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("car"))
        {

            spriteRenderer.sprite = detruit;
            spriteRenderer2.enabled=false;
            boxCollider.enabled = false;
            
            
        } 
    }
}
