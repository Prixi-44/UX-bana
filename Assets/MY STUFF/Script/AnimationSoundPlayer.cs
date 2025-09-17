using UnityEngine;

public class AnimationSoundPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    // 🔥 Call this from an Animation Event
    public void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

