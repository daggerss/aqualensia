using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Cooldown
{
    [SerializeField] private float cooldownTime;
    [SerializeField] private Slider cooldownSlider;

    private float nextFireTime;

    public bool IsCoolingDown => Time.time < nextFireTime;

    // Cooldown proper
    public void StartCooldown()
    {
        nextFireTime = Time.time + cooldownTime;
        cooldownSlider.gameObject.SetActive(true);
    }

    // Update Slider UI
    public void UpdateSlider()
    {
        if (cooldownSlider.value >= 1)
        {
            cooldownSlider.gameObject.SetActive(false);
            cooldownSlider.value = 0;
        }

        cooldownSlider.value += 1 / cooldownTime * Time.deltaTime;
    }
}
