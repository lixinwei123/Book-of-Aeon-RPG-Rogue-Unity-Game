using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeController : MonoBehaviour
{
    // Start is called before the first frame update
    public Hashtable enemies = new Hashtable();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 7 )
        {
            //Debug.Log("hit zombie!");
            enemies.Add(collider.gameObject.name, collider.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 7 || collider.gameObject.layer == 8)
        {
            Debug.Log("bye bye zombie!");
            enemies.Remove(collider.gameObject.name);
            //enemies.Add(collision.collider.gameObject.name, collision.collider.gameObject);
        }
    }


}
