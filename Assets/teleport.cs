using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class teleport : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;
    public float jumpDist = 1f;
    public Tilemap tilemap;
    public float teleportCooldown = 3f;
    float cooldownTimer = 0f;
    public GameObject teleportEffect;

    // Start is called before the first frame update
    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            cooldownTimer = 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && cooldownTimer == 0)
        {

            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Jump(mousePos);
            cooldownTimer = teleportCooldown;

            //show jump animation
        }
    }
    void JumpAnimation(Vector2 jumpPos)
    {
        GameObject effect = Instantiate(teleportEffect, jumpPos, Quaternion.identity);
        Destroy(effect, 5f);
    }


    // Update is called once per frame
    void Jump(Vector2 mousePos)
    {
        //check distance
        Vector2 newPos;
        float attemptDist = new Vector2(rb.position.x - mousePos.x, rb.position.y - mousePos.y).magnitude;

        //get real jump pos
        if (attemptDist > jumpDist)
        {
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x);
            newPos = rb.position + new Vector2(Mathf.Cos(angle) * jumpDist, Mathf.Sin(angle) * jumpDist);
        }
        else
        {
            newPos = mousePos;
        }
        //check if walkable
        Vector3Int loc = tilemap.WorldToCell(newPos);
        if (tilemap.HasTile(loc))
        {
            rb.position = newPos;
            JumpAnimation(newPos);

            //yield return new WaitForSeconds(1f);

        }
        else
        {
            print("bad jump loc");
            //yield return new WaitForSeconds(1f);
            ;
        }
        //jump
    }
}
