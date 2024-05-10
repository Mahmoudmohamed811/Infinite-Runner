using UnityEngine;
using System.Collections;


public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Projectile prefab
    public float projectileSpeed = 20f; // Projectile speed
    public float projectileLifetime = 2f; // Projectile lifetime
    
    public Transform point;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (!PlayerManger.gameStart)
            return;
        //Increase speed
        if (projectileSpeed < 50)
            projectileSpeed += 0.1f * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F) || SwipeManager.tap)
        {
            
            FindObjectOfType<AudioManger>().PlaySound("fight");
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        // Create a projectile at the player's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, point.position, point.rotation);
       
        // Get the rigidbody component of the projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // Add force to the projectile in the forward direction
        rb.velocity = point.up * projectileSpeed;
        animator.SetBool("attack", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("attack", false);
        // Destroy the projectile after a certain amount of time
        Destroy(projectile, projectileLifetime);
    }
}
