using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource audioSource;
    
    public void PlaySound(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("Audio clip is null.");
            return;
        }

        audioSource.PlayOneShot(clip);
    }
}
