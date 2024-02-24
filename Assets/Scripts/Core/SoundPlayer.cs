using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip soundClipHit;
    public AudioClip soundClipCry;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHit()
    {
        audioSource.PlayOneShot(soundClipHit);
    }


    public void PlaySoundCry()
    {
        audioSource.PlayOneShot(soundClipCry);
    }
}
