using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public Vector2 lookDir;
    public Transform firePoint;
    public HealthBar healthBar;
    public float moveSpeed = 5f;
    public Camera cam;
    public GameObject tryAgain;
    public GameObject exit;
    public GameObject winning;
    public Rigidbody2D rb;
    Vector2 movement;
    Vector2 mousePos;
    bool showGameOverScreen = false;
    float health = 100f;
    public bool wincondition = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (wincondition)
        {
            if (killedAllEnemies())
            {
                Win();
            }
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        lookDir = mousePos - (Vector2)firePoint.position;
        if (lookDir.magnitude > .9)
        {
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
            rb.rotation = angle;
        }

    }

    public void TakeDamage(int damage)
    {
        float percentageDamage = (damage / health) * -1f;
        healthBar.ChangeHealth(percentageDamage);
        //health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        print("Wipe yourself off man, you dead");
        Destroy(gameObject);
        tryAgain.SetActive(true);
        exit.SetActive(true);
    }
    public void Win()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("enemy");
        if (enemy == null)
        {
            print("you won");
            Destroy(gameObject);
            winning.SetActive(true);
        }


    }
    public bool killedAllEnemies()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("enemy");
        if (enemy == null)
        {
            return true;
        }
        return false;

    }

}
