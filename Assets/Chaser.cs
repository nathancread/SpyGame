using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Enemy
{
    public int health = 100;
    public GameObject deathEffect;
    public float speed = 2.5f;
    public float rotateSpeed = 2.5f;

    public float chaseDist = 3f;
    float lastShot = 0.0f;
    float fireRate = 0.5f;


    public Transform firePoint1;
    public Transform firePoint2;

    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    Transform player;
    float isMovingTimeOffset;


    public override void TakeDamage(int damage,string type)
    {
        if(type == "lazer"||type == "bullet")
        {
            damage=damage/2;
        }
        if(type =="explosion")
        {
            damage = damage*4;
        }
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Update()
    {
        player = FindPlayer();
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            RotateToPlayer();
            if (IsCloseToPlayer(chaseDist))
            {
                if (Time.time > fireRate + lastShot + isMovingTimeOffset)
                {
                    FireAway();
                    lastShot = Time.time;
                    isMovingTimeOffset = 0f;
                }
            }
            else
            {
                isMovingTimeOffset = 1f;
                MoveToPlayer(speed);
            }
        }
    }

    void FireAway()
    {
        GameObject bullet1 = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
        rb1.AddForce(firePoint1.up * bulletForce, ForceMode2D.Impulse);

        GameObject bullet2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        rb2.AddForce(firePoint2.up * bulletForce, ForceMode2D.Impulse);
    }
}
