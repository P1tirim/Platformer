using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float maxHealth;
    private EnemyClass enemy;
    private GameObject player;
    private Rigidbody2D rb;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        enemy = new EnemyClass();
        enemy.Create(this.gameObject ,maxHealth, enemy);

        player = GameObject.FindWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.transform.position - transform.position;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * Time.fixedDeltaTime);
    }

    //Create enemy and add in list
    public class EnemyClass
    {
        public GameObject enemyObject;
        public float maxHealth { get; private set; }
        public float currentHealth { get; set; }

        public void Create(GameObject obj ,float health, EnemyClass enemy)
        {
            enemyObject = obj;
            maxHealth = health;
            currentHealth = maxHealth;
            Global.listEnemy.Add(enemy);
        }
    
    }
}
