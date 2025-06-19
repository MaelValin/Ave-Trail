using UnityEngine;

public class ennemieSpawnner : MonoBehaviour
{
    public GameObject enemyNormalPrairie;
    public GameObject enemyRapidePrairie;
    public GameObject enemyLourdPrairie; 

    public GameObject enemyNormalVille;
    public GameObject enemyRapideVille;
    public GameObject enemyLourdVille;

    public GameObject enemyNormalBunker;
    public GameObject enemyRapideBunker;
    public GameObject enemyLourdBunker;

    private GameObject enemyNormal;
    private GameObject enemyRapide;
    private GameObject enemyLourd;


    public float spawnInterval; // Intervalle entre chaque spawn
    private Vector2 screenBounds;

    public static ennemieSpawnner instance;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ennemieSpawnner dans la scène");
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

        if(ModManager.instance.Mod == "facile")
        {
            spawnInterval = 5.0f; // Intervalle de spawn pour le mode facile
            enemyNormal = enemyNormalPrairie;
            enemyRapide = enemyRapidePrairie;
            enemyLourd = enemyLourdPrairie;
        }
        else if(ModManager.instance.Mod == "moyen")
        {
            spawnInterval = 3.0f; // Intervalle de spawn pour le mode moyen
            enemyNormal = enemyNormalVille;
            enemyRapide = enemyRapideVille;
            enemyLourd = enemyLourdVille;
        }
        else if(ModManager.instance.Mod == "difficile")
        {
            spawnInterval = 2.0f; // Intervalle de spawn pour le mode difficile
            enemyNormal = enemyNormalBunker;
            enemyRapide = enemyRapideBunker;
            enemyLourd = enemyLourdBunker;
        }

        // Lancer le spawn des ennemis à intervalles réguliers
        InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
    }

    public void SpawnEnemy()
    {
        // Générer une position aléatoire en dehors de l'écran
        Vector3 spawnPosition = GetRandomPositionOutsideScreen();

        float rand = Random.value;
        GameObject prefabToSpawn = enemyNormal;

        if(ModManager.instance.Mod == "facile")
          {
            if (rand < 0.7f)
                        prefabToSpawn = enemyNormal;      // 70%
                    else if (rand < 0.9f)
                        prefabToSpawn = enemyRapide;      // 20%
                    else
                        prefabToSpawn = enemyLourd;       // 10%
          }
        else if(ModManager.instance.Mod == "moyen")
          {
            if (rand < 0.5f)
                        prefabToSpawn = enemyNormal;      // 50%
                    else if (rand < 0.8f)
                        prefabToSpawn = enemyRapide;      // 30%
                    else
                        prefabToSpawn = enemyLourd;       // 20%
          }
        else if(ModManager.instance.Mod == "difficile")
          {
            if (rand < 0.2f)
                        prefabToSpawn = enemyNormal;      // 20%
                    else if (rand < 0.6f)
                        prefabToSpawn = enemyRapide;      // 40%
                    else
                        prefabToSpawn = enemyLourd;       // 40%
          }

        

        GameObject enemy = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        var comportementNormal = enemy.GetComponent<ennemieComportement_normal>();
        if (comportementNormal != null)
        {
            comportementNormal.player = GameObject.FindGameObjectWithTag("Player");
        }

        var comportementRapide = enemy.GetComponent<ennemieComportement_rapide>();
        if (comportementRapide != null)
        {
            comportementRapide.player = GameObject.FindGameObjectWithTag("Player");
        }

    }

    
  public void UpdateSpawnInterval(float newInterval)
{
    spawnInterval = Mathf.Max(newInterval, 0.01f); // Valeur minimale de sécurité
    CancelInvoke(nameof(SpawnEnemy));
    if (spawnInterval > 0.1f)
        InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
    else
        Debug.LogWarning("spawnInterval trop petit, InvokeRepeating non lancé !");
}




    Vector3 GetRandomPositionOutsideScreen()
    {
        // Choisir un côté aléatoire (haut, bas, gauche, droite)
        int side = Random.Range(0, 4);
        Vector3 position = Vector3.zero;

        switch (side)
        {
            case 0: // Haut
                position = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y + 1, 0);
                break;
            case 1: // Bas
                position = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y - 1, 0);
                break;
            case 2: // Gauche
                position = new Vector3(-screenBounds.x - 1, Random.Range(-screenBounds.y, screenBounds.y), 0);
                break;
            case 3: // Droite
                position = new Vector3(screenBounds.x + 1, Random.Range(-screenBounds.y, screenBounds.y), 0);
                break;
        }

        return position;
    }
}
