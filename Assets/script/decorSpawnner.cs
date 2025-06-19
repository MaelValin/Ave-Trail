using UnityEngine;
using System.Collections.Generic;

public class decorSpawnner : MonoBehaviour
{
    
    public int nemberspawn=10;
    public float minDistance = 0.5f; // Distance minimale entre deux décors
    private Vector2 screenBounds;

    public int randomgraphic = 1; // <-- Ajoute cette ligne

    public Camera camera;

    
    public GameObject[] decorPrefabsprairie;
    public GameObject[] decorPrefabsville;
    public GameObject[] decorPrefabsbunker;


    public GameObject decorVille;
    public GameObject decorBunker;
    public GameObject decorbunker2;
    
   
    private GameObject decorchoose;

    


    void Start()
    {
        Camera mainCamera = Camera.main;
    screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.nearClipPlane));

         if(ModManager.instance.Mod == "facile"){
            nemberspawn = 6;
         }
        
    else if(ModManager.instance.Mod == "moyen")
    {
        nemberspawn = 8;
    }
        
    else if(ModManager.instance.Mod == "difficile")
    {
        nemberspawn = 15;
    }
        

        
       SpawnDecor();
        
        

        
    }


   
    

    

    void SpawnDecor()
    {
        
        GameObject[] decorPrefabs = null;
        List<Vector3> decorPositions = new List<Vector3>();

        int attempts = 0;
        int maxAttempts = 2000;

        for (int i = 0; i < nemberspawn; i++)
        {
            attempts = 0;
            bool positionOK = false;
            Vector3 spawnPosition = Vector3.zero;
            if(ModManager.instance.Mod == "facile")
            {
                camera.backgroundColor = new Color(0.345f, 0.533f, 0.345f); // Change la couleur de fond pour le mode facile (example greenish color)
                decorPrefabs = decorPrefabsprairie;
                decorVille.SetActive(false);
                decorBunker.SetActive(false);
                decorbunker2.SetActive(false);
            }
            else if(ModManager.instance.Mod == "moyen")
            {
                camera.backgroundColor = new Color(0.556f, 0.556f, 0.556f); // Change la couleur de fond pour le mode moyen
                decorPrefabs = decorPrefabsville;
                decorVille.SetActive(true);
                decorBunker.SetActive(false);
                decorbunker2.SetActive(false);
            }
            else if(ModManager.instance.Mod == "difficile")
            {
                camera.backgroundColor = new Color(0.106f, 0.149f, 0.224f); // Change la couleur de fond pour le mode difficile
                decorPrefabs = decorPrefabsbunker;
                decorVille.SetActive(false);
                decorBunker.SetActive(true);
                decorbunker2.SetActive(true);
            }

            int randomIndex = Random.Range(0, decorPrefabs.Length);
            GameObject decorchoose = decorPrefabs[randomIndex];


            while (!positionOK && attempts < maxAttempts)
            {
                spawnPosition = GetRandomPositionInScreen();
                positionOK = true;

                // Vérifie la distance avec tous les décors déjà placés
                foreach (Vector3 pos in decorPositions)
                {
                    if (Vector3.Distance(pos, spawnPosition) < minDistance)
                        {
                            // Décale la position de 1.5f dans une direction aléatoire
                            Vector2 direction = (spawnPosition - pos).normalized;
                            if (direction == Vector2.zero)
                                direction = Random.insideUnitCircle.normalized;
                            spawnPosition += (Vector3)(direction * 1.5f);
                            
                            break;
                        }
                }
                attempts++;
            }

            if (positionOK)
            {
                decorPositions.Add(spawnPosition);
                Instantiate(decorchoose, spawnPosition, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomPositionInScreen()
    {
        float marge = 1.5f;
        float x = Random.Range(-screenBounds.x + marge, screenBounds.x - marge);
        float y = Random.Range(-screenBounds.y + marge, screenBounds.y - marge);
        return new Vector3(x, y, 0);
    }
    
}