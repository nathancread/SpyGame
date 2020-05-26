using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaserLazer : MonoBehaviour
{
    public GameObject hitEffect;
    public Rigidbody2D rigidBody;
    public int damage = 10;
    public float speed  = 20f;
    void start()
    {
        rigidBody.velocity = transform.right * speed;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if(player != null)
        {
            player.TakeDamage(damage);
        }
        GameObject effect = Instantiate(hitEffect,transform.position,Quaternion.identity);
        //print("hit!");
        Destroy(effect,0.167f);
        Destroy(gameObject);

    }
}
