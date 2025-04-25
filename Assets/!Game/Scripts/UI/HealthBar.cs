using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider slider;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image fillImage;
    [SerializeField] private Gradient gradient;

    private HealthComponent healthComponent;
    
    private void Start()
    {
        healthComponent = GetComponentInParent<HealthComponent>();
        if (healthComponent != null)
        {
            SetMaxHealth(healthComponent.MaxHealth);
            healthComponent.OnHealthChanged += UpdateHealthBar;
            healthComponent.OnDeath += HandleDeath;
        }
        
        transform.localPosition = new Vector3(0, 0.55f, 0);
    }
    
    private void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        fillImage.color = gradient.Evaluate(1f);
    }

    private void UpdateHealthBar(float oldHealth, float newHealth)
    {
        slider.value = newHealth;
        fillImage.color = gradient.Evaluate(slider.normalizedValue);
    }
    
    private void HandleDeath()
    {
        slider.value = 0;
        
        canvasGroup.DOFade(0, 0.5f)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
    
    protected virtual void OnDestroy()
    {
        if (healthComponent != null)
        {
            healthComponent.OnHealthChanged -= UpdateHealthBar;
            healthComponent.OnDeath -= HandleDeath;
        }
    }
}
