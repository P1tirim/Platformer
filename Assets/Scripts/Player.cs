using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public FixedJoystick fixedJoystick;
    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    bool canJump = true;
    public GameObject rightZone;
    public GameObject leftZone;

    [SerializeField] private Button btn = null;

    Direction direction = Direction.Right;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        btn.onClick.AddListener(delegate { ParameterOnClick(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void FixedUpdate()
    {
        //Move player in horizontal direction

        Vector2 targetVelocity = Vector2.right * fixedJoystick.Horizontal;
        targetVelocity = targetVelocity * speed;
        Vector2 velocity = rb.velocity;
        Vector2 velocityChange = (targetVelocity - velocity);
        velocityChange.y = 0;
        rb.AddForce(velocityChange, ForceMode2D.Impulse);
        
        //Turn player
        if(velocityChange.x < 0)
        {
            direction = Direction.Left;
        }else if(velocityChange.x > 0)
        {
            direction = Direction.Right;
        }

        //Jump
        if (canJump && fixedJoystick.Vertical > 0.6f)
        {
            canJump = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    //Check ground
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground") canJump = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground") canJump = false;
    }

    public enum Direction
    {
        Left,
        Right
    }

    //Attack enemy
    public void Attack()
    {
        if (direction == Direction.Right)
        {
            for(int i = 0; i < Global.objectsInAttackZones.Count; i++)
            {
                if (Global.objectsInAttackZones[i].zone == "Right")
                {
                    Enemy.EnemyClass enemy = Global.listEnemy.Find(f => f.enemyObject == Global.objectsInAttackZones[i].obj);
                    enemy.currentHealth -= 5;
                    if (enemy.currentHealth <= 0) Destroy(enemy.enemyObject);
                    
                }
            }
            
        }else if(direction == Direction.Left)
        {
            for (int i = 0; i < Global.objectsInAttackZones.Count; i++)
            {
                if (Global.objectsInAttackZones[i].zone == "Left")
                {
                    Enemy.EnemyClass enemy = Global.listEnemy.Find(f => f.enemyObject == Global.objectsInAttackZones[i].obj);
                    enemy.currentHealth -= 5;
                    if (enemy.currentHealth <= 0) Destroy(enemy.enemyObject);
                }
            }
        }
    }

    //Click button
    private void ParameterOnClick()
    {
        Attack();
    }
}
