using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayer : MonoBehaviour
{
    public GameObject chaser;
    public Vector3 Spawn = new Vector3(7, -1, 0);

    public int spawnNum = 5;
    public int packNum = 2;

    public float spawnTime = 5f;
    float timer = 0f;
    int spawned = 10;



    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "player")
        {
            spawned = 0;
        }
    }
    void Update()
    {
        //print("spawned" + spawned);
        if (spawned < spawnNum)
        {
            if (timer <= spawnTime)
            {
                timer += Time.deltaTime;
            }
            else 
            {
                for(int i =0;i<packNum;i++)
                {
                    GameObject enemy = Instantiate(chaser, Spawn, Quaternion.identity);
                }
                timer = 0;
                spawned+=1;


            }
        }

    }
}


