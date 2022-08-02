using UnityEngine;

public class Player : MonoBehaviour
{
    public FixedJoystick fixedJoystick;
    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
      
    }

    //Move player in horizontal direction
    void FixedUpdate()
    {
        Vector2 targetVelocity = Vector2.right * fixedJoystick.Horizontal;
        targetVelocity = targetVelocity * speed;
        Vector2 velocity = rb.velocity;
        Vector2 velocityChange = (targetVelocity - velocity);
        velocityChange.y = 0;
        rb.AddForce(velocityChange, ForceMode2D.Impulse);

        if (canJump && fixedJoystick.Vertical > 0.6f)
        {
            Debug.Log("jump");
            canJump = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground") canJump = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground") canJump = false;
    }
}
