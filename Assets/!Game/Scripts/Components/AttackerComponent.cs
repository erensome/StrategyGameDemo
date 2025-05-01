using System;
using System.Collections;
using UnityEngine;

namespace Components
{
    /// <summary>
    /// Component that handles the attack logic for an entity.
    /// </summary>
    public class AttackerComponent : MonoBehaviour, IAttacker
    {
        public GameObject AttackerObject => gameObject;

        [SerializeField, Range(0.5f, 5f)] private float attackCooldown = 1f;
        [SerializeField] private Transform muzzlePoint;
        [SerializeField] private LineRenderer bulletLine;
        [SerializeField] private AudioClip attackSound;

        private Coroutine currentAttackRoutine;
        private float attackDamage;
        private WaitForSeconds waitForAttackCooldown;
        private WaitForSeconds waitForBulletLine;

        public event Action<IAttacker, IDamageable> OnAttack;

        public float AttackDamage
        {
            get => attackDamage;
            set => attackDamage = Mathf.Max(0, value);
        }

        private void Awake()
        {
            waitForAttackCooldown = new WaitForSeconds(attackCooldown);
            waitForBulletLine = new WaitForSeconds(0.1f);
        }

        private void OnDisable()
        {
            StopCurrentAttack();
        }

        private void StopCurrentAttack()
        {
            if (currentAttackRoutine != null)
            {
                StopCoroutine(currentAttackRoutine);
                currentAttackRoutine = null;
            }
        }

        public void Attack(IDamageable target)
        {
            if (target.IsDead)
            {
                Debug.LogWarning("Target is dead.");
                return;
            }

            if (currentAttackRoutine != null)
            {
                StopCoroutine(currentAttackRoutine);
            }

            StopCurrentAttack();
            currentAttackRoutine = StartCoroutine(AutoAttackRoutine(target));
        }
        
        /// <summary>
        /// Coroutine to handle the auto-attack routine.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private IEnumerator AutoAttackRoutine(IDamageable target)
        {
            if (target == null)
            {
                Debug.LogWarning("Target is null.");
                yield break;
            }

            while (!target.IsDead)
            {
                StartCoroutine(FireSingleShot(target));
                yield return waitForAttackCooldown;
            }
        }

        /// <summary>
        /// Fires a single shot at the target.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private IEnumerator FireSingleShot(IDamageable target)
        {
            if (target.DamageableObject != null)
            {
                Vector3 targetPosition = target.DamageableObject.transform.position;

                if (!target.IsDead)
                {
                    OnAttack?.Invoke(this, target);

                    // Play attack sound
                    AudioManager.Instance.PlaySound(attackSound);

                    // Show bullet trail
                    ShowBulletTrail(muzzlePoint.position, targetPosition);
                    yield return waitForBulletLine;
                    HideBulletTrail();

                    target.TakeDamage(AttackDamage);
                }
            }
        }

        /// <summary>
        /// Shows the bullet trail between the start and end points.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        private void ShowBulletTrail(Vector3 startPoint, Vector3 endPoint)
        {
            if (bulletLine != null)
            {
                bulletLine.enabled = true;
                bulletLine.SetPosition(0, startPoint);
                bulletLine.SetPosition(1, endPoint);
            }
        }

        /// <summary>
        /// Hides the bullet trail.
        /// </summary>
        private void HideBulletTrail()
        {
            if (bulletLine != null)
            {
                bulletLine.enabled = false;
            }
        }
    }
}
