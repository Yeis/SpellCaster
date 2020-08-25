using UnityEngine;
using UnityEngine.UI;

public class EnemyHudController : MonoBehaviour
{
    public Slider castingSlider, healthSlider;

    public void SetupHUD(float castingTime, float healthPoints)
    {
        castingSlider.maxValue = castingTime;
        healthSlider.maxValue = healthPoints;
        castingSlider.value = castingTime;
        healthSlider.value = healthPoints;
    }

    public void UpdateHealth(float currentHealth)
    {
        healthSlider.value = currentHealth;
    }

    public void UpdateCastingTime(float timeLeft)
    {
        castingSlider.value = timeLeft;
    }


}