using System;
using UnityEngine;

namespace GridSystem
{
    public class Grid<T>
    {
        public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
        public class OnGridObjectChangedEventArgs : EventArgs
        {
            public int x;
            public int y;
        }
        
        private int width, height;
        private float cellSize;
        private Vector3 originPosition;
        private T[,] gridArray;
        private TextMesh[,] textArray;

        public int Width => width;
        public int Height => height;
        public float CellSize => cellSize;
        public Vector3 OriginPosition => originPosition;
        
        public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<T>, int, int, T> createGridObject, bool showDebug = false)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;
            gridArray = new T[width, height];
            textArray = new TextMesh[width, height];


            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    gridArray[i, j] = createGridObject(this, i, j);
                }
            }

            if (showDebug) // Set to false to disable world text and grid lines
            {
                Transform parent = new GameObject("Grid").transform;
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Vector3 position = GetWorldPosition(i, j) + new Vector3(cellSize, cellSize) * 0.5f; // center the text
                        var worldText = TestUtils.CreateWorldText(gridArray[i, j]?.ToString(), parent, position, Color.white);
                        textArray[i, j] = worldText;

                        Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i, j + 1), Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i + 1, j), Color.white, 100f);
                    }

                    Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
                
                    OnGridObjectChanged += (_, eventArgs) => {
                        textArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
                    };
                }
            }
        }
        
        public void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
            y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        }

        public T GetGridObject(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return gridArray[x, y];
            }
            else
            {
                return default;
            }
        }

        public T GetGridObject(Vector3 worldPosition)
        {
            GetXY(worldPosition, out int x, out int y);
            return GetGridObject(x, y);
        }

        public void SetGridObject(int x, int y, T value)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                gridArray[x, y] = value;
                TriggerGridObjectChanged(x, y);
            }
        }

        public void SetGridObject(Vector3 worldPosition, T value)
        {
            GetXY(worldPosition, out int x, out int y);
            SetGridObject(x, y, value);
        }
        
        public void TriggerGridObjectChanged(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x, y = y });
            }
        }
        
        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * cellSize + originPosition;
        }
    }

    public class GridObject
    {
        private int x, y;
        private string name;
        private Grid<GridObject> grid;
        
        public GridObject(Grid<GridObject> grid, int x, int y, string name)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
            this.name = name;
        }
        
        public void SetName(string name)
        {
            this.name = name;
            grid.TriggerGridObjectChanged(x, y);
        }
        
        public override string ToString()
        {
            return name;
        }
    }
}
