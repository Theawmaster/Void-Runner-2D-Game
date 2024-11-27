using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource; // Music source
    [SerializeField] AudioSource sfxSource; // Sound effects source

    public AudioClip bgm; // Background music
    public AudioClip jumpSFX; // Jump sound effect
    public AudioClip crouchSFX; // Crouch sound effect
    public AudioClip clickSFX; // Click sound effect 
    public AudioClip deathSFX; // Death sound effect

    private void Start() {
        musicSource.clip = bgm; // Set background music
        musicSource.loop = true; // Loop background music
        musicSource.Play(); // Play background music
    }

    public void PlaySFX(AudioClip clip) {
        sfxSource.PlayOneShot(clip); // Play sound effect
    }
}
