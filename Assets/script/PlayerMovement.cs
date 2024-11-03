using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 7.5f;
    private float jumpingPadY = 12f;
    private float jumpingPadX = 5f;
    private int jumpsLeft = 2; // Tracks the number of jumps left
    private int scoreNum = 0;
    

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Text score;
    [SerializeField] private ParticleSystem collect;


    void Update()
    {
        score.text = scoreNum.ToString();
        horizontal = Input.GetAxisRaw("Horizontal");

        // Perform jump if grounded or has jumps left
        if ((Input.GetButtonDown("Jump") && IsGrounded()) || (Input.GetButtonDown("Jump") && jumpsLeft > 0))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            jumpsLeft--; // Reduce the number of jumps left
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        Collider[] colliders = Physics.OverlapSphere(groundCheck.position, 0.2f, groundLayer);
        if (colliders.Length > 0)
        {
            jumpsLeft = 2; // Reset jumps if grounded
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "obstacle")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(other.tag == "cup")
        {
            SceneManager.LoadScene("level2");
        }
        if (other.tag == "cup2")
        {
            SceneManager.LoadScene("level3");
        }
        if (other.tag == "cup3")
        {
            SceneManager.LoadScene("level4");
        }
        if (other.tag == "portal")
        {
            scoreNum++;
            collect.Play();
            other.gameObject.SetActive(false);
        }
        if (other.tag == "jump")
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x + jumpingPadX, jumpingPadY);
        }
    }
}
