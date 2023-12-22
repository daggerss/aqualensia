using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Cooldown
{
    [field: SerializeField]
    public float CooldownTime {get; private set;}

    [SerializeField] private Slider cooldownSlider;
    [SerializeField] private Image cooldownRadial;

    private float nextFireTime;

    public bool IsCoolingDown => Time.time < nextFireTime;

    // Set (for upgrade purposes)
    public void SetCooldownTime(float newTime)
    {
        CooldownTime = newTime;
    }

    // Cooldown proper
    public void StartCooldown()
    {
        nextFireTime = Time.time + CooldownTime;

        if (cooldownSlider != null)
        {
            cooldownSlider.gameObject.SetActive(true);
        }
    }

    // Update Slider UI
    public void UpdateSlider()
    {
        if (cooldownSlider != null)
        {
            if (cooldownSlider.value >= 1)
            {
                cooldownSlider.gameObject.SetActive(false);
                cooldownSlider.value = 0;
            }
    
            cooldownSlider.value += 1 / CooldownTime * Time.deltaTime;
        }
        else
        {
            Debug.LogWarning("No cooldown slider found!");
        }
    }

    // Update Radial UI
    public void UpdateRadial()
    {
        if (cooldownRadial != null)
        {
            cooldownRadial.fillAmount -= 1 / CooldownTime * Time.deltaTime;
        }
        else
        {
            Debug.LogWarning("No cooldown radial found!");
        }
    }
}
