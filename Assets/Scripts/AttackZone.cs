using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    private Enemy enemyScript;
    private Rigidbody2D rbEnemy;
    bool playerInZone = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyScript =transform.parent.GetComponent<Enemy>();
        rbEnemy = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //if player in attack zone, return true
    public bool CheckPlayerInZone()
    {
        return playerInZone;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        //if enemy in zone of attack, add enemy in list

        if (collision.transform.tag == "Enemy")
        {
            ObjectsInAttackZone objectInAttackZone = new ObjectsInAttackZone();
            string zone = "";
            if (this.gameObject.name == "LeftZone")
            {
                zone = "Left";
            }
            else zone = "Right";

            objectInAttackZone.Add(collision.gameObject, zone, objectInAttackZone);

        }

        //if player in attack zone enemy, enemy stop and start attack
        if(collision.transform.tag == "Player" && transform.parent.tag == "Enemy")
        {
            Debug.Log("stop");
            playerInZone = true;
            rbEnemy.constraints = RigidbodyConstraints2D.FreezeAll;
            if (this.gameObject.name == "LeftZone")
            {
                enemyScript.AttackAnimation("left");
            }
            else enemyScript.AttackAnimation("right");
        }
    }

    
    void OnTriggerExit2D(Collider2D collision)
    {
        //if enemy exit zone, remove enemy from list

        if (transform.parent.tag == "Player")
        {
            ObjectsInAttackZone remove = Global.objectsInAttackZones.Find(f => f.obj == collision.gameObject);
            Global.objectsInAttackZones.Remove(remove);
        }
        // When player exit zone, enemy can move again
        else if(transform.parent.tag == "Enemy" && collision.tag == "Player")
        {
            playerInZone = false;
            rbEnemy.constraints = RigidbodyConstraints2D.None;
            rbEnemy.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        }
        
    }

    public class ObjectsInAttackZone
    {
        public GameObject obj;
        public string zone;
        public void Add(GameObject obj, string zone, ObjectsInAttackZone objectInAttack)
        {
            this.obj = obj;
            this.zone = zone;
            Global.objectsInAttackZones.Add(objectInAttack);
        }
    }
}
