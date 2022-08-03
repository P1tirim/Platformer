using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //if enemy in zone of attack, add enemy in list
    void OnTriggerEnter2D(Collider2D collision)
    {
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
    }

    //if enemy exit out zone, remove enemy from list
    void OnTriggerExit2D(Collider2D collision)
    {
        ObjectsInAttackZone remove = Global.objectsInAttackZones.Find(f => f.obj == collision.gameObject);
        Global.objectsInAttackZones.Remove(remove);
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
