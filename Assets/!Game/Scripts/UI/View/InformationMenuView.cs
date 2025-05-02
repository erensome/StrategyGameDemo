using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// This class is used to display the information menu for the selected entity.
    /// </summary>
    public class InformationMenuView : MonoBehaviour
    {
        [Header("Entity Section")]
        [SerializeField] private GameObject entitySection;
        [SerializeField] private TextMeshProUGUI entityNameText;
        [SerializeField] private Image entityImage;
        
        [Header("Production Section")]
        [SerializeField] private GameObject productionSection;
        [SerializeField] private Image productImage;
        [SerializeField] private TextMeshProUGUI productNameText;
        [SerializeField] private Button previousProductButton;
        [SerializeField] private Button nextProductButton;
        
        private void Start()
        {
            // Hide sections at the start
            Hide();
        }
        
        public void Hide()
        {
            entitySection.SetActive(false);
            productionSection.SetActive(false);
        }
        
        public void DisplayEntity(EntityData entityData)
        {
            entitySection.SetActive(true);
            entityNameText.text = entityData.Name;
            entityImage.sprite = entityData.Icon;
        }

        public void DisplayProduct(EntityData productData)
        {
            productionSection.SetActive(true);
            productNameText.text = productData.Name;
            productImage.sprite = productData.Icon;
        }

        public void HideProductionSection()
        {
            productionSection.SetActive(false);
        }

        public void SetProductNavigationInteractable(bool previousEnabled, bool nextEnabled)
        {
            previousProductButton.interactable = previousEnabled;
            nextProductButton.interactable = nextEnabled;
        }
    }
}
