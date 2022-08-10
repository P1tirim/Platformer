using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    [SerializeField] private float damage;
    private EnemyClass enemy;
    private GameObject player;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        enemy = new EnemyClass();
        enemy.Create(this.gameObject ,maxHealth, damage, enemy);

        player = GameObject.FindWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.transform.position - transform.position;
    }

    public void FixedUpdate()
    {
        //Move enemy to the player
        rb.MovePosition(rb.position + direction * Time.fixedDeltaTime);
    }

    //Start attack animation
    public void AttackAnimation(string zone)
    {
        if(zone == "right")
        {
            animator.Play("EnemyAttackRight");
        }else animator.Play("EnemyAttackLeft");
    }

    //Event on animation of attack. Dealing damage to the player
    public void OnHitEndAnimation()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyAttackRight"))
        {
           if(transform.GetChild(1).GetComponent<AttackZone>().CheckPlayerInZone())
            {
                player.GetComponent<Player>().ApplyDamage(enemy.damage);
            }
        }
        else
        {
            if (transform.GetChild(0).GetComponent<AttackZone>().CheckPlayerInZone())
            {
                player.GetComponent<Player>().ApplyDamage(enemy.damage);
            }
        }
    }

    public void ApplyDamage(float damage)
    {

    }

    //Create enemy and add in list
    public class EnemyClass
    {
        public GameObject enemyObject;
        public float maxHealth { get; private set; }
        public float currentHealth { get; set; }

        public float damage { get; set; }

        public void Create(GameObject obj ,float health, float damage, EnemyClass enemy)
        {
            enemyObject = obj;
            maxHealth = health;
            currentHealth = maxHealth;
            this.damage = damage;
            Global.listEnemy.Add(enemy);
        }
    
    }
}
