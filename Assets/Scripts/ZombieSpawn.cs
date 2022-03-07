using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieSpawn : MonoBehaviour
{
    private float nextActionTime = 0.0f;
    public GameObject[] enemies;
    public GameObject player;
    public bool isLeft = false;
    public static int enemyCounter = 0;
    // every 1f = 1 second
    public float period = 5f;
    public bool enableSpawn = true;
    // Start is called before the first frame upddate
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enableSpawn)
        {
            if (Time.time > nextActionTime)
            {
                nextActionTime = Time.time + period;
                var RandomOption = Random.Range(0, enemies.Length);
                period = Random.Range(2, 5);
                var enemy = enemies[RandomOption].gameObject.GetComponent<Cainos.PixelArtMonster_Dungeon.MonsterInputMouseAndKeyboard>();
                enemy.isLeft = isLeft;
                enemy.player = player;
                enemy.gameObject.layer = 7;
                enemy.jumpKey = KeyCode.None;
                enemy.timeToFire = 0.3f;
                GameObject instantiated = (GameObject)Instantiate(enemies[RandomOption], gameObject.transform.position, Quaternion.identity);
                enemyCounter += 1;
                instantiated.name = instantiated.name + enemyCounter.ToString();
                //  Debug.Log(instantiated.name);
            }
        }
    }
       
}
