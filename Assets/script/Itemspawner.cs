
using UnityEngine;

public class Itemspawner : MonoBehaviour
{

   


    public static Itemspawner instance;

    public GameObject heart;//5
    public GameObject ballelegere;//20
    public GameObject ballelourde;//11.7
    public GameObject ressourcelegere;//20
    public GameObject ressourceLourde;//1.7
    public GameObject argentleger;//20
    public GameObject argentlourd;//11.7

    public int nemberspawn=0; // Nombre d'ennemis à générer
    public int itemMaxspawn = 10; // Nombre maximum d'ennemis à générer
    public float spawnInterval ; // Intervalle entre chaque spawn
    private Vector2 screenBounds;


     private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Itemspawner dans la scène");
            return;
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        // Calculer les limites de l'écran en coordonnées du monde
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.transform.position.z));

        // Lancer le spawn des ennemis à intervalles réguliers
        if(ModManager.instance.Mod == "facile")
        {
            spawnInterval = 5.0f; // Intervalle de spawn pour le mode facile
            itemMaxspawn = 10; // Nombre maximum d'items à générer en mode facile
        }
        else if(ModManager.instance.Mod == "moyen")
        {
            spawnInterval = 4f; // Intervalle de spawn pour le mode moyen
            itemMaxspawn = 15; // Nombre maximum d'items à générer en mode moyen
        }
        else if(ModManager.instance.Mod == "difficile")
        {
            spawnInterval = 3f; // Intervalle de spawn pour le mode difficile
            itemMaxspawn = 20; // Nombre maximum d'items à générer en mode difficile
        }
        
            InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
        
        
        
    }



    public void UpdateSpawnItemsInterval(float newInterval)
{
    spawnInterval = newInterval;
    CancelInvoke(nameof(SpawnEnemy));
    InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
}

    public void SpawnEnemy()
    {
        if(nemberspawn < itemMaxspawn)
        {
        // Générer une position aléatoire en dehors de l'écran
        Vector3 spawnPosition = GetRandomPositionOutsideScreen();

        float rand = Random.value;
        GameObject prefabToSpawn = ballelegere;

        if(ModManager.instance.Mod == "facile")
        {
                // Probabilités cumulées (exemple, à ajuster selon tes besoins)
            if (rand < 0.35f) { // 35%
                prefabToSpawn = ballelegere;
            }
            else if (rand < 0.60f) { // +25% = 60%
                prefabToSpawn = ressourcelegere;
            }
            else if (rand < 0.80f) { // +20% = 80%
                prefabToSpawn = argentleger;
            }
            else if (rand < 0.88f) { // +8% = 88%
                prefabToSpawn = ballelourde;
            }
            else if (rand < 0.94f) { // +6% = 94%
                prefabToSpawn = ressourceLourde;
            }
            else if (rand < 0.98f) { // +4% = 98%
                prefabToSpawn = argentlourd;
            }
            else { // 2%
                prefabToSpawn = heart;
            }
        }
        else if(ModManager.instance.Mod == "moyen")
        {
            if (rand < 0.30f) { // 30%
                prefabToSpawn = ballelegere;
            }
            else if (rand < 0.50f) { // +20% = 50%
                prefabToSpawn = ressourcelegere;
            }
            else if (rand < 0.65f) { // +15% = 65%
                prefabToSpawn = argentleger;
            }
            else if (rand < 0.78f) { // +13% = 78%
                prefabToSpawn = ballelourde;
            }
            else if (rand < 0.88f) { // +10% = 88%
                prefabToSpawn = ressourceLourde;
            }
            else if (rand < 0.96f) { // +8% = 96%
                prefabToSpawn = argentlourd;
            }
            else { // 4%
                prefabToSpawn = heart;
            }
        }
        else if(ModManager.instance.Mod == "difficile")
        {
            if (rand < 0.25f) { // 25%
                prefabToSpawn = ballelegere;
            }
            else if (rand < 0.40f) { // +15% = 40%
                prefabToSpawn = ressourcelegere;
            }
            else if (rand < 0.50f) { // +10% = 50%
                prefabToSpawn = argentleger;
            }
            else if (rand < 0.68f) { // +18% = 68%
                prefabToSpawn = ballelourde;
            }
            else if (rand < 0.82f) { // +14% = 82%
                prefabToSpawn = ressourceLourde;
            }
            else if (rand < 0.94f) { // +12% = 94%
                prefabToSpawn = argentlourd;
            }
            else { // 6%
                prefabToSpawn = heart;
            }
        }

        

        ajoutItem();
        

        GameObject item = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }

    }


    

    public void ajoutItem()
    {
        nemberspawn++;
    }

    public void removeItem()
    {
        nemberspawn--;
    }





    Vector3 GetRandomPositionOutsideScreen()
    {
        float x = Random.Range(-screenBounds.x+1f, screenBounds.x-1f);
        float y = Random.Range(-screenBounds.y+1f, screenBounds.y-1f);
        return new Vector3(x, y, 0);

    }


}
