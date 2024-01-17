using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody2d;
    public float flapStrength;
    public ScoreManager logic;
    public bool playerIsAlive = true;
    public float tiltSmooth = 5; // Adjust this value to change the smoothness of the tilt
   
    private AudioSource[] allAudioSources;
    private AudioSource  jumpSound;
    private AudioSource hitSound;
    private bool canMakeHitSound = true;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<ScoreManager>();
        allAudioSources = GetComponents<AudioSource>();
        jumpSound = allAudioSources[0];
        hitSound = allAudioSources[1];
    }
        // Update is called once per frame
            void Update()
            {
            if (Input.GetKeyDown(KeyCode.Space) && playerIsAlive)
            {
                myRigidBody2d.velocity = Vector2.up * flapStrength;
                jumpSound.Play();
            }

            if (transform.position.y > 4.5 || transform.position.y < -6.5)
            {
                logic.gameOver();
                playerIsAlive = false;
            }

            TiltBird();
            }

        void TiltBird()
        {
            float angle = 0;
            if (myRigidBody2d.velocity.y > 0)
            {
                angle = Mathf.Lerp(0, 25, myRigidBody2d.velocity.y / flapStrength);
            }
            else
            {
                angle = Mathf.Lerp(0, -45, -myRigidBody2d.velocity.y / flapStrength);
            }

            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * tiltSmooth);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (canMakeHitSound==true)
        {
            hitSound.Play();
            canMakeHitSound = false;
        }
            logic.gameOver();
            playerIsAlive = false;
        }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    canMakeHitSound = true;
    //}
}
