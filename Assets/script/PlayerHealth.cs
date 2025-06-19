using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerHealth : MonoBehaviour
{

    public int maxhealth=100;
    public int currenthealth;

    public healtbar healthbar;

    public bool isInvisible = false;

    private ChangementVisage changementVisage;

    public AudioClip blessure1;
    
    public AudioSource audioSource;
    


 public static PlayerHealth instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }
        else
        {
            instance = this;
        }
        if(PlayerPrefs.GetString("local_amelioration") == "0"){
            PlayerHealth.instance.maxhealth = 100;
        }
        else if(PlayerPrefs.GetString("local_amelioration") == "1"){
            PlayerHealth.instance.maxhealth = 150;
        }
        else if(PlayerPrefs.GetString("local_amelioration") == "2"){
            PlayerHealth.instance.maxhealth = 250;
        }
        else if(PlayerPrefs.GetString("local_amelioration") == "3"){
            PlayerHealth.instance.maxhealth = 400;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currenthealth = maxhealth;
        healthbar.SetMaxHealth(maxhealth);
        
    }

    // Update is called once per frame
    void Update()
    {

        //prendre des degats avec la touche H
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }

        
    }

    public void Heal(int healAmount)
    {
        currenthealth += healAmount;
        if(currenthealth>maxhealth)
        {
            currenthealth = maxhealth;
        }
        healthbar.SetHealth(currenthealth);

        ChangementVisage.instance.Changementvisage();
    }


 
    public void TakeDamage(int damage)
    {
        if(!isInvisible)
        {
           currenthealth -= damage;
        healthbar.SetHealth(currenthealth);
        ChangementVisage.instance.Changementvisage();

    //verif live
    if(currenthealth<=0){
        Die();
        return;
    }
    
            audioSource.PlayOneShot(blessure1);
        


        
    
        }
        
    }

    public void Die()
    {
        
        //game over
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        playerMovement.enabled = false;
        playerMovement.rb.linearVelocity = Vector2.zero;
        playerMovement.rb.bodyType = RigidbodyType2D.Kinematic;
        playerMovement.playercollider.enabled = false;
        GameObject[] ennemis = GameObject.FindGameObjectsWithTag("enemie");
    foreach (GameObject ennemi in ennemis)
    {
        Destroy(ennemi);
    }
    if (ennemieSpawnner.instance != null)
        {
            ennemieSpawnner.instance.CancelInvoke(nameof(ennemieSpawnner.SpawnEnemy));
        }

        if (Itemspawner.instance != null)
        {
            Itemspawner.instance.CancelInvoke(nameof(Itemspawner.SpawnEnemy));
        }

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("death");
        playerMovement.spriteRenderer.sortingOrder = 0;        
        GameOverManager.instance.OnPlayerDeath();
        
        

    }

    public void Respawn()
    {
        
        currenthealth = maxhealth;
        healthbar.SetHealth(currenthealth);
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        
        playerMovement.enabled = true;
        playerMovement.rb.bodyType = RigidbodyType2D.Dynamic;
        playerMovement.playercollider.enabled = true;
        
    }

    

    

    
}

