using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Initialise
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform Astronaut;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float jumpTime = 0.3f;
    [SerializeField] private float crouchHeight = 0.5f;

    AudioManager audioSource;

    private void Awake()
    {
        audioSource = GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<AudioManager>();
    }
    

    private bool isGrounded = false;
    private bool isJumping = false;
    private float jumpTimer;

    private void Update() {

        // Player is on ground
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

        #region JUMPING
        // Checks player is on ground and user hits jump button
        if (isGrounded && Input.GetButtonDown("Jump")) {
            isJumping = true;
            rb.linearVelocity = Vector2.up * jumpForce;
            audioSource.PlaySFX(audioSource.jumpSFX);
        }

        // Check when player jumptimer is less than jump time then carry on moving upwards
        if (isJumping && Input.GetButton("Jump")) {
            if (jumpTimer < jumpTime) {
                rb.linearVelocity = Vector2.up * jumpForce;

                jumpTimer += Time.deltaTime;
            }

            else {
                isJumping = false; 
            }
        }

        // Check if user release button when player is jumping then will begin falling 
        if (Input.GetButtonUp("Jump")) {
            isJumping = false;
            jumpTimer = 0;
        }
        #endregion

        #region CROUCHING
        
        // Check if user is on ground and press crouch button then player will crouch
        if (isGrounded && Input.GetButton("Crouch")) {
            Astronaut.localScale = new Vector3(Astronaut.localScale.x, crouchHeight, Astronaut.localScale.z);
            audioSource.PlaySFX(audioSource.crouchSFX);

            if (isJumping) {
                Astronaut.localScale = new Vector3(Astronaut.localScale.x, 1f, Astronaut.localScale.z);
                audioSource.PlaySFX(audioSource.crouchSFX);
            }
        }

        // Check if user release button when player is crouching then will return to normal height
        if (Input.GetButtonUp("Crouch")) {
            Astronaut.localScale = new Vector3(Astronaut.localScale.x, 1f, Astronaut.localScale.z); 
        }

        #endregion
    
    }
}


