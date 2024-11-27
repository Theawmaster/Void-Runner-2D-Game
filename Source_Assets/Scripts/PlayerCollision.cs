using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    AudioManager audioSource;

    private void Awake()
    {
        audioSource = GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    private void Start()
    {
         GameManager.instance.on_Play.AddListener(OnGameStart);
    }

    private void OnGameStart()
    {
        gameObject.SetActive(true);
    }

    // Check if player collides with obstacle
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check if player collides with obstacle
        if(other.transform.tag == "Obstacle")
        {
            // Set player to inactive
            gameObject.SetActive(false);

            GameManager.instance.GameOver();
            audioSource.PlaySFX(audioSource.deathSFX);
        }
    }
}
