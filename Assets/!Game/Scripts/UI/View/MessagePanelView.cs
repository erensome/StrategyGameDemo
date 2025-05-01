using DG.Tweening;
using EventBus;
using TMPro;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// View for the message panel
    /// </summary>
    public class MessagePanelView : MonoBehaviour
    {
        [SerializeField] private RectTransform viewRectTransform;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField, Range(0.5f, 2f)] private float displayDuration = 1f;
        [SerializeField, Range(0.1f, 1f)] private float animationDuration = 0.5f;
        private Vector2 initialSize;
        private Tween currentTween;

        private void Awake()
        {
            UIEventBus.OnMessageRaised += DisplayMessage;
        }

        private void Start()
        {
            initialSize = viewRectTransform.sizeDelta;
            viewRectTransform.sizeDelta = Vector2.zero;
            messageText.text = string.Empty;
        }

        public void DisplayMessage(string message)
        {
            messageText.text = message;
            ShowPanel();
        }
    
        private void ShowPanel()
        {
            currentTween?.Kill();
    
            Sequence sequence = DOTween.Sequence();
            sequence.Append(viewRectTransform.DOSizeDelta(initialSize, animationDuration).SetEase(Ease.OutBack));
            sequence.AppendInterval(displayDuration);
            sequence.Append(viewRectTransform.DOSizeDelta(Vector2.zero, animationDuration).SetEase(Ease.InBack));
            sequence.OnComplete(() => currentTween = null);

            currentTween = sequence;
        }
    }
}
