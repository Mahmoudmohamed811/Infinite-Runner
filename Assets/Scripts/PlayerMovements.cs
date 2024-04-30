using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 10f;
    public float maxSpeed = 50f;
    public float jumpForce = 7f;
    public float gravity = -20f;
    
    private Rigidbody rb;
    bool isGrounded = true;

    private int desiredLane = 1;
    public float landDistance = 2.5f;

    private CapsuleCollider capsuleCollider;
    private Vector3 originalColliderCenter;
    private float originalColliderHeight;

    private bool isSliding = false;

    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        originalColliderCenter = capsuleCollider.center;
        originalColliderHeight = capsuleCollider.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManger.gameStart)
            return;
        //Increase speed
        if(moveSpeed < maxSpeed)
            moveSpeed += 0.1f * Time.deltaTime;
        // Player Movements

        if (Input.GetKeyDown(KeyCode.D) || SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }

        if (Input.GetKeyDown(KeyCode.A) || SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }
        Vector3 targetPostion = transform.position.z * transform.forward + transform.position.y * transform.up;
        
        if(desiredLane == 0)
        {
            targetPostion += Vector3.left * landDistance;
        }

        if (desiredLane == 2)
        {
            targetPostion += Vector3.right * landDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPostion, 70 * Time.deltaTime);
        
        

        // Player jump
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) || SwipeManager.swipeUp)
            {
                Jump();
                isGrounded = false;
            }
        }
        else
        {
            animator.SetBool("isJumping", false);
            rb.AddForce(Vector3.up * gravity * Time.deltaTime, ForceMode.Impulse);
        }

        //Player slide
        if ((Input.GetKeyDown(KeyCode.S) || SwipeManager.swipeDown) && !isSliding)
        {
            StartCoroutine(Slide());
        }
    }

    void FixedUpdate()
    {
        if (!PlayerManger.gameStart)
            return;
        // Move player forward
        animator.SetBool("isMoving", true);
        rb.MovePosition(rb.position + Vector3.forward * moveSpeed * Time.fixedDeltaTime);
    }

    void Jump()
    {
        
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetBool("isJumping", true);
        
    }

    IEnumerator Slide()
    {
        //animator.SetTrigger("Slide");
        animator.SetBool("isSliding", true);
        isSliding = true;
        // Reduce collider size
        capsuleCollider.center = new Vector3(originalColliderCenter.x, originalColliderCenter.y / 2, originalColliderCenter.z);
        capsuleCollider.height = originalColliderHeight / 2;

        yield return new WaitForSeconds(1.3f);
        animator.SetBool("isSliding", false);
        isSliding = false;
        RestoreCollider();

    }

    void RestoreCollider()
    {
        // Restore collider size
        capsuleCollider.center = originalColliderCenter;
        capsuleCollider.height = originalColliderHeight;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "obstacle")
        {
            StartCoroutine(death());
            
            FindObjectOfType<AudioManger>().StopSound("Main Theme");
            FindObjectOfType<AudioManger>().PlaySound("End Game");
        }
        if (collision.collider.tag == "ground")
        {
            isGrounded = true;
        }
        if(collision.collider.tag == "power")
        {
            SceneManager.LoadScene("Level2");
        }
    }

    private IEnumerator death()
    {
        animator.SetBool("death", true);
        yield return new WaitForSeconds(1.5f);
        PlayerManger.gameOver = true;
    }

}
