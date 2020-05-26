using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject impactEffect;
    public LineRenderer lineRenderer;
    public int lazerDamage = 15;

    public float bulletForce = 5f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(ShootRay());
        }
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
    IEnumerator ShootRay()
    {
        int layer_mask = LayerMask.GetMask("normal", "player","projectiles","enemy");
        RaycastHit2D hitinfo = Physics2D.Raycast(firePoint.position, firePoint.up,100f,layer_mask);
        if (hitinfo)
        {

            if(hitinfo.transform.gameObject.tag == "enemy")
            {
                Enemy enemy = hitinfo.transform.GetComponent<Enemy>();
                enemy.TakeDamage(lazerDamage,"lazer");
            }
            if(hitinfo.transform.gameObject.tag == "bullet")
            {
                Bullet b = hitinfo.transform.GetComponent<Bullet>();
                b.ExplosionDamage(hitinfo.collider);
                b.Explode();
            }
            lineRenderer.SetPosition(0,firePoint.position);
            lineRenderer.SetPosition(1,hitinfo.point);
            GameObject effect = Instantiate(impactEffect,hitinfo.point,Quaternion.identity);
            Destroy(effect,1f);

        }
        else
        {
            lineRenderer.SetPosition(0,firePoint.position);
            lineRenderer.SetPosition(0,firePoint.up *1000);
        }
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(.05f);
        lineRenderer.enabled = false;

    }

}
