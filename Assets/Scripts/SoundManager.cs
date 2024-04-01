using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip soundEffectClip; // Assign the sound effect clip in the Inspector
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Ensure that an AudioClip is assigned
        if (soundEffectClip == null)
        {
            Debug.LogError("Sound effect clip is not assigned!");
        }
    }

    // Method to play the sound effect
    public void PlaySoundEffect()
    {
        // Check if audioSource and soundEffectClip are assigned
        if (audioSource != null && soundEffectClip != null)
        {
            // Play the sound effect
            audioSource.PlayOneShot(soundEffectClip);
        }
        else
        {
            Debug.LogError("AudioSource or sound effect clip is not assigned!");
        }
    }
}
