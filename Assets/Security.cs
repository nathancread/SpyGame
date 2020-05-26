using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security : Enemy
{

    public GameObject explanationPoint;
    public GameObject chaser;
    public Vector3 Spawn = new Vector3(7, -1, 0);

    Transform player;
    //Rigidbody2D rb;
    public int health = 10;
    public float maxRange = 2f;
    bool continueCoroutine = false;


    void Start()
    {
        StartCoroutine(spawnEneimies());
    }
    // Update is called once per frame

    void FixedUpdate()
    {
        //player = FindPlayer();
        RotateToPlayer();

        if (playerIsVisible())
        {
            if (IsCloseToPlayer(maxRange))
            {
                continueCoroutine = true;
                //StartCoroutine(spawnEneimies());

            }
        }

    }
    public override void TakeDamage(int damage, string type)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    IEnumerator spawnEneimies()
    {
        while (!continueCoroutine)
        {
            //print("going");
        yield return new WaitForSeconds(.1f);
        }
        print("spawning");
        GameObject effect = Instantiate(explanationPoint, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
        Destroy(effect, 1f);
        RotateToPlayer();
        GameObject enemy = Instantiate(chaser, Spawn, Quaternion.identity);
        continueCoroutine = false;
        yield return new WaitForSeconds(1f);
        StartCoroutine(spawnEneimies());
    }
    bool playerIsVisible()
    {
        int layer_mask = LayerMask.GetMask("player", "normal");
        Debug.DrawRay(gameObject.transform.position, gameObject.transform.up, Color.red);
        RaycastHit2D hitinfo = Physics2D.Raycast(gameObject.transform.position,  gameObject.transform.up, 100f, layer_mask);
        if (hitinfo)
        {
            //print(hitinfo.transform.gameObject.tag);
            if (hitinfo.transform.gameObject.tag == "player")
            {
                //print("in LOS");
                return true;
            }
            if (hitinfo.transform.gameObject.tag == "wall")
            {
                //print("hit wall");
            }
        }
        return false;
    }
}
