using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Gradient gradient;

    private void Start()
    {
        transform.localPosition = new Vector3(0, 0.55f, 0);
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        fillImage.color = gradient.Evaluate(slider.normalizedValue);
    }
    
    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        fillImage.color = gradient.Evaluate(1f);
    }
}
