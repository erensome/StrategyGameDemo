using UnityEngine;

/// <summary>
/// This test utility class is used to create world text in the game world.
/// </summary>
public static class TestUtils
{
    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default,
        Color? color = null, TextAnchor textAnchor = TextAnchor.MiddleCenter, int fontSize = 40, int sortingOrder = 0)
    {
        color ??= Color.white;
        return CreateWorldText(parent, text, localPosition, (Color)color, textAnchor, fontSize, sortingOrder);
    }

    private static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, Color color,
        TextAnchor textAnchor, int fontSize, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent);
        transform.localPosition = localPosition;
        transform.localScale = Vector3.one * 0.05f;

        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.text = text;
        textMesh.color = color;
        textMesh.fontSize = fontSize;
        textMesh.GetComponent<Renderer>().sortingOrder = sortingOrder;

        return textMesh;
    }
}
