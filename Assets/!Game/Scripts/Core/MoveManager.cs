using UnityEngine;

public class MoveManager : MonoSingleton<MoveManager>
{
    public void HandleMove(Vector3 worldPosition)
    {
        IMovable movable = (SelectionManager.Instance.CurrentSelectable as MonoBehaviour)?.GetComponent<IMovable>();

        if (movable == null)
        {
            Debug.LogWarning("No movable selected.");
            return;
        }

        movable.Move(worldPosition);
    }
}