using EventBus;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Presenter for the message panel
    /// </summary>
    public class MessagePanelPresenter : MonoBehaviour
    {
        [SerializeField] private MessagePanelView view;

        private void Awake()
        {
            UIEventBus.OnMessageRaised += SetNewMessage;
        }

        private void OnDestroy()
        {
            UIEventBus.OnMessageRaised -= SetNewMessage;
        }

        public void SetNewMessage(string message)
        {
            if (view == null)
            {
                Debug.LogError("MessagePanelView is not assigned in the inspector.");
                return;
            }
            
            view.DisplayMessage(message);
        }
    }
}
