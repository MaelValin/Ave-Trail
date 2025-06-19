using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MissionManager : MonoBehaviour
{

    public Button missionville;
    public Button missionbunker;

    public GameObject fondmissionville;
    public GameObject fondmissionbunker;

    public static MissionManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de MissionManager dans la scène");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

   
   void Start()
{
    SetMissionState(missionville, fondmissionville, 0.4f); // 40%
    SetMissionState(missionbunker, fondmissionbunker, 0.1f); // 10%
}

void SetMissionState(Button btn, GameObject fond, float chance)
{
    bool active = Random.value < chance;
    btn.interactable = active;
    fond.SetActive(!active);
}
     

    
}
