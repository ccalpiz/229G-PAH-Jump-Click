using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public int maxHP = 3;
    public float jumpTime = 0.5f;
    public float maxJumpSpeed = 10f;
    public float maxChargeTime = 1.5f;
    public float minChargeTime = 0.2f;
    public bool isJumping = false;

    private int currentHP;
    private float chargeTime = 0f;
    private bool isCharging = false;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isJumping)
        {
            Vector2 origin = rb2d.position;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 velocity = CalculateProjectileVelocity(origin, mousePos, jumpTime);
            rb2d.velocity = velocity;
            isJumping = true;

            if (velocity.magnitude > maxJumpSpeed)
            {
                velocity = velocity.normalized * maxJumpSpeed;
            }

            rb2d.velocity = velocity;
            isJumping = true;

            velocity.x = Mathf.Clamp(velocity.x, -maxJumpSpeed, maxJumpSpeed);
            velocity.y = Mathf.Clamp(velocity.y, -maxJumpSpeed, maxJumpSpeed);
        }

        //if (!isJumping)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        isCharging = true;
        //        chargeTime = 0f;
        //    }

        //    if (Input.GetMouseButton(0) && isCharging)
        //    {
        //        chargeTime += Time.deltaTime;
        //        chargeTime = Mathf.Min(chargeTime, maxChargeTime);
        //    }

        //    if (Input.GetMouseButtonUp(0) && isCharging)
        //    {
        //        isCharging = false;
        //        float jumpTime = Mathf.Max(chargeTime, minChargeTime);

        //        Vector2 origin = rb2d.position;
        //        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        Vector2 velocity = CalculateProjectileVelocity(origin, mousePos, jumpTime);

        //        // Clamp the velocity to maxJumpSpeed
        //        if (velocity.magnitude > maxJumpSpeed)
        //        {
        //            velocity = velocity.normalized * maxJumpSpeed;
        //        }

        //        rb2d.velocity = velocity;
        //        isJumping = true;
        //    }
        //}
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;
        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;
        return new Vector2(velocityX, velocityY);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;

            Vector2 stoppedVelocity = rb2d.velocity;
            stoppedVelocity.x = 0f;
            rb2d.velocity = stoppedVelocity;
        }

        //foreach (ContactPoint2D contact in other.contacts)
        //{
        //    if (Vector2.Angle(contact.normal, Vector2.up) < 45f && rb2d.velocity.y <= 0f)
        //    {
        //        isJumping = false;
        //        break;
        //    }
        //}

        if (other.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int amount)
    {
        currentHP -= amount;
        Debug.Log("HP: " + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died");
        Invoke("RestartScene", 1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameObject.SetActive(false);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
