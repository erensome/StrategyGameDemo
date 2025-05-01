using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Components;

namespace UI
{
    /// <summary>
    /// Class representing a health bar world UI element.
    /// </summary>
    public class HealthBar : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private RectTransform rectTransform; // RectTransform of the health bar

        [SerializeField] private Slider slider;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image fillImage;
        [SerializeField] private Gradient gradient;

        [Header("Configs")] [SerializeField, Range(0.1f, 0.5f)]
        private float fadeDuration = 0.5f;

        private DamageableComponent _damageableComponent;

        private const float DefaultWidth = 400f;
        private const float DefaultHeight = 75f;

        private void Start()
        {
            _damageableComponent = GetComponentInParent<DamageableComponent>();
            if (_damageableComponent != null)
            {
                SetMaxHealth(_damageableComponent.MaxHealth);
                _damageableComponent.OnHealthChanged += UpdateDamageableBar;
                _damageableComponent.OnDeath += HandleDeath;
            }

            AdjustToParentSize();
        }
        
        protected virtual void OnDestroy()
        {
            if (_damageableComponent != null)
            {
                _damageableComponent.OnHealthChanged -= UpdateDamageableBar;
                _damageableComponent.OnDeath -= HandleDeath;
            }
        }

        /// <summary>
        /// Adjusts the health bar size to fit the parent entity's size.
        /// </summary>
        private void AdjustToParentSize()
        {
            Transform parent = transform.parent;
            if (parent == null)
                return;

            EntityData entityData = parent.GetComponent<EntityComponent>().EntityData;
            Vector2Int size = entityData.Size;
            Vector3 parentScale = parent.localScale;

            // Remove parent's scale effect from canvas size
            rectTransform.sizeDelta = new Vector2(size.x * DefaultWidth / parentScale.x, DefaultHeight / parentScale.y);
            transform.localPosition = new Vector3(0f, size.y / 1.5f, 0f);
        }

        private void SetMaxHealth(float maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
            fillImage.color = gradient.Evaluate(1f);
        }

        private void UpdateDamageableBar(float oldHealth, float newHealth)
        {
            slider.value = newHealth;
            fillImage.color = gradient.Evaluate(slider.normalizedValue);
        }

        private void HandleDeath()
        {
            slider.value = 0;

            canvasGroup.DOFade(0, fadeDuration)
                .OnComplete(() => { Destroy(gameObject); });
        }
    }
}
