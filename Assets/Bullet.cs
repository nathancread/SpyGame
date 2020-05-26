using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explodeEffect;
    public GameObject hitEffect;

    public Rigidbody2D rigidBody;
    public int damage = 30;
    public int aoeDamage = 20;
    public float aoeRadius = 10f;
    public float speed = 5f;
    void start()
    {
        rigidBody.velocity = transform.right * speed;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        DealDamage(collision);
        HitAnimation();
        Disapear();

    }
    public void HitAnimation()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
    }
    public void Explode()
    {
        GameObject effect = Instantiate(explodeEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);
    }
    public void Disapear()
    {
        Destroy(gameObject);
    }
    void DealDamage(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(damage,"bullet");
        }
        if (collision.tag == "player")
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            player.TakeDamage(damage);
        }

    }
    public void ExplosionDamage(Collider2D collision)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, aoeRadius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            print(hitColliders[i].gameObject.tag);
            if (hitColliders[i].tag == "enemy")
            {
                Enemy enemy = hitColliders[i].GetComponent<Enemy>();
                enemy.TakeDamage(aoeDamage,"explosion");

            }
            else if (hitColliders[i].tag == "player")
            {
                PlayerMovement player = hitColliders[i].GetComponent<PlayerMovement>();
                player.TakeDamage(aoeDamage / 2);
            }
            else if (hitColliders[i].tag == "bullet")
            {
                //cool idea
                // Bullet b = hitColliders[i].GetComponent<Bullet>();
                // if (this != b)
                // {
                //     //b.ExplosionDamage(hitColliders[i]);
                //     b.Explode();
                // }

            }

        }
    }

}
