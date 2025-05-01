using System;
using System.Collections;
using System.Collections.Generic;
using EventBus;
using UnityEngine;

namespace Components
{
    /// <summary>
    /// Component that handles the movement logic for an entity.
    /// </summary>
    public class MovableComponent : MonoBehaviour, IMovable
    {
        [SerializeField] private float speed = 5f;
        private Coroutine moveCoroutine;
        private const float DistanceThreshold = 0.1f; 
        
        public event Action<Vector3> OnTargetPositionChanged;
        public float Speed => speed;
        
        /// <summary>
        /// Calculates the path to the target position and starts moving towards it.
        /// </summary>
        /// <param name="targetPosition"></param>
        public void Move(Vector3 targetPosition)
        {
            List<Vector3> path = GroundManager.Instance.Pathfinding.FindPath(transform.position, targetPosition);

            if (path == null || path.Count == 0)
            {
                Debug.LogWarning("No path found.");
                UIEventBus.TriggerMessageRaised("Clicked on an unreachable tile.");
                return;
            }

            path.RemoveAt(0); // Remove the first position (current position)
            StopMoveCoroutine();
            moveCoroutine = StartCoroutine(MoveTo(path));
        }

        private void OnDisable()
        {
            StopMoveCoroutine();
        }

        private void StopMoveCoroutine()
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                moveCoroutine = null;
            }
        }

        /// <summary>
        /// Moves the object to a list of target positions.
        /// </summary>
        /// <param name="targetPositions">A list of world path positions.</param>
        /// <returns></returns>
        private IEnumerator MoveTo(List<Vector3> targetPositions)
        {
            foreach (Vector3 targetPosition in targetPositions)
            {
                yield return MoveTo(targetPosition);
            }
        }

        /// <summary>
        /// Moves the object to a single target position.
        /// </summary>
        /// <param name="targetPosition">Target world posiiton.</param>
        /// <returns></returns>
        private IEnumerator MoveTo(Vector3 targetPosition)
        {
            OnTargetPositionChanged?.Invoke(targetPosition);

            while (Vector3.Distance(transform.position, targetPosition) > DistanceThreshold)
            {
                Vector3 position = transform.position;
                float step = speed * Time.deltaTime;

                transform.position = Vector3.MoveTowards(position, targetPosition, step);
                yield return null;
            }

            transform.position = targetPosition;
        }
    }
}
