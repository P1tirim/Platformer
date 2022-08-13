using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    [SerializeField] private float damage;
    [SerializeField] private float cdAttackTime;
    private EnemyClass enemy;
    private GameObject player;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 direction;
    private float elapsedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemy = new EnemyClass();
        enemy.Create(this.gameObject ,maxHealth, damage, enemy);

        player = GameObject.FindWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        elapsedTime = cdAttackTime;
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.transform.position - transform.position;
        AttackAnimation();
        elapsedTime += Time.deltaTime;
    }

    public void FixedUpdate()
    {
        //Move enemy to the player
        rb.MovePosition(rb.position + direction * Time.fixedDeltaTime);
    }

    //Start attack animation
    public void AttackAnimation()
    {
        if (Global.zone == "right" && elapsedTime >= cdAttackTime)
        {
            animator.Play("EnemyAttackRight");
            elapsedTime = 0;
        }
        else if (Global.zone == "left" && elapsedTime >= cdAttackTime) 
        {
            animator.Play("EnemyAttackLeft");
            elapsedTime = 0;
        } 
    }

    //Event on animation of attack. Dealing damage to the player
    public void OnHitEndAnimation()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyAttackRight"))
        {
           if(transform.GetChild(1).GetComponent<AttackZone>().CheckPlayerInZone())
            {
                player.GetComponent<Player>().ApplyDamage(enemy.damage, "right");
            }
        }
        else
        {
            if (transform.GetChild(0).GetComponent<AttackZone>().CheckPlayerInZone())
            {
                player.GetComponent<Player>().ApplyDamage(enemy.damage, "left");
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
