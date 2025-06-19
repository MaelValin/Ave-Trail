using UnityEngine;
using UnityEngine.UI;

public class reloadbar : MonoBehaviour
{
    public Slider slider;

    

    public Image fill;

    public void SetMaxReload(int reload)
    {
        slider.maxValue = reload;
        slider.value = reload;

       
    }

    public void SetReload(int reload)
    {
        slider.value = reload;
        
    }

}
