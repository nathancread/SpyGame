using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Enemy : MonoBehaviour
{
    public abstract void TakeDamage(int damage,string type);
    public Transform FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        if(player)
        {
        return player.transform;
        }
        return null;
    }
    public Transform FindFirePoint()
    {
        Transform fp = transform.Find("FirepointBrute");
        if(fp)
        {
        return fp;
        }
        return null;
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void RotateToPlayerFP()
    {
        Transform player = FindPlayer();
        Vector2 target = new Vector2(player.position.x, player.position.y);
        Vector2 lookDir = target - (Vector2)FindFirePoint().position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        float t =8f;
        gameObject.GetComponent<Transform>().rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, angle), t * Time.deltaTime);
    }
    public void RotateToPlayer()
    {
        Transform player = FindPlayer();
        Vector2 target = new Vector2(player.position.x, player.position.y);
        Vector2 lookDir = target - new Vector2(this.transform.position.x, this.transform.position.y);
        Vector3 requestedDirection = new Vector3(lookDir.x, lookDir.y, 0f).normalized;
        float t = .1f;
        gameObject.GetComponent<Transform>().up = Vector3.Lerp(gameObject.GetComponent<Transform>().up, requestedDirection, t);
    }
    public bool IsCloseToPlayer(float range)
    {
        Transform player = FindPlayer();
        float totalDist = new Vector2((this.transform.position.x - player.position.x), (this.transform.position.y - player.position.y)).magnitude;
        if (totalDist < range)
        {
            return true;
        }
        return false;
    }
    public Rigidbody2D GetRb()
    {
        return gameObject.GetComponent<Rigidbody2D>();
    }
    public Transform GetFP()
    {
        return gameObject.GetComponent<Transform>();
    }
    public void MoveToPlayer(float speed)
    {
        Transform player = FindPlayer();
        Vector2 target = new Vector2(player.position.x, player.position.y);
        Rigidbody2D rb = GetRb();
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }
    public float GetAngleToPlayer()
    {
        Vector2 lookDir = (Vector2)FindPlayer().position - (Vector2)gameObject.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        return angle;
    }
}
