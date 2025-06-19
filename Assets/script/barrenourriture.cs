using UnityEngine;
using UnityEngine.UI;

public class barrenourriture : MonoBehaviour
{
    public Slider slider;

    

    

    public void SetMaxBarrenourriture(int health)
    {
        slider.maxValue = health;
        slider.value = 0;

        
    }

    public void SetBarrenourriture(int health)
    {
        slider.value = health;
        
    }

}
