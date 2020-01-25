using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{

    [Header("VARIABLES")]
    public float speed = 12f;
    public float gravity = -19.62f;
    public float jumpHeight = 3f;

    public float dashSpeed = 50f;
    public float dashDuration = 0.25f;

    private int amountOfDashesLeft;
    public int amountOfDashes = 1;

    float x;
    float z;
    Vector3 move;
    Vector3 velocity;

    [Header("CHECKS")]
    public bool isGrounded;
    private bool previouslyGrounded;
    public bool canJump;
    public bool isJumping;

    public bool canDash;
    public bool isDashing;

    [Header("GROUNDED")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("AUDIOCLIPS")]
    public AudioClip footstepSound;
    public AudioClip jumpSound;           // the sound played when character leaves the ground.
    public AudioClip landSound;           // the sound played when character touches back on ground.

    [Header ("COMPONENTS")]
    public GameObject explosionEffect;
    public CharacterController cc;
    public AudioSource audio;
    public Animator anim;

    public void Start()
    {
        cc = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        amountOfDashesLeft = amountOfDashes;
    }

    public void Update()
    {
        CheckInputs();
        Move();
        Gravity();
        Jump();

        CheckIfCanDash();
        GroundCheck();
    }

    public void CheckInputs()
    {
        // Move input
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        //Jump Input
        canJump = Input.GetButtonDown("Jump");

        //Dash Input
        if (Input.GetButtonDown("Dash"))
        {
            Dash();
        }
    }

    public void Move()
    {
        move = transform.right * x + transform.forward * z;
        cc.Move(move * speed * Time.deltaTime);

        if (((x != 0) || (z != 0)) && isGrounded)
        {
            anim.SetBool("Walk", true);
        }
        else anim.SetBool("Walk", false);
    }

    public void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }

    public void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    public void Jump()
    {
        if (canJump && isGrounded && previouslyGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            PlayJumpSound();
            canJump = false;
            isJumping = true;
        }

        if (!previouslyGrounded && isGrounded)
        {
            PlayLandingSound();
            isJumping = false;
        }

        if (isGrounded && !isJumping && previouslyGrounded)
        {
            velocity.y = 0f;
        }

        previouslyGrounded = isGrounded;
    }

    public void Dash()
    {
        if (canDash)
        {
            StartCoroutine(DashTime(dashDuration));
            amountOfDashesLeft--;
            Debug.Log("Dasheaste");
        }
    }

    private void CheckIfCanDash()
    {
        if (isGrounded && (velocity == move))
        {
            amountOfDashesLeft = amountOfDashes;
        }

        if (amountOfDashesLeft <= 0)
        {
            canDash = false;
        }
        else
        {
            canDash = true;
        }
    }

    private void PlayFootstepSound()
    {
        audio.clip = footstepSound;
        audio.pitch = (UnityEngine.Random.Range(0.5f, 1f));
        audio.Play();
    }

    private void PlayLandingSound()
    {
        audio.clip = landSound;
        audio.Play();
    }

    private void PlayJumpSound()
    {
        audio.clip = jumpSound;
        audio.Play();
        audio.pitch = 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }

    private IEnumerator DashTime(float waitTime)
    {
        velocity = transform.forward * dashSpeed;
        cc.Move(velocity * dashSpeed * Time.deltaTime);
        isDashing = true;

        yield return new WaitForSeconds(waitTime);

        velocity = Vector3.zero;
        isDashing = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ring")
        {
            StartCoroutine(DashTime(dashDuration));
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "InstantDeath")
        {
            Debug.Log("Game Over");
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
    }
}
