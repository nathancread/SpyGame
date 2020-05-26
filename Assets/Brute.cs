using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brute : Enemy
{

    public int health = 200;
    public float speed = 2.5f;
    public float rotateSpeed = 2.5f;
    public int numBullets = 10;
    public float burstOffset = .075f;


    float fireRate = 0.5f;
    
    float lastShot = 0.0f;
    float gunTimer = 0f;

    public float chaseDist = 5f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float movingTimeOffset = 1f;
    public float reloadTimeOffset = .5f;

    int Citerator = 0;
    Transform player;
    IEnumerator firing;


    // Start is called before the first frame update
    void Start()
    {
        gunTimer = movingTimeOffset;
    }

    // Update is called once per frame
    void Update()
    {

        player = FindPlayer();

    }
    void FixedUpdate()
    {
        if (player != null)
        {
            //RotateToPlayerFP();
            if (IsCloseToPlayer(chaseDist))
            {
                gunTimer -= Time.deltaTime;
                if (gunTimer < 0)
                {
                    if (firing == null)
                    {
                        firing = FireBurst(numBullets, burstOffset);
                        StartCoroutine(firing);
                        //StopCoroutine(firing);
                    }

                }
                RotateToPlayerFP();
            }
            else
            {
                gunTimer = movingTimeOffset;
                MoveToPlayer(speed);
                RotateToPlayerFP();

            }
        }
    }
    public override void TakeDamage(int damage,string type)
    {
        if(type == "lazer"||type =="explosion")
        {
            damage=damage/2;
        }
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    void FireAway()
    {
        GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
        rb1.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    IEnumerator FireBurst(int numBullets, float pause)
    {

        while (Citerator < numBullets)
        {
            //print("firing " + Citerator);
            //RotateToPlayerFP();
            FireAway();
            Citerator++;
            //print("pausing " + Citerator);
            yield return new WaitForSeconds(pause);
        }

        //RotateToPlayerFP();
        //print("done with burts");
        //RotateToPlayerFP();

        firing = null;
        Citerator = 0;
        gunTimer = reloadTimeOffset;
        yield return new WaitForSeconds(pause);


    }
}

