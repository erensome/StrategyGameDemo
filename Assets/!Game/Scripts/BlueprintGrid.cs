using UnityEngine;

public class BlueprintGrid : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Mark(bool isAvailable)
    {
        spriteRenderer.color = isAvailable ? Color.green : Color.red;
    }
}
