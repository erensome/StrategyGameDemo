using System;
using System.Collections.Generic;
using EventBus;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InfiniteScroll : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private List<BuildingData> buildingDataList = new();

        [SerializeField] private RectTransform scrollRectTransform;
        [SerializeField] private RectTransform contentRectTransform;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;

        private ProductionMenuItem lastSelectedItem; // last focused item
        private int columnCount; // number of columns in the grid layout group
        private int itemCount; // total created items count
        private float viewportHeight; // total height of the scroll view
        private float cellSpacing; // space between cells-images
        private float cellHeight; // height of each cell-image
        private Vector2 padding; // padding of the grid layout group
        private readonly List<RectTransform> items = new();
        
        private float Threshold => cellHeight + cellSpacing; // threshold for moving items

        private void Awake()
        {
            UIEventBus.OnProductionMenuItemSelected += HandleProductionMenuItemSelected;
        }

        // Start is called before the first frame update
        void Start()
        {
            Init();
            FillScrollContentWithImages();
            contentRectTransform.localPosition = new Vector2(0, 2 * Threshold);
        }
        
        private void OnDestroy()
        {
            UIEventBus.OnProductionMenuItemSelected -= HandleProductionMenuItemSelected;
        }

        private void Update()
        {
            float contentPos = contentRectTransform.anchoredPosition.y;

            if (contentPos > 2 * Threshold)
            {
                MoveTopItemToBottom();
                contentRectTransform.anchoredPosition = new Vector2(0, contentPos - Threshold);
            }
            else if (contentPos < Threshold)
            {
                MoveBottomItemToTop();
                contentRectTransform.anchoredPosition = new Vector2(0, contentPos + Threshold);
            }
        }
        
        private void Init()
        {
            itemCount = buildingDataList.Count;
            columnCount = gridLayoutGroup.constraintCount;

            viewportHeight = scrollRectTransform.rect.size.y;
            cellHeight = gridLayoutGroup.cellSize.y;
            cellSpacing = gridLayoutGroup.spacing.y;

            padding = new Vector2(gridLayoutGroup.padding.top, gridLayoutGroup.padding.bottom);
        }

        private void FillScrollContentWithImages()
        {
            int buildingDataIndex = 0;
            int rowCount = Mathf.FloorToInt((viewportHeight - (padding.x + padding.y)) / (cellHeight + cellSpacing));

            for (int i = 0;
                 i < rowCount + 2;
                 i++) // This added two rows will be used for seamless scrolling (placeholders)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    ProductionMenuItem productionMenuItem = ObjectPoolManager.Instance.GetObjectFromPool("ProductionMenuItem", contentRectTransform)
                        .GetComponent<ProductionMenuItem>();
                    productionMenuItem.Initialize(buildingDataList[buildingDataIndex]);
                    buildingDataIndex = (buildingDataIndex + 1) % itemCount;
                    items.Add(productionMenuItem.RectTransform);
                }
            }
        }

        private void MoveTopItemToBottom()
        {
            for (int i = 0; i < columnCount; i++)
            {
                RectTransform topItem = items[0];
                items.RemoveAt(0);
                items.Add(topItem);

                topItem.SetAsLastSibling();
            }
        }

        private void MoveBottomItemToTop()
        {
            for (int i = 0; i < columnCount; i++)
            {
                RectTransform bottomItem = items[^1];
                items.RemoveAt(items.Count - 1);
                items.Insert(0, bottomItem);
                bottomItem.SetAsFirstSibling();
            }
        }
        
        private void HandleProductionMenuItemSelected(ProductionMenuItem productionMenuItem)
        {
            if (lastSelectedItem != null)
            {
                lastSelectedItem.Unfocus();
            }

            lastSelectedItem = productionMenuItem;
            lastSelectedItem.Focus();
        }
    }
}
