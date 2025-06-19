using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
   public AudioClip[] playlist;
   public AudioClip[] playlistprairie;
   public AudioClip[] playlistville;
   public AudioClip[] playlistbunker;

    public AudioSource audioSource;
    private int musicIndex=0;

    public AudioMixerGroup soundEffectmixer;

    public static AudioManager instance;

   

    private void Awake(){

        if(instance != null){
            Debug.LogWarning("Il y a plus d'une instance de AudioManager dans la scène");
            return;
        }
        instance = this;
    }


    void Start()
    {
        
        if (SceneManager.GetActiveScene().name == "Jeudetir")
        {
            if(ModManager.instance.Mod == "facile"){
                PlayRandomprairie();
            }
            else if(ModManager.instance.Mod == "moyen"){
                PlayRandomville();
            }
            else if(ModManager.instance.Mod == "difficile"){
                PlayRandombunker();
            }
            
        }
        else{
            PlayRandom();
        }
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            
            if (SceneManager.GetActiveScene().name == "Jeudetir")
        {
            if(ModManager.instance.Mod == "facile"){
                PlayRandomprairie();
            }
            else if(ModManager.instance.Mod == "moyen"){
                PlayRandomville();
            }
            else if(ModManager.instance.Mod == "difficile"){
                PlayRandombunker();
            }
        }
        else{
            PlayRandom();
        }
        }
    }

    void PlayRandom()
    {
        musicIndex = Random.Range(0, playlist.Length);
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    void PlayRandomprairie()
    {
        musicIndex = Random.Range(0, playlistprairie.Length);
        audioSource.clip = playlistprairie[musicIndex];
        audioSource.Play();
        Debug.Log("PlayRandomprairie called");
    }

    void PlayRandomville()
    {
        musicIndex = Random.Range(0, playlistville.Length);
        audioSource.clip = playlistville[musicIndex];
        audioSource.Play();
        Debug.Log("PlayRandomville called");
    }

    void PlayRandombunker()
    {
        musicIndex = Random.Range(0, playlistbunker.Length);
        audioSource.clip = playlistbunker[musicIndex];
        audioSource.Play();
        Debug.Log("PlayRandombunker called");
    }

    public AudioSource CreateAudioSource(AudioClip clip, Vector3 pos)
    {
        GameObject tempGo = new GameObject("TempAudio");
        tempGo.transform.position = pos;
        AudioSource aSource = tempGo.AddComponent<AudioSource>();
        aSource.clip = clip;
        aSource.outputAudioMixerGroup = soundEffectmixer;
        aSource.Play();
        Destroy(tempGo, clip.length);
        return aSource;
    }
}
