using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float maxHealth;
    private EnemyClass enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = new EnemyClass();
        enemy.Create(this.gameObject ,maxHealth, enemy);
    }

    // Update is called once per frame
    void Update()
    {
        
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
