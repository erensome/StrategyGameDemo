using System.Collections.Generic;
using Components;
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

        private List<EntityData> productionItems = new();
        private EntityData currentProductEntityData;
        private int currentProductionIndex = 0;
        
        public EntityData CurrentProductEntityData => currentProductEntityData;
        
        private void Start()
        {
            // Hide sections at the start
            Hide();
        }
        
        public void Hide()
        {
            entitySection.SetActive(false);
            productionSection.SetActive(false);
            productionItems.Clear();
        }
        
        /// <summary>
        /// Display the information menu for the selected entity.
        /// </summary>
        /// <param name="entityData"></param>
        public void Display(EntityComponent entityComponent)
        {
            // null means clicked on empty space
            if (entityComponent == null || entityComponent.EntityData == null)
            {
                Hide();
                return;
            }
            
            entitySection.gameObject.SetActive(true);
            SetEntitySectionData(entityComponent.EntityData);

            // The entity is a building and it has production items, then show the production section
            if (entityComponent.EntityData is BuildingData buildingData && buildingData.ProductionItems.Count > 0)
            {
                productionSection.SetActive(true);
                SetProductionSectionData(buildingData);
            }
            else
            {
                productionSection.SetActive(false);
                productionItems.Clear();
            }
        }
        
        /// <summary>
        /// Set the data for the entity section.
        /// </summary>
        /// <param name="entityData"></param>
        private void SetEntitySectionData(EntityData entityData)
        {
            entityNameText.text = entityData.Name;
            entityImage.sprite = entityData.Icon;
        }
        
        /// <summary>
        /// Set the data for the production section.
        /// </summary>
        /// <param name="buildingData"></param>
        private void SetProductionSectionData(BuildingData buildingData)
        {
            productionItems.AddRange(buildingData.ProductionItems);
            currentProductionIndex = 0;
            SetProductInfo(productionItems[currentProductionIndex]);
            
            // Set buttons' interactable state
            previousProductButton.interactable = currentProductionIndex > 0;
            nextProductButton.interactable = currentProductionIndex < productionItems.Count - 1;
        }

        /// <summary>
        /// Set the data for the product info.
        /// </summary>
        /// <param name="productEntityData"></param>
        private void SetProductInfo(EntityData productEntityData)
        {
            productImage.sprite = productEntityData.Icon;
            productNameText.text = productEntityData.Name;
            currentProductEntityData = productEntityData;
        }
        
        public void PreviousProductionItem()
        {
            SetProductInfo(productionItems[--currentProductionIndex]);

            if (currentProductionIndex == 0)
            {
                previousProductButton.interactable = false;   
            }
            
            nextProductButton.interactable = true;
        }
        
        public void NextProductionItem()
        {
            SetProductInfo(productionItems[++currentProductionIndex]);

            if (currentProductionIndex == productionItems.Count - 1)
            {
                nextProductButton.interactable = false;   
            }
            
            previousProductButton.interactable = true;
        }
    }
}
