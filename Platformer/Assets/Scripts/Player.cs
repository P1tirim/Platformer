using UnityEngine;

public class Player : MonoBehaviour
{
    public FixedJoystick fixedJoystick;
    Rigidbody2D rb;
    public float speed;

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
        rb.AddForce(velocityChange, ForceMode2D.Impulse);
        
    }
}
