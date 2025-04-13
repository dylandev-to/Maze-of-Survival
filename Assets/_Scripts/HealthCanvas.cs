using UnityEngine;
using UnityEngine.UI;

public class HealthCanvas : MonoBehaviour
{
    public Slider Slider;

    void Update()
    {
        Slider.value = Mathf.Clamp(PlayerHealth.Global_Health, 0, 100);
    }
}
