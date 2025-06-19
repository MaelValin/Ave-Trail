using UnityEngine;
using UnityEngine.UI;

public class barreargent : MonoBehaviour
{
    public Slider slider;

    

    public void SetMaxbarreargent(int health)
    {
        slider.maxValue = health;
        slider.value = 0;


    }

    public void Setbarreargent(int health)
    {
        slider.value = health;
    }

}
